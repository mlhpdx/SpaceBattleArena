package ihs.apcs.spacebattle.networking;

import com.google.gson.Gson;

import ihs.apcs.spacebattle.*;
import ihs.apcs.spacebattle.commands.ShipCommand;
import ihs.apcs.spacebattle.util.StringMap;
import ihs.apcs.spacebattle.util.StringStringMap;

public class MwnpMessage {
	private int[] ids;
	private String command;
	private Object data;
	
	private static StringMap<Class<?>> cmdDataTypes;
	
	static {
		cmdDataTypes = new StringMap<Class<?>>();
		cmdDataTypes.put("\"MWNL2_ASSIGNMENT\"", double.class);
		cmdDataTypes.put("\"REQUEST\"", StringStringMap.class);
		cmdDataTypes.put("\"ENV\"", Environment.class);
		cmdDataTypes.put("\"ERROR\"", ErrorData.class);
	}
	
	public MwnpMessage(int[] ids, String command, Object data) {
		this.ids = ids;
		this.command = command;
		this.data = data;
	}
	
	public MwnpMessage(int[] ids, ShipCommand cmd) throws IllegalArgumentException, IllegalAccessException {
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
	public String getCommand() { return command; }
	public Object getData() { return data; }
	
	/**
	 * Parses JSON text into a MwnpMessage.
	 * This process is non-trivial because the network messages contain mixed types.
	 * It is assumed that messages look like this:
	 *   ##[sender, receiver],COMMANDNAME,DATA
	 * where ## is the length of the message in bytes.  The message must then be parsed
	 *   as three separate JSON objects. 
	 * @param messageText the string representation of the message
	 * @return the @MwnpMessage object created from this text
	 */
	public static MwnpMessage parseMessage(String messageText) {
		String id = messageText.substring(0, messageText.indexOf(']') + 1);
		String command = messageText.substring(id.length() + 1, messageText.indexOf('\"', messageText.indexOf('\"') + 1) + 1);

		Gson gson = new Gson();
		Class<?> dataType = cmdDataTypes.get(command);

		if (dataType != null) {
			String data = messageText.substring(id.length() + command.length() + 2);
			
			return new MwnpMessage (
					gson.fromJson(id, int[].class),
					gson.fromJson(command, String.class), 
					gson.fromJson(data, dataType));
		} else {
			return new MwnpMessage (
					gson.fromJson(id, int[].class),
					gson.fromJson(command, String.class), 
					null);			
		}
	}
	
	/**
	 * Converts this message into its JSON representation.
	 * @return the JSON representation of this message
	 */
	public String toJsonString() {
		Gson gson = new Gson();
		StringBuilder build = new StringBuilder();
		
		build.append(gson.toJson(this.ids));
		build.append(",");
		build.append(gson.toJson(this.command));
		if (this.data != null) {
			build.append(",");
			build.append(gson.toJson(this.data));
		}
		
		int msgSize = build.length();
		build.insert(0, msgSize);
		
		return build.toString();
	}
}
