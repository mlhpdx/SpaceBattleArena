using Newtonsoft.Json;

public class RotateCommand : ShipCommand {
	[JsonProperty]
	private int DEG;
	
	public RotateCommand(int degrees) {
		DEG = degrees;
	}
	
	override public string getName() {
		return CommandNames.Rotate.ToString();
	}	
	public static int getOngoingEnergyCost() { return 2; }
}
