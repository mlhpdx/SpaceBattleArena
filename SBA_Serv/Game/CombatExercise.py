from Game import BasicGame, RoundTimer
from World.WorldGenerator import ConfiguredWorld, addObjectAwayFromOthers
from World.Entities import Entity, Ship
from GUI.ObjWrappers.GUIEntity import GUIEntity
from World.WorldMath import intpos, friendly_type, PlayerStat
from GUI.GraphicsCache import Cache
from GUI.Helpers import debugfont
import logging
import pygame
from operator import attrgetter

# Game which keeps track of statistics about combat to score points
# Each ship is worth a number of points when killed equal to how long its been alive (this.timealive)
# When a ship has been killed and respawns there will be 100 'revenge' points available to the ship destroyed if they can destroy the ship
#  that killed them.  This bonus decreases for every second elapsed since respawn.

# fraction based on damage?
# See Torpedo.owner

# Detect blocking w/ Shield and get points?
# Award points for Asteroids? Spawn Asteroids over time?
# Lose points if killed by Asteroid or self
class CombatExerciseGame(BasicGame):
    
    def __init__(self, cfgobj):
        super(CombatExerciseGame, self).__init__(cfgobj)

        self.__baubles = {}
        self.__highscore = 0
        self.__deathpenalty = self.cfg.getint("BaubleHunt", "deathpenalty")

    def GUICFG(self):
        super(CombatExerciseGame, self).GUICFG()

        self.__dfont = debugfont()

    def createWorld(self):
        w = ConfiguredWorld(self, self.cfg)
        
        # Add some initial small Baubles
        self.__addBaubles(w, self.cfg.getint("BaubleHunt", "initialbaubles"))

        return w

    def getInitialInfoParameters(self):
        return {"GAMENAME": "BaubleHunt"}

    def registerPlayer(self, name, color, imgindex, netid):
        return super(CombatExerciseGame, self).registerPlayer(name, color, imgindex, netid)

    # TODO: remove, clean start procedure? Add to start??
    def addPlayerAttributes(self, player):
        player.score = 0
        player.deaths = 0

        return player

    def newRoundForPlayer(self, player):
        super(CombatExerciseGame, self).newRoundForPlayer(player)
        player.score = 0
        player.deaths = 0

        self.__addBauble(player)
        self.__addBaubles(self.world, self.cfg.getint("BaubleHunt", "extrabaublesperplayer"))

    def __addBauble(self, player, force=False):
        logging.info("Add Bauble (%s) for Player %d", repr(force), player.netid)
        # add player bauble
        b = Bauble(addObjectAwayFromOthers(self.world, 80, 30, force))
        b.value = 3

        self.__baubles[player.netid] = b

        self.world.append(b)
        logging.info("Done Adding Bauble")

    def __addBaubles(self, w, num, force=False):
        logging.info("Adding %d Baubles (%s)", num, repr(force))
        for i in xrange(num):
            w.append(Bauble(addObjectAwayFromOthers(w, 100, 30, force)))
        logging.info("Done Adding Baubles")

    def worldObjectPreCollision(self, shapes):
        types = []
        objs = []
        for shape in shapes:
            objs.append(self.world[shape.id])
            types.append(friendly_type(objs[-1]))
        
        if "Ship" in types and "Bauble" in types:
            b = []
            ship = None
            for i in xrange(len(objs)-1, -1, -1):
                if isinstance(objs[i], Bauble):
                    b.append(objs[i])
                    del objs[i]
                elif isinstance(objs[i], Ship):
                    ship = objs[i]

            return [ False, [ [self.collectBaubles, ship, b] ] ]

    def collectBaubles(self, ship, para):
        logging.info("Collected Baubles Ship #%d", ship.id)
        for bauble in para[0]:
            # collect own Bauble?
            if bauble == self.__baubles[ship.player.netid]:
                logging.info("Collected Own Bauble #%d", ship.id)
                ship.player.score += 2

                # add new bauble
                self.__addBauble(ship.player, True)
            elif bauble in self.__baubles.values():
                logging.info("Collected Gold Bauble #%d", ship.id)
                # someone else's bauble
                for key, value in self.__baubles.iteritems():
                    if self._players.has_key(key) and value == bauble:
                        self.__addBauble(self._players[key], True)
                    elif value == bauble:
                        # Gold Bauble no longer owned, add back a regular one
                        self.__addBaubles(self.world, 1, True)
                    #eif
            else:
                logging.info("Collected Regular Bauble #%d", ship.id)
                self.__addBaubles(self.world, 1, True)
            #eif
            ship.player.score += bauble.value
            bauble.destroyed = True
            self.world.remove(bauble)
        if ship.player.score > self.__highscore:
            self.__highscore = ship.player.score
        logging.info("Done Collecting Baubles #%d", ship.id)

    def worldAddRemoveObject(self, wobj, added):
        logging.debug("BH Add Object(%s): #%d (%s)", repr(added), wobj.id, friendly_type(wobj))
        if isinstance(wobj, Ship) and (wobj.disconnected or wobj.killed):
            return super(CombatExerciseGame, self).worldAddRemoveObject(wobj, added)               
        if not added and isinstance(wobj, Ship) and wobj.player.netid in self._players:
            nid = wobj.player.netid

            self._players[nid].object = None                     

            self._players[nid].score -= self.__deathpenalty
            self._players[nid].deaths += 1
            if self._players[nid].score < 0:
                self._players[nid].score = 0

            # readd
            self._addShipForPlayer(nid, True)

    def getExtraEnvironment(self, player):
        return {"POSITION": intpos(self.__baubles[player.netid].body.position), "SCORE": player.score, "HIGHSCORE": self.__highscore, "DEATHS": player.deaths}

    def getPlayerStats(self):
        stat = sorted(self._players.itervalues(), key=attrgetter("deaths"))
        stat = sorted(stat, key=attrgetter("score"), reverse=True)
        sstat = []
        for player in stat:
            sstat.append(str(player.score) + " " + str(player.deaths) + " : " + player.name)
        return sstat
        
        #stat.append("Player "+player.name + ": "+str(player.score))

    def roundOver(self):
        self.__btimer.cancel() # prevent more baubles from being spawned!

        stat = sorted(self.getCurrentPlayers(), key=attrgetter("deaths"))
        stat = sorted(stat, key=attrgetter("score"), reverse=True)

        if not self._tournamentfinalround:
            # get winner
            for x in xrange(self.cfg.getint("BaubleHunt", "numtofinalround")):
                logging.info("Adding player to final round %s", stat[x].name)
                self.addPlayerToFinalRound(stat[x])
            #next
        else:
            #TODO: Clean up
            logging.info("Final Round Winner %s", stat[0].name)
            self._tournamentfinalwinner = stat[0]

        super(CombatExerciseGame, self).roundOver()

    def drawInfo(self, surface, flags):
        for player in self._players.values():
            if player.object != None:
                # draw line between player and Bauble
                pygame.draw.line(surface, player.color, intpos(player.object.body.position), intpos(self.__baubles[player.netid].body.position))

    def drawGameData(self, screen, flags):
        if self._tournament and self._tournamentinitialized:
            super(CombatExerciseGame, self).drawGameData(screen, flags)
            stat = sorted(self.getCurrentPlayers(), key=attrgetter("deaths"))
            stat = sorted(stat, key=attrgetter("score"), reverse=True)
            x = len(stat) - 1
            for player in stat:                
                screen.blit(self.__dfont.render(str(player.score) + " : " + str(player.deaths) + " " + player.name, False, (255, 192, 192)), (screen.get_width()-300, screen.get_height() - 64 - 12*x))
                x -= 1

    def start(self):
        super(CombatExerciseGame, self).start()

        # start Bauble Spawn Timer
        self.newBaubleTimer()    
        
    def newBaubleTimer(self):
        self.__btimer = RoundTimer(self.cfg.getint("BaubleHunt", "timefornewbauble"), self.spawnBauble)
        self.__btimer.start()

    def spawnBauble(self):
        self.__addBaubles(self.world, self.cfg.getint("BaubleHunt", "baublesperspawn"))
        
        self.newBaubleTimer()

    """
    def start(self):
        super(CombatExerciseGame, self).start()

        for player in self.getCurrentPlayers():
            player.score = 0
            player.deaths = 0

            self.__addBauble(player)
            self.__addBaubles(self.world, self.cfg.getint("BaubleHunt", "extrabaublesperplayer"))
    """
