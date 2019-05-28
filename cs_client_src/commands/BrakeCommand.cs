using System;
using Newtonsoft.Json;

namespace SpaceBattleArena
{
    public class BrakeCommand : ShipCommand
    {
        [JsonProperty]
        private double PER;

        public BrakeCommand(double power)
        {
            if (power < 0.0 || power >= 1.0)
                throw new ArgumentException("Invalid brake power: must be at least 0.0 and less than 1.0");

            PER = power;
        }
        override public string getName()
        {
            return CommandNames.Brake.ToString();
        }

        public static int getOngoingEnergyCost() { return 4; }
    }
}