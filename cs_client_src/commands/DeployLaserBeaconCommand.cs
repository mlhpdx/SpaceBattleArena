namespace SpaceBattleArena
{
    public class DeployLaserBeaconCommand : ShipCommand
    {
        override public string getName()
        {
            return CommandNames.DeployLaserBeacon.ToString();
        }

        public static bool executesImmediately() { return true; }
    }
}