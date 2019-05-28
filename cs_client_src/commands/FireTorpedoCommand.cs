using System;
using Newtonsoft.Json;

public class FireTorpedoCommand : ShipCommand {
	[JsonProperty]
	private char DIR;

	/**
	 * Creates a command to fire a torpedo.
	 * @param direction which launcher to fire from ('F' for front, 'B' for back)
	 */
	public FireTorpedoCommand(char direction) {
		if (direction != 'F' && direction != 'B')
			throw new ArgumentException("Invalid torpedo direction: must be 'F' or 'B'");
		
		this.DIR = direction;
	}
	
	/* (non-Javadoc)
	 * @see ihs.apcs.spacebattle.commands.ShipCommand#getName()
	 */
	
	override public string getName() {
		return CommandNames.FireTorpedo.ToString();
	}
	
	/**
	 * Gets the one-time energy cost to initiate this command.
	 * @return the amount of energy consumed by initiating this command (12)
	 */
	public static int getInitialEnergyCost() { return 12; }
	
	/**
	 * Fire Torpedo executes immediately with a cooldown of 0.2 seconds.
	 * 
	 * @since 1.1
	 * @version 1.2
	 * @return true
	 */
	public static bool executesImmediately() { return true; }
}
