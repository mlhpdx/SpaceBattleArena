using System.Collections.Generic;

public class BaubleHuntGameInfo : BasicGameInfo {
	private double[][] BAUBLES;
	private int COLLECTED;
	private int STORED;
	private int STOREDVALUE;
	private int WEIGHT;
	
	/**
	 * Gets a list of positions where there are high-value baubles.  Not all
	 *   bauble positions are returned, but each position in the list
	 *   has a bauble (unless it has been collected already).
	 * @return a list of bauble positions
	 */
	public List<Point> getBaublePositions() {
		List<Point> result = new List<Point>();
		foreach (double[] pos in BAUBLES) {
			result.Add(new Point(pos));
		}
		return result;
	}
	
	/**
	 * Gets the number of baubles collected and returned to your base.
	 * @return the number of baubles return to base
	 */
	public int getNumCollected() { return COLLECTED; }
	
	/**
	 * Gets the number of baubles currently carried by your ship (maximum of 5).
	 * @return the number of baubles carried by your ship
	 */
	public int getNumBaublesCarried() { return STORED; }
	
	/**
	 * Gets the value of the baubles being carried by your ship (maximum of 25). 
	 * @return the value of the baubles carried by your ship
	 */
	public int getBaublesCarriedValue() { return STOREDVALUE; }
	
	/**
	 * Gets the total weight of the baubles being carried by your ship.
	 * @return weight of baubles carried
	 */
	public int getBaublesCarriedWeight() { return WEIGHT; }
	
	
	public override string ToString() {
		return $"Target: {getObjectiveLocation()}; Score: {getScore()}; Deaths: {getNumDeaths()}; High Score: {getHighScore()}";
	}
}
