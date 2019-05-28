using System.Collections.Generic;

namespace SpaceBattleArena
{
    public class BaubleHuntGameInfo : BasicGameInfo
    {
        private double[][] BAUBLES;
        private int COLLECTED;
        private int STORED;
        private int STOREDVALUE;
        private int WEIGHT;

        public List<Point> getBaublePositions()
        {
            List<Point> result = new List<Point>();
            foreach (double[] pos in BAUBLES)
            {
                result.Add(new Point(pos));
            }
            return result;
        }

        public int getNumCollected() { return COLLECTED; }

        public int getNumBaublesCarried() { return STORED; }

        public int getBaublesCarriedValue() { return STOREDVALUE; }

        public int getBaublesCarriedWeight() { return WEIGHT; }

        public override string ToString()
        {
            return $"Target: {getObjectiveLocation()}; Score: {getScore()}; Deaths: {getNumDeaths()}; High Score: {getHighScore()}";
        }
    }
}