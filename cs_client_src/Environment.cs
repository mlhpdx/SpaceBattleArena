using System;
using System.Reflection;
using System.Text;
using Newtonsoft.Json;

public class Environment<T> {
	[JsonProperty]
	protected string[] MESSAGES;
	[JsonProperty]
	protected int RADARLEVEL;
	[JsonProperty]
	protected RadarResults RADARDATA;
	[JsonProperty]
	protected ObjectStatus SHIPDATA;
	[JsonProperty]
	protected T GAMEDATA;
		
	/**
	 * Gets a list of messages currently received. [Not Used]
	 * @return a list of messages
	 */
	public string[] getMessages() { return MESSAGES; }
	
	/**
	 * Gets the level of the last radar sweep, if the last command issued was a {@link ihs.apcs.spacebattle.commands.RadarCommand }.
	 * @return the level of radar sweep performed by the {@link ihs.apcs.spacebattle.commands.RadarCommand }.
	 */
	public int getRadarLevel() { return RADARLEVEL; }
	
	/**
	 * Gets the results of the most recent radar sweep, if and only if the last command issued was a {@link ihs.apcs.spacebattle.commands.RadarCommand }.
	 * @return results of the radar sweep performed with the last {@link ihs.apcs.spacebattle.commands.RadarCommand },
	 *   or null if the last command issued was not a radar sweep 
	 */
	public RadarResults getRadar() { return RADARDATA; }
	
	/**
	 * Gets the status of your ship.
	 * @return the status of your ship
	 */
	public ObjectStatus getShipStatus() { return SHIPDATA; }
	
	/**
	 * Gets information concerning the current game objective.
	 * @return information about the current game, or null if no game
	 *   is in progress
	 */
	public T getGameInfo() { return GAMEDATA; }
	
	
	override public string ToString() {
		StringBuilder build = new StringBuilder();
		build.Append("MESSAGES: ");
		build.Append(string.Join(",", MESSAGES));
		
		build.Append(", GAMEINFO: ");
		build.Append(getGameInfo());

		build.Append(", RADARLEVEL: ");
		build.Append(getRadarLevel());
		
		build.Append(", RADARDATA: ");
		build.Append(getRadar());
		
		build.Append(", SHIPDATA: ");
		build.Append(getShipStatus());
		
		
		return build.ToString();
	}
	
	public bool equals(Environment<T> other) {
		try {
			foreach (FieldInfo f in GetType().GetFields()) {
				object thisVal = f.GetValue(this);
				object otherVal = f.GetValue(other);
				if (!thisVal.Equals(otherVal))
					return false;
			}
			return true;
		} catch (Exception ex) {
			return false;
		}
	}

}
