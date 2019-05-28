using Newtonsoft.Json;

public class SteerCommand : ShipCommand {
	[JsonProperty]
	private int DEG;
	[JsonProperty]
	private bool BLOCK;
	
	/**
	 * Creates a blocking command to steer a ship.  
	 * @param degrees the number of degrees to adjust course
	 */
	public SteerCommand(int degrees) : this(degrees, true) {
	}
	
	/**
	 * Creates a command to steer a ship.  
	 * @param degrees the number of degrees to adjust course
	 * @param block indicates if the command should block or not
	 */
	public SteerCommand(int degrees, bool block) {
		DEG = degrees;
		BLOCK = block;
	}

	/* (non-Javadoc)
	 * @see ihs.apcs.spacebattle.commands.ShipCommand#getName()
	 */
	
	override public string getName() {
		return CommandNames.Steer.ToString();
	}
	
	/**
	 * Gets the energy cost per second of this command.
	 * @return the amount of energy consumed per second while this command is executing (4)
	 */
	public static int getOngoingEnergyCost() { return 4; }
}
