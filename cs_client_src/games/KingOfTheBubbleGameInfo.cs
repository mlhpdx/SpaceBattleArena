using System.Collections.Generic;

public class KingOfTheBubbleGameInfo : BasicGameInfo {
	private double[][] BUBBLES;
	
	private List<Point> bubblePositions;
		
	public List<Point> getBubblePositions() {
		if (bubblePositions == null) {
			bubblePositions = new List<Point>();
		}
		if  (BUBBLES != null) {
			foreach (double[] pos in BUBBLES) {
				bubblePositions.Add(new Point(pos));
			}
		}
		return bubblePositions;
	}
	
	
	public override string ToString() {
		return string.Format("Score: {0}; Deaths: {1}; High Score: {2}; Num. Bubbles: {3}", getScore(), getNumDeaths(), getHighScore(), getBubblePositions() == null ? 0 : getBubblePositions().Count);
	}
}
