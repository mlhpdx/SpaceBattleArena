using System.Drawing;
using Newtonsoft.Json;

public class RegistrationData {
	[JsonProperty]
	private string NAME;
	[JsonProperty]
	private int[] COLOR;
	[JsonProperty]
	private int IMAGEINDEX;
	
	/**
	 * Creates a new RegistrationData object with the specified parameters.
	 * @param name the name to be associated with the ship being registered
	 * @param color the color for the ship to appear
	 * @param image the index of the image from the server's image list
	 * 			to be used for this ship
	 */
	public RegistrationData(string name, Color color, int image) {
		this.NAME = name;
		this.COLOR = new int[] { color.R, color.G, color.B };
		this.IMAGEINDEX = image;
	}
	
	/**
	 * Gets the registered name.
	 * @return the name registered for the ship
	 */
	public string getName() { return NAME; }
	
	/**
	 * Gets the registered color.
	 * @return the color registered for the ship
	 */
	public Color getColor() { return Color.FromArgb(COLOR[0], COLOR[1], COLOR[2]); }
	
	/**
	 * Gets the registered image index.
	 * @return the index of the image registered for the ship
	 */
	public int getImage() { return IMAGEINDEX; }
	
	override public string ToString() {
		return string.Format("[{0}, [{1}, {2}, {3}], {4}]", getName(), getColor().R, getColor().G, getColor().B, getImage());
	}
}
