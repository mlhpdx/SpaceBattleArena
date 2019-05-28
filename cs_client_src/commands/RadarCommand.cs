using System;
using Newtonsoft.Json;

namespace SpaceBattleArena
{
    public class RadarCommand : ShipCommand
    {
        [JsonProperty]
        private int LVL;
        [JsonProperty]
        private int TARGET;

        public RadarCommand(int level)
        {
            if (level < 1 || level > 5 || level == 3)
                throw new ArgumentException("Invalid radar level: must be 1, 2, 4, or 5");

            this.LVL = level;
        }

        public RadarCommand(int level, int target)
        {
            if (level != 3)
                throw new ArgumentException("Invalid radar level: only radar level 3 accepts a target");
            if (target <= 0)
                throw new ArgumentException("Invalid radar target: must be positive integer");

            this.LVL = level;
            this.TARGET = target;
        }

        override public string getName()
        {
            return CommandNames.Radar.ToString();
        }

        public static int getOngoingEnergyCost() { return 6; }
    }
}