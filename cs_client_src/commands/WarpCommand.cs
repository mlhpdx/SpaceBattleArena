using System;
using Newtonsoft.Json;

public class WarpCommand : ShipCommand {
	[JsonProperty]
	private double DIST;
	
	public static double MAX_WARP_DISTANCE = 400.0; 
	
	public WarpCommand() { }
	
	public WarpCommand(double distance) {
		if (distance <= 0 || distance > MAX_WARP_DISTANCE)
			throw new ArgumentException("Invalid warp distance: must be greater than 0 and less than " + MAX_WARP_DISTANCE);
		
		this.DIST = distance;
	}

	/* (non-Javadoc)
	 * @see ihs.apcs.spacebattle.commands.ShipCommand#getName()
	 */
	
	override public string getName() {
		return CommandNames.Warp.ToString();
	}
	
	/**
	 * Gets the one-time energy cost to initiate this command.
	 * @return the amount of energy consumed by initiating this command (10)
	 */
	public static int getInitialEnergyCost() { return 10; }
	
	/**
	 * Gets the energy cost per second of this command.
	 * @return the amount of energy consumed per second while this command is executing (9)
	 */
	public static int getOngoingEnergyCost() { return 9; }
	
	protected StringMap<object> getArgs() {
		if (DIST > 0) {
			return base.getArgs();
		} else {
			return new StringMap<object>();
		}
	}

}
