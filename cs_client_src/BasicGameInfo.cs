using Newtonsoft.Json;

public class BasicGameInfo {
	// Info about the player
	[JsonProperty]
	private double SCORE;
	[JsonProperty]
	private double BESTSCORE;
	[JsonProperty]
	private int DEATHS;
	[JsonProperty]
	private string LSTDSTRBY;
	
	// Info about the game
	[JsonProperty]
	private double HIGHSCORE;
	[JsonProperty]
	private double TIMELEFT;
	[JsonProperty]
	private double ROUNDTIME;
	
	// Possible Objective Location
	[JsonProperty]
	private double[] POSITION;

	/**
	 * Gets your current score.
	 * @return your current score
	 */
	public double getScore() { return SCORE; }
	
	/**
	 * Gets your current highest score for the round.
	 * @return your highest score
	 */
	public double getBestScore() { return BESTSCORE; }
	
	/**
	 * Gets the current game leader's score.
	 * @return the current game leader's score
	 */
	public double getHighScore() { return HIGHSCORE; }
	
	/**
	 * Gets the number of times you have died in this game.
	 * @return your number of deaths for this game 
	 */
	public int getNumDeaths() { return DEATHS; }
	
	/**
	 * Gets a string representing the last thing that destroyed your ship.
	 * 
	 * @return name of a player or type of an object with its #ID number
	 * @since 1.0.1
	 */
	public string getLastDestroyedBy() { return LSTDSTRBY; }
	
	/**
	 * Gets the current time remaining in the round (in seconds).
	 * @return time remaining in round in seconds
	 */
	public double getTimeRemaining() { return TIMELEFT; }
	
	/**
	 * Gets the total length of the round for this game (in seconds).
	 * <p>
	 * Will be 0 if there is currently no time limit.
	 * @return total length of time for the current game's round
	 */
	public double getTotalRoundTime() { return ROUNDTIME; }
	
	/**
	 * Gets a potential position of an assigned game location (read game rules for details).
	 * @return the objective's position
	 */
	public Point getObjectiveLocation() { return POSITION != null ? new Point(POSITION) : null; }

	
	override public string ToString() {
		return string.Format("{{Score: {0}; Deaths: {1}; High Score: {2}}}", getScore(), getNumDeaths(), getHighScore());
	}
}
