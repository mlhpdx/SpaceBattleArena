using Newtonsoft.Json;

namespace SpaceBattleArena
{
    public class EjectCommand : ShipCommand
    {
        [JsonProperty]
        private int ID;

        public EjectCommand(int target)
        {
            this.ID = target;
        }

        override public string getName()
        {
            return CommandNames.Eject.ToString();
        }

        public int getEnergyCost(int baubleValue)
        {
            return 3 * baubleValue;
        }

        public static int getInitialEnergyCost() { return 12; }

        public static bool executesImmediately() { return true; }
    }
}
