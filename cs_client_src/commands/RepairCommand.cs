using System;
using Newtonsoft.Json;

public class RepairCommand : ShipCommand {
	[JsonProperty]
	private int AMT;
	
	public RepairCommand(int amount) {
		if (amount <= 0 || amount > 100) 
			throw new ArgumentException("Invalid repair amount: must be greater than 0 and no more than 100"); 
		
		this.AMT = amount;
	}

	/* (non-Javadoc)
	 * @see ihs.apcs.spacebattle.commands.ShipCommand#getName()
	 */
	
	override public string getName() {
		return CommandNames.Repair.ToString();
	}

	/**
	 * Gets the energy cost per second of this command.
	 * @return the amount of energy consumed per second while this command is executing (8)
	 */
	public static int getOngoingEnergyCost() { return 8; }
	
	/**
	 * Repairing does not block the processing of other commands.
	 * 
	 * @since 1.1
	 * @return false, repairing doesn't block.
	 */
	public static bool isBlocking() { return false; }
}
