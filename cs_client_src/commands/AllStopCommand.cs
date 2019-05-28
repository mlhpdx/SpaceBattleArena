public class AllStopCommand : ShipCommand {
	public AllStopCommand() { }	
	override public string getName() => CommandNames.AllStop.ToString();
	public static int getInitialEnergyCost() => 40;
	public static bool executesImmediately() => true;
}
