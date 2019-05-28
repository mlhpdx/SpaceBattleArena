public class DiscoveryQuestGameInfo : BasicGameInfo {
	private string[] MISSION;
	private bool FAILED;
    private int[] CURIDS;
    private int[] SUCIDS;
		
	public bool isMissionFailed() { return FAILED; }
	
	public string[] getMissionLeft() { return MISSION; }
		
   public int[] getScanIdsInProgress() { return CURIDS; }
      
   public int[] getLastSuccessfulIds() { return SUCIDS; }
      
	
	public override string ToString() {
		return $"Mission: {string.Join(",", getMissionLeft())}; Outpost: {getObjectiveLocation()}; Score: {getScore()}; Deaths: {getNumDeaths()}; High Score: {getHighScore()}";
	}
}
