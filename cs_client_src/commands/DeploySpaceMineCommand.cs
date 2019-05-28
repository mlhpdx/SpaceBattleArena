using System;
using Newtonsoft.Json;

public class DeploySpaceMineCommand : ShipCommand {
	[JsonProperty]
	private int MODE;
	[JsonProperty]
	private double DELAY;
	[JsonProperty]
	private int DIR;
	[JsonProperty]
	private int SPEED;
	[JsonProperty]
	private double DUR;

	public DeploySpaceMineCommand(double delay)
		: this(delay, false)
	{
	}
	
	public DeploySpaceMineCommand(double delay, bool homing) {
		if (delay <= 0 || delay > 10) 
			throw new ArgumentException("Invalid mine delay: must be greater than 0 and no more than 10");
		
		if (homing)
		{
			this.MODE = 3;
		}
		else 
		{
			this.MODE = 1;
		}
		this.DELAY = delay;
	}
	
	public DeploySpaceMineCommand(double delay, int direction, int speed, double duration)
	{
		if (duration <= 0 | duration > 10)
			throw new ArgumentException("Invalid mine duration: must be greater than 0 and no more than 10");
		if (speed <= 0 || speed > 5)
			throw new ArgumentException("Invalid mine speed: must be between 1 and 5 inclusive");
		if (delay <= 0 || delay > 10) 
			throw new ArgumentException("Invalid mine delay: must be greater than 0 and no more than 10");
		
		this.MODE = 2;
		this.DELAY = delay;
		this.DIR = direction;
		this.SPEED = speed;
		this.DUR = duration;
	}
	
	override public string getName() {
		return CommandNames.DeploySpaceMine.ToString();
	}
	public int getEnergyCost()
	{
		return 22 + this.MODE * 11;
	}
	public static int getInitialEnergyCost() { return 33; }	
	public static bool executesImmediately() { return true; }
}
