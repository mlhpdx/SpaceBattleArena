using System.Collections.Generic;

namespace SpaceBattleArena
{
    public class TheHungerBaublesGameInfo : BasicGameInfo
    {
        private List<ObjectStatus> BAUBLES;
        private bool TORPEDO;
        private bool MINE;

        public List<ObjectStatus> getBaublesInCargo()
        {
            return BAUBLES;
        }

        public int getNumBaublesCarried() { return BAUBLES.Count; }

        public int getBaublesCarriedValue()
        {
            double value = 0;
            foreach (ObjectStatus obj in BAUBLES)
            {
                value += obj.getValue();
            }
            return (int)value;
        }

        public int getBaublesCarriedWeight()
        {
            double weight = 0;
            foreach (ObjectStatus obj in BAUBLES)
            {
                weight += obj.getMass();
            }
            return (int)weight;
        }

        public bool hasFiredTorpedo()
        {
            return TORPEDO;
        }

        public bool hasFiredSpaceMine()
        {
            return MINE;
        }

        public override string ToString()
        {
            return string.Format("Target: {0}; Score: {1}; Deaths: {2}; High Score: {3}", getObjectiveLocation(), getScore(), getNumDeaths(), getHighScore());
        }
    }
}