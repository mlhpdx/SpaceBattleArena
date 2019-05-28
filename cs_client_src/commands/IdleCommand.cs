using System;
using Newtonsoft.Json;

public class IdleCommand : ShipCommand {
	[JsonProperty]
	private double DUR;
	
	public IdleCommand(double duration) {
		if (duration < 0.1)
			throw new ArgumentException("Invalid idle duration: must be at least 0.1");
		
		DUR = duration;
	}

	/* (non-Javadoc)
	 * @see ihs.apcs.spacebattle.commands.ShipCommand#getName()
	 */
	
	override public string getName() {
		return CommandNames.Idle.ToString();
	}

	/**
	 * Gets the energy cost per second of this command.
	 * @return the amount of energy consumed per second while this command is executing (0)
	 */
	public static int getOngoingEnergyCost() { return 0; }
}
