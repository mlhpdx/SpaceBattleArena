using System;
using Newtonsoft.Json;

public class RaiseShieldsCommand : ShipCommand {
	[JsonProperty]
	private double DUR;
	
	/**
	 * Creates a command to raise shields.
	 * @param duration the amount of time for which shields should be up (in seconds)
	 */
	public RaiseShieldsCommand(double duration) {
		if (duration <= 0)
			throw new ArgumentException("Invalid shield duration: must be greater than 0");
		
		this.DUR = duration;
	}

	/* (non-Javadoc)
	 * @see ihs.apcs.spacebattle.commands.ShipCommand#getName()
	 */
	
	override public string getName() {
		return CommandNames.RaiseShields.ToString();
	}

	/**
	 * Gets the one-time energy cost to initiate this command.
	 * @return the amount of energy consumed by initiating this command (20)
	 */
	public static int getInitialEnergyCost() { return 20; }
	
	/**
	 * Gets the energy cost per second of this command.
	 * @return the amount of energy consumed per second while this command is executing (4)
	 */
	public static int getOngoingEnergyCost() { return 4; }
	
	/**
	 * Raising Shields does not block the processing of other commands.
	 * 
     * @since 1.1
	 * @return false, raising shields doesn't block.
	 */
	public static bool isBlocking() { return false; }
}
