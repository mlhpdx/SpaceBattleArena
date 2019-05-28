using Newtonsoft.Json;

public class ScanCommand : ShipCommand {
	[JsonProperty]
	private int TARGET;
	
	/**
	 * Creates a command to perform a research scan of a specific object.
	 * @param target id number of the object to scan.
	 */
	public ScanCommand(int target) {
		this.TARGET = target;
	}
	
	/* (non-Javadoc)
	 * @see ihs.apcs.spacebattle.commands.ShipCommand#getName()
	 */
	
	override public string getName() {
		return CommandNames.Scan.ToString();
	}

	/**
	 * Gets the one-time energy cost to initiate this command.
	 * @return the amount of energy consumed by initiating this command (4)
	 */
	public static int getInitialEnergyCost() { return 4; }	
	
	/**
	 * Gets the energy cost per second of this command.
	 * @return the amount of energy consumed per second while this command is executing (4)
	 */
	public static int getOngoingEnergyCost() { return 4; }
	
	/**
	 * Scanning does not block the processing of other commands.
	 * 
	 * @return false, scanning doesn't block.
	 */
	public static bool isBlocking() { return false; }
}
