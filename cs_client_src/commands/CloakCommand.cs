using System;
using Newtonsoft.Json;

public class CloakCommand : ShipCommand {
	[JsonProperty]
	private double DUR;
	
	public CloakCommand(double duration)  {
		if (duration <= 0)
			throw new ArgumentException("Invalid cloak duration: must be greater than 0");
		
		this.DUR = duration;
	}
	
	public override string getName() => CommandNames.Cloak.ToString();
	public static int getInitialEnergyCost() => 15;
	public static int getOngoingEnergyCost() => 2;	
	public static bool isBlocking() => false;
}
