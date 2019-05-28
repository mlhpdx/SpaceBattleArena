using System;
using Newtonsoft.Json;

namespace SpaceBattleArena
{
    public class RaiseShieldsCommand : ShipCommand
    {
        [JsonProperty]
        private double DUR;

        public RaiseShieldsCommand(double duration)
        {
            if (duration <= 0)
                throw new ArgumentException("Invalid shield duration: must be greater than 0");

            this.DUR = duration;
        }

        override public string getName()
        {
            return CommandNames.RaiseShields.ToString();
        }

        public static int getInitialEnergyCost() { return 20; }

        public static int getOngoingEnergyCost() { return 4; }

        public static bool isBlocking() { return false; }
    }
}