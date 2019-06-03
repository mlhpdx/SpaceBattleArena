using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using SpaceBattleArena;

namespace APCS
{
    class MyMiddleShip : BasicSpaceship {
        static Random r = new Random();
        Point center;
        
	    override public RegistrationData registerShip(int numImages, int worldWidth, int worldHeight)
        {
            center = new Point(worldWidth/2, worldHeight/2);
            return new RegistrationData("Lee", Color.White, 0);
        }

	    override public ShipCommand getNextCommand(BasicEnvironment env)
        {
            var toCenter = env.getShipStatus().getPosition().getAngleTo(center);

            var toward = toCenter - env.getShipStatus().getOrientation();
            while (toward > 180) toward -= 360;
            while (toward < -180) toward += 360;

            var flip = Math.Abs(toward) > 90;
            var adjustment = flip ? toward + 180 : toward;
            while (adjustment > 180) adjustment -= 360;
            while (adjustment < -180) adjustment += 360;

            Console.WriteLine($"flip: {flip}, adjustment: {adjustment}");

            if (Math.Abs(adjustment) > 2)
                return new RotateCommand(adjustment);

            var distance = env.getShipStatus().getPosition().getDistanceTo(center);
            var closing = Math.Cos((toCenter - env.getShipStatus().getMovementDirection()) * Math.PI / 180) * env.getShipStatus().getSpeed();
            var drift = Math.Sin((toCenter - env.getShipStatus().getMovementDirection()) * Math.PI / 180) * env.getShipStatus().getSpeed();
            var eta = distance / closing;

            Console.WriteLine($"closing: {closing}, drift: {drift}, distance: {distance}, eta: {eta}");

            if (Math.Abs(drift) > 2 && r.Next() % 4 == 0)
                return new ThrustCommand(drift < 0 ^ flip ? 'L' : 'R', .1, 1, false);

            if ((eta < 0 || eta > 3.1 || closing < 0) && env.getShipStatus().getEnergy() > 50)
                return new ThrustCommand(flip ? 'F' : 'B', .1, 1, false);

            if (closing < 5 || eta > 2.9)
                return new IdleCommand(.1);

            var brakes = Math.Max(.25, Math.Min(1, distance / 50));
            return new ThrustCommand(flip ? 'B' : 'F', .1, brakes, false);
        }
    }

    class BaubleShip : BasicSpaceship {
        static Random r = new Random();
        Point center;
        
	    override public RegistrationData registerShip(int numImages, int worldWidth, int worldHeight)
        {
            center = new Point(worldWidth/2, worldHeight/2);
            return new RegistrationData("Lee", Color.White, 0);
        }

        long iteration = 0L;
        Stopwatch sw = new Stopwatch();
	    override public ShipCommand getNextCommand(BasicEnvironment env)
        {
            iteration++;

            if (env.getShipStatus().getSpeed() < 30) { 
                return new ThrustCommand('B', 1, 1.0, false);
            }
            else if (!sw.IsRunning || sw.Elapsed.TotalSeconds > 10) {
                sw.Restart();
                return new RaiseShieldsCommand(10);                
            }

            double adjust = env.getShipStatus().getOrientation() 
                - env.getShipStatus().getMovementDirection();
            while (adjust < -180) adjust += 360;
            while (adjust > 180) adjust -= 360;
            if (Math.Abs(adjust) > 5) {
                return new RotateCommand(-(int)adjust);
            }

            return new IdleCommand(.1);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var ship = new BaubleShip();
            TextClient<BasicGameInfo>.run("172.18.20.175", ship, 2012);
        }
    }
}
