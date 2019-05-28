using System.Collections.Generic;

namespace SpaceBattleArena
{
    public class RadarResults : List<ObjectStatus>
    {
        private static long serialVersionUID = -5552710352589416751L;

        public int getNumObjects()
        {
            return this.Count;
        }

        public ObjectStatus getById(int id)
        {
            foreach (ObjectStatus s in this)
            {
                if (s.getId() == id)
                    return s;
            }
            return null;
        }

        public ObjectStatus getByPosition(Point pos)
        {
            foreach (ObjectStatus s in this)
            {
                if (s.getPosition() != null && s.getPosition().Equals(pos))
                    return s;
            }
            return null;
        }

        public List<ObjectStatus> getByType(string type)
        {
            List<ObjectStatus> list = new List<ObjectStatus>();
            foreach (ObjectStatus s in this)
            {
                if (s.getType() != null && string.Equals(s.getType(), type, System.StringComparison.OrdinalIgnoreCase))
                {
                    list.Add(s);
                }
            }
            return list;
        }
    }
}