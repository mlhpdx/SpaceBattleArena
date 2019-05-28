using Newtonsoft.Json;

namespace SpaceBattleArena
{
    public class BasicGameInfo
    {
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

        public double getScore() { return SCORE; }

        public double getBestScore() { return BESTSCORE; }

        public double getHighScore() { return HIGHSCORE; }

        public int getNumDeaths() { return DEATHS; }

        public string getLastDestroyedBy() { return LSTDSTRBY; }

        public double getTimeRemaining() { return TIMELEFT; }

        public double getTotalRoundTime() { return ROUNDTIME; }

        public Point getObjectiveLocation() { return POSITION != null ? new Point(POSITION) : null; }

        override public string ToString()
        {
            return string.Format("{{Score: {0}; Deaths: {1}; High Score: {2}}}", getScore(), getNumDeaths(), getHighScore());
        }
    }
}