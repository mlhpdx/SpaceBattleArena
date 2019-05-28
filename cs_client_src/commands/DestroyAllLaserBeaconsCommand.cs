public class DestroyAllLaserBeaconsCommand : ShipCommand {	
	override public string getName() {
		return CommandNames.DestroyAllLaserBeacons.ToString();
	}
	
	/**
	 * Destroy Laser Beacons execute immediately.
	 * 
	 * @since 1.1
	 * @return true
	 */
	public static bool executesImmediately() { return true; }
}
