using Newtonsoft.Json;

namespace SpaceBattleArena
{
    public class LowerEnergyScoopCommand : ShipCommand
    {
        [JsonProperty]
        private bool SHORT;

        public LowerEnergyScoopCommand(bool shortlength)
        {
            this.SHORT = shortlength;
        }

        override public string getName()
        {
            return CommandNames.LowerEnergyScoop.ToString();
        }

        public static int getInitialEnergyCost() { return 4; }

        public static int getOngoingEnergyCost() { return 8; }

        public static bool isBlocking() { return false; }
    }
}