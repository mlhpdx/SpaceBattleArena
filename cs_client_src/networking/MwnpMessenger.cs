using System;
using System.IO;
using System.Net.Sockets;

public class MwnpMessenger {
	static string name = "Messenger Thread";
	private Client client;
	private StreamWriter outStream;
	private Socket sock;
	
	private bool LOGGING = true;
	private StreamWriter logStream;
	
	public MwnpMessenger(Client client, Socket sock) {
		this.client = client;
		this.sock = sock;
		outStream = new StreamWriter(new NetworkStream(sock));
		logStream = new StreamWriter("messenger.log");
		//logStream = System.out;
	}
	
	public void start() { }
	
	public void end() { 
		client.logMessage("Messenger ending...");
		if (sock?.Connected == true) {
			sock?.Close();
			sock = null;
		}
		logStream?.Close();
		logStream = null;
	}
	
	public void sendMessage(MwnpMessage msg) {
		if (sock.Connected) {
			string msgstring = msg.toJsonstring();
			outStream.Write(msgstring);
			outStream.Flush();
			
			Console.Out.WriteLine(msg);
			if (LOGGING)
				printMessage(msg);
		} else {
			Console.Error.WriteLine("Failed to send message -- output socket closed.");
			logStream.WriteLine("Failed to send message -- output socket closed.");
		}
	}
	
	private void printMessage(MwnpMessage message) {
		logStream.Write("Message sent from {0} to {1} - \r\n", message.getSenderId(), message.getReceiverId());
		logStream.WriteLine(message);
	}
}
