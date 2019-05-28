using System;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

public class MwnpMessage {
	private int[] ids;
	private string command;
	private object data;
	
	private static StringMap<Type> cmdDataTypes = new StringMap<Type>() {
		{ "\"MWNL2_ASSIGNMENT\"", typeof(double) },
		{ "\"REQUEST\"", typeof(StringStringMap) },
		{ "\"ERROR\"", typeof(ErrorData) }
	};
	
	public static void RegisterGameType(string gameName)
	{
		switch (gameName)
		{
			case "BaubleHunt":
				cmdDataTypes.Add("\"ENV\"", typeof(Environment<BaubleHuntGameInfo>));
				break;
			case "KingOfTheBubble":
				cmdDataTypes.Add("\"ENV\"", typeof(Environment<KingOfTheBubbleGameInfo>));
				break;
			case "DiscoveryQuest":
				cmdDataTypes.Add("\"ENV\"", typeof(Environment<DiscoveryQuestGameInfo>));
				break;
			case "TheHungerBaubles":
				cmdDataTypes.Add("\"ENV\"", typeof(Environment<TheHungerBaublesGameInfo>));
				break;
			default:
				cmdDataTypes.Add("\"ENV\"", typeof(Environment<BasicGameInfo>));
				break;
		}
	}
	
	public MwnpMessage(int[] ids, string command, object data) {
		this.ids = ids;
		this.command = command;
		this.data = data;
	}
	
	public MwnpMessage(int[] ids, ShipCommand cmd) {
		this.ids = ids;
		this.command = "SCMD";
		this.data = cmd.getMessage();
	}
	
	public MwnpMessage(int[] ids, RegistrationData regData) {
		this.ids = ids;
		this.command = "REGISTER";
		this.data = regData;
	}
	
	public int getSenderId() { return ids[0]; }
	public int getReceiverId() { return ids[1]; }
	public string getCommand() { return command; }
	public object getData() { return data; }
	
	public static MwnpMessage parseMessage(string messageText) {
		string id = messageText.Substring(0, messageText.IndexOf(']') + 1);
		string command = messageText.Substring(id.Length + 1, messageText.IndexOf('\"', messageText.IndexOf('\"') + 1) - id.Length);

		Type dataType = cmdDataTypes.ContainsKey(command) ? cmdDataTypes[command] : null;

		if (dataType != null) {
			string data = messageText.Substring(id.Length + command.Length + 2);		
			
			return new MwnpMessage(
					JsonConvert.DeserializeObject<int[]>(id),
					JsonConvert.DeserializeObject<string>(command), 
					JsonConvert.DeserializeObject(data, dataType));
		} else {
			return new MwnpMessage (
					JsonConvert.DeserializeObject<int?[]>(id).Select(i => i > 0 ? i.Value : -1).ToArray(), 
					JsonConvert.DeserializeObject<string>(command), 
					null);			
		}
	}
	
	/**
	 * Converts this message into its JSON representation.
	 * @return the JSON representation of this message
	 */
	public string toJsonstring() {
		StringBuilder build = new StringBuilder();
		
		build.Append(JsonConvert.SerializeObject(this.ids));
		build.Append(",");
		build.Append(JsonConvert.SerializeObject(this.command));
		if (this.data != null) {
			build.Append(",");
			build.Append(JsonConvert.SerializeObject(this.data));
		}
		
		int msgSize = build.Length;
		build.Insert(0, msgSize);
		
		return build.ToString();
	}

	override public string ToString()
	{
		return toJsonstring();
	}
}
