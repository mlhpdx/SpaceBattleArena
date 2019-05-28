using System;
using System.Reflection;
using System.Text;
using Newtonsoft.Json;

namespace SpaceBattleArena
{
    public class Environment<T>
    {
        [JsonProperty]
        protected string[] MESSAGES;
        [JsonProperty]
        protected int RADARLEVEL;
        [JsonProperty]
        protected RadarResults RADARDATA;
        [JsonProperty]
        protected ObjectStatus SHIPDATA;
        [JsonProperty]
        protected T GAMEDATA;

        public string[] getMessages() { return MESSAGES; }

        public int getRadarLevel() { return RADARLEVEL; }

        public RadarResults getRadar() { return RADARDATA; }

        public ObjectStatus getShipStatus() { return SHIPDATA; }

        public T getGameInfo() { return GAMEDATA; }


        override public string ToString()
        {
            StringBuilder build = new StringBuilder();
            build.Append("MESSAGES: ");
            build.Append(string.Join(",", MESSAGES));

            build.Append(", GAMEINFO: ");
            build.Append(getGameInfo());

            build.Append(", RADARLEVEL: ");
            build.Append(getRadarLevel());

            build.Append(", RADARDATA: ");
            build.Append(getRadar());

            build.Append(", SHIPDATA: ");
            build.Append(getShipStatus());


            return build.ToString();
        }

        public bool equals(Environment<T> other)
        {
            try
            {
                foreach (FieldInfo f in GetType().GetFields())
                {
                    object thisVal = f.GetValue(this);
                    object otherVal = f.GetValue(other);
                    if (!thisVal.Equals(otherVal))
                        return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}