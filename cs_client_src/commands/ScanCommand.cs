using Newtonsoft.Json;

namespace SpaceBattleArena
{
    public class ScanCommand : ShipCommand
    {
        [JsonProperty]
        private int TARGET;

        public ScanCommand(int target)
        {
            this.TARGET = target;
        }

        override public string getName()
        {
            return CommandNames.Scan.ToString();
        }

        public static int getInitialEnergyCost() { return 4; }

        public static int getOngoingEnergyCost() { return 4; }

        public static bool isBlocking() { return false; }
    }
}