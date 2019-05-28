using System.Drawing;
using Newtonsoft.Json;

namespace SpaceBattleArena
{
    public class RegistrationData
    {
        [JsonProperty]
        private string NAME;
        [JsonProperty]
        private int[] COLOR;
        [JsonProperty]
        private int IMAGEINDEX;

        public RegistrationData(string name, Color color, int image)
        {
            this.NAME = name;
            this.COLOR = new int[] { color.R, color.G, color.B };
            this.IMAGEINDEX = image;
        }

        public string getName() { return NAME; }

        public Color getColor() { return Color.FromArgb(COLOR[0], COLOR[1], COLOR[2]); }

        public int getImage() { return IMAGEINDEX; }

        override public string ToString()
        {
            return string.Format("[{0}, [{1}, {2}, {3}], {4}]", getName(), getColor().R, getColor().G, getColor().B, getImage());
        }
    }
}