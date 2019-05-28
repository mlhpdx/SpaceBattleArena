using System;
using Newtonsoft.Json;

namespace SpaceBattleArena
{
    public class RepairCommand : ShipCommand
    {
        [JsonProperty]
        private int AMT;

        public RepairCommand(int amount)
        {
            if (amount <= 0 || amount > 100)
                throw new ArgumentException("Invalid repair amount: must be greater than 0 and no more than 100");

            this.AMT = amount;
        }

        override public string getName()
        {
            return CommandNames.Repair.ToString();
        }

        public static int getOngoingEnergyCost() { return 8; }

        public static bool isBlocking() { return false; }
    }
}