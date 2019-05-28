
using System;
using Newtonsoft.Json;

public class RadarCommand : ShipCommand {
	[JsonProperty]
	private int LVL;
	[JsonProperty]
	private int TARGET;
	
	/**
	 * Creates a command to perform a radar sweep of the specified level.
	 * Blocks for the amount of time specified above.
	 * @param level the level of sweep to perform
	 */
	public RadarCommand(int level) {
		if (level < 1 || level > 5 || level == 3)
			throw new ArgumentException("Invalid radar level: must be 1, 2, 4, or 5");
		
		this.LVL = level;
	}
	
	/**
	 * Creates a command to perform a targeted radar sweep to obtain full
	 *   details on a particular target.  This is a level 3 sweep.
	 * @param level the level of sweep to perform <b><i>(Must be 3)</i></b> 
	 * @param target the id number of the target to scan
	 */
	public RadarCommand(int level, int target) {
		if (level != 3)
			throw new ArgumentException("Invalid radar level: only radar level 3 accepts a target");
		if (target <= 0)
			throw new ArgumentException("Invalid radar target: must be positive integer");
		
		this.LVL = level;
		this.TARGET = target;
	}

	/* (non-Javadoc)
	 * @see ihs.apcs.spacebattle.commands.ShipCommand#getName()
	 */
	
	override public string getName() {
		return CommandNames.Radar.ToString();
	}

	/**
	 * Gets the energy cost per second of this command.
	 * @return the amount of energy consumed per second while this command is executing (6)
	 */
	public static int getOngoingEnergyCost() { return 6; }
}
