using Newtonsoft.Json;

namespace SpaceBattleArena
{
    public class SteerCommand : ShipCommand
    {
        [JsonProperty]
        private int DEG;
        [JsonProperty]
        private bool BLOCK;

        public SteerCommand(int degrees) : this(degrees, true)
        {
        }

        public SteerCommand(int degrees, bool block)
        {
            DEG = degrees;
            BLOCK = block;
        }

        override public string getName()
        {
            return CommandNames.Steer.ToString();
        }

        public static int getOngoingEnergyCost() { return 4; }
    }
}