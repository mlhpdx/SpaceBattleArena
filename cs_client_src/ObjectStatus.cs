using System;
using System.Reflection;
using Newtonsoft.Json;

namespace SpaceBattleArena
{
    public class ObjectStatus
    {
        // Fields
        [JsonProperty]
        private int ID;
        [JsonProperty]
        private string TYPE;
        [JsonProperty]
        private double TIMEALIVE;

        [JsonProperty]
        private double[] POSITION;
        [JsonProperty]
        private double SPEED;
        [JsonProperty]
        private double MAXSPEED;
        [JsonProperty]
        private double DIRECTION;
        [JsonProperty]
        private long MASS;

        [JsonProperty]
        private double CURHEALTH;
        [JsonProperty]
        private double MAXHEALTH;
        [JsonProperty]
        private double CURENERGY;
        [JsonProperty]
        private double MAXENERGY;
        [JsonProperty]
        private double ENERGYRECHARGERATE;

        [JsonProperty]
        private int ROTATION; // Ship,Nebula Only
        [JsonProperty]
        private int ROTATIONSPEED; // Ship Only
        [JsonProperty]
        private double CURSHIELD; // Ship Only
        [JsonProperty]
        private double MAXSHIELD; // Ship Only
        [JsonProperty]
        private int RADARRANGE; // Ship Only
        [JsonProperty]
        private string[] CMDQ; // Ship Only
        [JsonProperty]
        private bool INBODY; // Ship or Celestial Body Only;
        [JsonProperty]
        private double HITRADIUS; // Round Only

        // Game Specific
        [JsonProperty]
        private double VALUE; // Bubble, Bauble or Ship Only
        [JsonProperty]
        private int NUMSTORED; // Ship Only - Number of Baubles Carried
        [JsonProperty]
        private bool SUCCESS; // Game bool for success for this object

        [JsonProperty]
        private int PULL; // Planet/BlackHole/Nebula Only
        [JsonProperty]
        private int MAJOR; // Planet/BlackHole/Nebula Only
        [JsonProperty]
        private int MINOR; // Planet/BlackHole/Nebula Only

        [JsonProperty]
        private int OWNERID; // Torpedo/Outpost/SpaceMine Only

        [JsonProperty]
        private string NAME; // If Turned on in Server Config

        // Getter methods
        /**
         * Gets the object's Unique ID. 
         * 
         * Note: Ship IDs will change when they are destroyed.
         * @return a unique identifier for the object
         */
        public int getId() { return ID; }
        /**
         * string representation of the Type of object.
         * 
         * Could be Ship, Planet, BlackHole, Star, Nebula, Asteroid, Dragon, Torpedo,
         * Bauble, Bubble, or Outpost.
         * 
         * @return name of the object's type.
         */
        public string getType() { return TYPE; }
        /**
         * Gets the amount of time this object has lived in seconds.
         * @return seconds alive.
         */
        public double getTimeAlive() { return TIMEALIVE; }

        /**
         * Gets the position of this object within the world (0, 0) is the upper-left and increases down and to the right.
         *
         * @return the x, y position in pixel coordinates as a {@link ihs.apcs.spacebattle.Point }.
         */
        public Point getPosition() { return POSITION == null ? null : new Point(POSITION); }
        /**
         * Gets this object's current speed.
         * 
         * Combine with its direction to get a Velocity Vector.
         * @return current speed in pixels per second
         */
        public double getSpeed() { return SPEED; }
        /**
         * Gets the maximum speed this object can travel.
         * @return upper-bound of obtainable speed
         */
        public double getMaxSpeed() { return MAXSPEED; }
        /**
         * Get the actual direction of travel for this object.  This may not equal its the direction it is facing (orientation) {@link #getOrientation() }
         * @return angle in degrees
         * 
         * @version 1.2
         */
        public double getMovementDirection() { return (DIRECTION + 360.0) % 360.0; }
        /**
         * Gets this object's mass.
         * @return mass value.
         */
        public long getMass() { return MASS; }

        /**
         * Gets the current Health of this object.
         * 
         * When an object has zero or less health, it is destroyed.
         * @return health remaining.
         */
        public double getHealth() { return CURHEALTH; }
        /**
         * Gets the maximum amount of health this object could have. (100 for Ships)
         * @return upper-bound of health value.
         */
        public double getMaxHealth() { return MAXHEALTH; }
        /**
         * Gets the current amount of energy this object has.
         * This is only provided for your own ship. (i.e. You can't see other ship's energy levels through radar.)
         * 
         * @return energy remaining.
         */
        public double getEnergy() { return CURENERGY; }
        /**
         * Gets the maximum amount of energy this object could store. (100 for Ships)
         * @return upper-bound of energy value.
         */
        public double getMaxEnergy() { return MAXENERGY; }
        /**
         * Gets the amount of energy this object restores per second.
         * @return energy per second restored.
         */
        public double getRechargeRate() { return ENERGYRECHARGERATE; }

        // Ship Only
        /**
         * Gets the current Orientation of a Ship or Nebula.
         * @return the orientation in degrees.
         * 
         * @version 1.2
         */
        public int getOrientation() { return (ROTATION + 360) % 360; }

        /**
         * Gets the number of degrees this Ship can turn in a second (default is 120).
         * @return speed in degrees per second the ship can rotate.
         * 
         * @version 1.2
         */
        public int getRotationSpeed() { return ROTATIONSPEED; }

        /**
         * Gets the current Shield strength of a Ship.
         * @return shields remaining.
         */
        public double getShieldLevel() { return CURSHIELD; }

        /**
         * Gets the maximum amount of Shields the Ship object could have. (default is 100)
         * @return upper-bound of shield value.
         */
        public double getMaxShield() { return MAXSHIELD; }

        /**
         * Gets the radius from a Ship object that its radar can detect objects. 
         * @return radar radius.
         */
        public int getRadarRange() { return RADARRANGE; }

        /**
         * Gets the list of {@link CommandNames} your Ship is currently executing.
         * This is only provided for your own ship. (i.e. You can't see other ship's queues through radar.)
         * If the last command requested was a blocking command, it will have finished before this list is populated.
         * 
         * @return commands still being executed
         */
        public string[] getCommandQueue() { return CMDQ; }

        /**
         * Is this object in a celestial object's body of effect?
         * @return true if a ship is in a celestial body's main area of effect (could be false but in gravity well still {@link #getAxisMajorLength()}).
         * A celestial body will return true if it contains an object like a ship.
         *
         * @since 1.1
         */
        public bool isInCelestialBody() { return INBODY; }

        /**
         * Was an object successfully scanned for a game? (Discovery Quest)
         * @return true if this object has a value of success for the current game.
         * i.e. Successfully scanned in Discovery Quest.
         *
         * @since 1.1
         */
        public bool isSuccessful() { return SUCCESS; }

        /**
         * Gets the point value worth of a Bubble, Bauble, or Ship in a Game.
         * @return point value.
         */
        public double getValue() { return VALUE; }

        /**
         * Gets the number of cargo items carried by a Ship in a Game. 
         * @return number of items/points
         */
        public int getNumberStored() { return NUMSTORED; }

        /**
         * Gets the radius of the object needed for collision detection. This tells you an object's radius.
         * @return the radius of the object from its {@link ihs.apcs.spacebattle.ObjectStatus#getPosition() }
         */
        public double getHitRadius() { return HITRADIUS; }

        /**
         * Gets the strength of the gravity or drag of a Planet, BlackHole or Nebula. A higher number represents a stronger effect.
         * @return pull value.
         * @since 1.0
         */
        public int getPullStrength() { return PULL; }
        /**
         * Gets the length of a Planet, BlackHole, or Nebula on its major axis.  For Planets and BlackHoles, the Major and Minor lengths will be the same.
         * <p>
         * For Planets and BlackHoles this length represents the size of the gravitational field around it.  The {@link #getPullStrength()} method will tell you how much it will effect your ship.
         * <p>
         * For Nebula this length is oriented in the Nebula's direction/rotation see {@link #getOrientation() } and represents its major radius.  If your ship is in the Nebula, it will slow down based on the {@link #getPullStrength()} amount.
         * @return the elliptical radius along the parallel axis
         * @since 1.0
         */
        public int getAxisMajorLength() { return MAJOR; }

        /**
         * Gets the length from the center of a Nebula that is perpendicular to the direction/rotation of the Nebula.
         * @return the elliptical radius along the perpendicular axis
         * @since 1.0
         */
        public int getAxisMinorLength() { return MINOR; }

        /**
         * Gets the ID of the Owner of this object.  This could be who fired a Torpedo or who owns an Outpost (Bauble Hunt).
         * @return the owner id of this object (if any)
         */
        public int getOwnerId() { return OWNERID; }

        /**
         * Gets the name of a Ship object. (may be turned off on the server but see {@link #getType() })
         * @return a name of a Ship
         */
        public string getName() { return NAME; }

        override public string ToString()
        {
            try
            {
                StringMap<object> map = new StringMap<object>();
                foreach (FieldInfo f in GetType().GetFields())
                {
                    object val = f.GetValue(this);
                    if (val != null)
                    {
                        if (f.Name.Equals("POSITION"))
                        {
                            map.Add(f.Name, string.Join(",", (double[])val));
                        }
                        else
                        {
                            map.Add(f.Name, val);
                        }
                    }
                }
                return map.ToString();
            }
            catch (Exception e)
            {
                return "";
            }
        }

        public bool equals(ObjectStatus other)
        {
            try
            {
                foreach (FieldInfo f in GetType().GetFields())
                {
                    object thisVal = f.GetValue(this);
                    object otherVal = f.GetValue(other);
                    if (!thisVal.Equals(otherVal))
                        return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}