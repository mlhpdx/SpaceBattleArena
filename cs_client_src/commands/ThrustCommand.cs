using System;
using Newtonsoft.Json;

public class ThrustCommand : ShipCommand {
	[JsonProperty]
	private char DIR;
	[JsonProperty]
	private double DUR;
	[JsonProperty]
	private double PER;
	[JsonProperty]
	private bool BLOCK;
	
	public ThrustCommand(char dir, double duration, double power) : this(dir, duration, power, true) {
	}
	
	public ThrustCommand(char dir, double duration, double power, bool block) {
		if (dir != 'L' && dir != 'F' && dir != 'R' && dir != 'B')
			throw new ArgumentException("Invalid thrust direction; must be one of 'L', 'F', 'R', or 'B'");
		if (power < 0.1 || power > 1.0)
			throw new ArgumentException("Invalid thrust power: must be between 0.1 and 1.0");
		if (duration < 0.1)
			throw new ArgumentException("Invalid thrust duration: must be at least 0.1");
		
		DIR = dir;
		DUR = duration;
		PER = power;
		BLOCK = block;
	}

	/* (non-Javadoc)
	 * @see ihs.apcs.spacebattle.commands.ShipCommand#getName()
	 */
	
	override public string getName() {
		return CommandNames.Thrust.ToString();
	}

	/**
	 * Gets the energy cost per second of this command.
	 * @return the amount of energy consumed per second while this command is executing (3)
	 */
	public static int getOngoingEnergyCost() { return 3; }
	
	/**
	 * Thrust commands will prevent you from executing other commands by default, pass a 'false' block argument to change this behavior.
	 * 
     * @since 1.1
     * @version 1.2
	 * @return true, thrusting blocks by default in version 1.2.
	 */
	public static bool isBlocking() { return true; }
}
