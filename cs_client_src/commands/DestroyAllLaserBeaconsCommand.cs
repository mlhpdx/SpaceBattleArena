namespace SpaceBattleArena
{
    public class DestroyAllLaserBeaconsCommand : ShipCommand
    {
        override public string getName()
        {
            return CommandNames.DestroyAllLaserBeacons.ToString();
        }

        public static bool executesImmediately() { return true; }
    }
}
