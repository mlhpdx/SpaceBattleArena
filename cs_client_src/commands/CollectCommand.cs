using Newtonsoft.Json;

public class CollectCommand : ShipCommand {
	[JsonProperty]
	private int ID;

	public CollectCommand(int target) {
		this.ID = target;
	}
		
	override public string getName() {
		return CommandNames.Collect.ToString();
	}
	
	public static int getInitialEnergyCost() { return 8; }
	
	public static bool executesImmediately() { return true; }
}
