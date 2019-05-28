using System.Collections.Generic;

public class TheHungerBaublesGameInfo : BasicGameInfo {
	private List<ObjectStatus> BAUBLES;
	private bool TORPEDO;
	private bool MINE;
	
	/**
	 * Gets a list of the baubles in your cargo hold.
	 * Will be {@link ihs.apcs.spacebattle.ObjectStatus} classes with Mass, Value, and ID filled in.
	 * @return the set of baubles carried by the ship
	 */
	public List<ObjectStatus> getBaublesInCargo() {
		return BAUBLES;
	}
	
	/**
	 * Gets the number of baubles currently carried by your ship.
	 * @return the number of baubles carried by your ship
	 */
	public int getNumBaublesCarried() { return BAUBLES.Count; }
	
	/**
	 * Gets the value of the baubles being carried by your ship. 
	 * @return the value of the baubles carried by your ship
	 */
	public int getBaublesCarriedValue() {
		double value = 0;
		foreach (ObjectStatus obj in BAUBLES)
		{
			value += obj.getValue();
		}
		return (int)value; 
	}
	
	/**
	 * Gets the total weight of the baubles being carried by your ship.
	 * @return weight of baubles carried
	 */
	public int getBaublesCarriedWeight() {
		double weight = 0;
		foreach (ObjectStatus obj in BAUBLES)
		{
			weight += obj.getMass();
		}
		return (int)weight; 
	}
	
	/**
	 * Indicates if you already have a torpedo in the world already.
	 * @return true if your torpedo exists in the world already
	 */
	public bool hasFiredTorpedo() {
		return TORPEDO;
	}
	
	/**
	 * Indicated if you already have a Space Mine in the world already.
	 * @return true if your space mine exists in the world already
	 */
	public bool hasFiredSpaceMine() {
		return MINE;
	}
	
	
	public override string ToString() {
		return string.Format("Target: {0}; Score: {1}; Deaths: {2}; High Score: {3}", getObjectiveLocation(), getScore(), getNumDeaths(), getHighScore());
	}
}
