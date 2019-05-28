using System.Collections.Generic;
using Newtonsoft.Json;

namespace SpaceBattleArena
{
    public class ErrorData
    {
        [JsonProperty]
        private string COMMAND;
        [JsonProperty]
        private Dictionary<string, object> PARAMETERS;
        [JsonProperty]
        private string MESSAGE;

        override public string ToString()
        {
            return string.Format("ERROR: {0}", JsonConvert.SerializeObject(this));
        }
    }
}