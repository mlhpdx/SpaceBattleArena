using System;
using Newtonsoft.Json;

namespace SpaceBattleArena
{
    public class ThrustCommand : ShipCommand
    {
        [JsonProperty]
        private char DIR;
        [JsonProperty]
        private double DUR;
        [JsonProperty]
        private double PER;
        [JsonProperty]
        private bool BLOCK;

        public ThrustCommand(char dir, double duration, double power) : this(dir, duration, power, true)
        {
        }

        public ThrustCommand(char dir, double duration, double power, bool block)
        {
            if (dir != 'L' && dir != 'F' && dir != 'R' && dir != 'B')
                throw new ArgumentException("Invalid thrust direction; must be one of 'L', 'F', 'R', or 'B'");
            if (power < 0.1 || power > 1.0)
                throw new ArgumentException("Invalid thrust power: must be between 0.1 and 1.0");
            if (duration < 0.1)
                throw new ArgumentException("Invalid thrust duration: must be at least 0.1");

            DIR = dir;
            DUR = duration;
            PER = power;
            BLOCK = block;
        }

        override public string getName()
        {
            return CommandNames.Thrust.ToString();
        }

        public static int getOngoingEnergyCost() { return 3; }

        public static bool isBlocking() { return true; }
    }
}