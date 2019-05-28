using Newtonsoft.Json;

public class LowerEnergyScoopCommand : ShipCommand {
	[JsonProperty]
	private bool SHORT;
	
	/**
	 * Creates a command to lower your energy scoop to recharge your ships energy in a Nebula or Star.
	 * @param shortlength indicates if you want to recharge for 3 (true) or 6 (false) seconds.
	 */
	public LowerEnergyScoopCommand(bool shortlength) {
		this.SHORT = shortlength;
	}

	/* (non-Javadoc)
	 * @see ihs.apcs.spacebattle.commands.ShipCommand#getName()
	 */
	
	override public string getName() {
		return CommandNames.LowerEnergyScoop.ToString();
	}

	/**
	 * Gets the one-time energy cost to initiate this command.
	 * @return the amount of energy consumed by initiating this command (4)
	 */
	public static int getInitialEnergyCost() { return 4; }
	
	/**
	 * Gets the energy cost per second of this command.
	 * @return the amount of energy consumed per second while this command is executing (8)
	 */
	public static int getOngoingEnergyCost() { return 8; }	
	
	/**
	 * Scooping does not block the processing of other commands.
	 * 
	 * @return false, scooping doesn't block.
	 */
	public static bool isBlocking() { return false; }
}
