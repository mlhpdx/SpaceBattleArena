using System;
using Newtonsoft.Json;

namespace SpaceBattleArena
{
    public class FireTorpedoCommand : ShipCommand
    {
        [JsonProperty]
        private char DIR;

        public FireTorpedoCommand(char direction)
        {
            if (direction != 'F' && direction != 'B')
                throw new ArgumentException("Invalid torpedo direction: must be 'F' or 'B'");

            this.DIR = direction;
        }

        override public string getName()
        {
            return CommandNames.FireTorpedo.ToString();
        }

        public static int getInitialEnergyCost() { return 12; }

        public static bool executesImmediately() { return true; }
    }
}