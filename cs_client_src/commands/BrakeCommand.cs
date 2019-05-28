
using System;
using Newtonsoft.Json;

public class BrakeCommand : ShipCommand {
	[JsonProperty]
	private double PER;

	public BrakeCommand(double power) {
		if (power < 0.0 || power >= 1.0)
			throw new ArgumentException("Invalid brake power: must be at least 0.0 and less than 1.0");
		
		PER = power;
	}
	override public string getName() {
		return CommandNames.Brake.ToString();
	}

	/**
	 * Gets the energy cost per second of this command.
	 * @return the amount of energy consumed per second while this command is executing (4)
	 */
	public static int getOngoingEnergyCost() { return 4; }
}
