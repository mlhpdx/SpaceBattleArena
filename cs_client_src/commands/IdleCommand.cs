using System;
using Newtonsoft.Json;

namespace SpaceBattleArena
{
    public class IdleCommand : ShipCommand
    {
        [JsonProperty]
        private double DUR;

        public IdleCommand(double duration)
        {
            if (duration < 0.1)
                throw new ArgumentException("Invalid idle duration: must be at least 0.1");

            DUR = duration;
        }

        override public string getName()
        {
            return CommandNames.Idle.ToString();
        }

        public static int getOngoingEnergyCost() { return 0; }
    }
}