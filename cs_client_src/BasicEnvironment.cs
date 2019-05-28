public class BasicEnvironment : Environment<BasicGameInfo> {	
	public BasicEnvironment(Environment<BasicGameInfo> env) {
		MESSAGES = env.getMessages();
		RADARLEVEL = env.getRadarLevel();
		RADARDATA = env.getRadar();
		SHIPDATA = env.getShipStatus();
		GAMEDATA = env.getGameInfo();
	}
}
