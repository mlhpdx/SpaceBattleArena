public interface Client {
	bool isDisconnected();
	void disconnect();
	void parseMessage(MwnpMessage msg);
	void logMessage(string message);
}
