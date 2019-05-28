using Newtonsoft.Json;

public class EjectCommand : ShipCommand {
	[JsonProperty]
	private int ID;

	/**
	 * Creates a command to drop the specified Bauble.
	 * @param target id number of the bauble.
	 */
	public EjectCommand(int target) {
		this.ID = target;
	}
		
	/* (non-Javadoc)
	 * @see ihs.apcs.spacebattle.commands.ShipCommand#getName()
	 */
	
	override public string getName() {
		return CommandNames.Eject.ToString();
	}
	
	/**
	 * Gets the energy cost for the constructed command.
	 * @return (3 * baubleValue)
	 */
	public int getEnergyCost(int baubleValue)
	{
		return 3 * baubleValue;
	}
	
	/**
	 * Gets the average one-time energy cost to initiate this command.
	 * @return the amount of energy consumed by initiating this command (3 * Bauble Value)
	 */
	public static int getInitialEnergyCost() { return 12; }
	
	/**
	 * Ejecting Baubles executes immediately after a cooldown of 0.5 seconds * mass of Bauble to eject.
	 * 
	 * @return true
	 */
	public static bool executesImmediately() { return true; }
}
