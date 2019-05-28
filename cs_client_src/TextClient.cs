using System;
using System.IO;
using System.Net.Sockets;
using System.Threading;

public class TextClient<T> : Client where T : BasicGameInfo {
	private StreamWriter logStream;
	private bool LOGGING = true;
	private int netId;
	private int shipId = -1;
	private MwnpListener listener;
	private MwnpMessenger messenger;
//	private string shipSuffix = "";
	
	private Spaceship<T> ship;	
	private Environment<T> env;
	
	private volatile bool disconnected = false;
	
	public TextClient(Spaceship<T> ship) {
		try {
			logStream = new StreamWriter("client.log");
		} catch (FileNotFoundException ex) {
			Console.Error.WriteLine("Could not write to 'client.log' file.");
		}
		this.ship = ship;
	}
	
	public static void main(string[] args) {
		if (args == null || args.Length < 2)
		{		
			Console.Error.WriteLine("Invalid parameters. Require IP Address and Class Name.");
			return;
		}
		
		try {
			var ship = Activator.CreateInstance(Type.GetType(args[1])) as Spaceship<T>;
			run(args[0], ship, args.Length > 2 ? int.Parse(args[2]) : 2012);
		} catch (Exception ex) {
			Console.Error.WriteLine("Unexpected error in run: " + ex.ToString());
			return;
		}
	}
	
	public static void run(string ipAddress, Spaceship<T> ship) {
		run(ipAddress, ship, 2012);
	}
	
	/**
	 * @param args
	 * @throws IOException 
	 */
	public static void run(string ipAddress, Spaceship<T> ship, int socketNum) {

		Console.Out.WriteLine("Loading Ship " + ship.GetType().Name);
		TextClient<T> client = new TextClient<T>(ship);
		try {
			// Add disconnect hook
			Console.CancelKeyPress += (sender, args) => {
				try {
					Console.Out.WriteLine("Shutting down...");
					if (!client.isDisconnected())
					{
						client.disconnect();
					}
				} catch (IOException e) {
					Console.Error.WriteLine("Shutdown error...");
					Console.Error.WriteLine(e.ToString());
				} 				
			};

			// Connect to server
			Console.Out.WriteLine("Connecting to " + ipAddress);
			client.logMessage("Connecting to server...");
			Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			server.Connect(ipAddress, socketNum);
			
			// Start listening for messages
			client.logMessage("Starting listener...");
			client.listener = new MwnpListener(client, server);
			var t = client.listener.start();
			
			// Start sending messages
			client.logMessage("Starting messenger...");
			client.messenger = new MwnpMessenger(client, server);
			client.messenger.start();
	
//			if (args.length > 3) {
//				System.out.println("Adding suffix " + args[3]);
//				client.shipSuffix = args[3];
//			}
			
			// Wait for termination command
			Console.Out.WriteLine("Type Ctrl-C to disconnect from server and end program");
			t.Wait();
		} catch (IOException ex) {
			Console.Error.WriteLine("Server connection failed.");
			Console.Error.WriteLine(ex.ToString());
		} catch (Exception ex) {
			Console.Error.WriteLine(ex.ToString());
		} finally {
			try {
				client.disconnect();
			} catch (IOException ex) {
				Console.Error.WriteLine("Error while disconnecting:");
				Console.Error.WriteLine(ex.ToString());
			}
		}
	}
	
	/**
	 * Reads a network message and takes appropriate action. 
	 * @param msg the message received from the network
	 * @throws IllegalAccessException 
	 * @throws ArgumentException 
	 */
	public void parseMessage(MwnpMessage msg) {
		if (msg.getCommand().Equals("MWNL2_ASSIGNMENT")) {
			MwnpMessage dMsg = (MwnpMessage)msg;
			this.netId = (int)((Double)(dMsg.getData()));
		} else if (msg.getCommand().Equals("MWNL2_AC")) {
			Console.Error.WriteLine("Already connected from this IP address.  Exiting...");
			Environment.Exit(1);
		} else if (msg.getCommand().Equals("REQUEST")) {
			StringStringMap map = (StringStringMap)msg.getData();
			int numImages = int.Parse(map["IMAGELENGTH"]);
			int width = int.Parse(map["WORLDWIDTH"]);
			int height = int.Parse(map["WORLDHEIGHT"]);

			MwnpMessage.RegisterGameType(map["GAMENAME"]);		
						
			// TODO: Some games may return extra data in the map, we should figure out how we want to expose this in the client
			RegistrationData data = ship.registerShip(numImages, width, height);
			data = new RegistrationData(data.getName(), data.getColor(), data.getImage());
			
			MwnpMessage response = new MwnpMessage(new int[]{ netId, 0 }, data);
			messenger.sendMessage(response);
		} else if (msg.getCommand().Equals("ENV")) {
			env = (Environment<T>)msg.getData();

			// check for death
			int currShipId = env.getShipStatus().getId();
			if (shipId == -1) {
				shipId = currShipId;
			}
			
			if (shipId != currShipId) {
				// new id means ship has died; inform ship
				ship.shipDestroyed(((BasicGameInfo)env.getGameInfo()).getLastDestroyedBy());
				shipId = currShipId;
			}
			
			ShipCommand cmd = null;
			try {
				cmd = ship.getNextCommand(env);
			} catch (Exception ex) {
				// typically means ship has an exception; skip to the actual problem to avoid confusion
				Console.Error.WriteLine("Exception thrown by getNextCommand: \n" + ex.ToString());
				disconnect();
			}
			if (cmd == null) {
				cmd = new IdleCommand(0.1);
			}
			MwnpMessage response = new MwnpMessage(new int[] { netId, 0 }, cmd);
			messenger.sendMessage(response);
		} else if (msg.getCommand().Equals("ERROR")) {
			logMessage(msg.getData().ToString());
			Console.Error.WriteLine(msg.getData().ToString());
			//ship.processError((ErrorData)msg.getData());
		} else if (msg.getCommand().Equals("MWNL2_DISCONNECT")) {
			this.disconnect();
		}
	}
	
	public void disconnect() {		
		if (!disconnected) {
			Console.Out.WriteLine("Attempting to disconnect...");
			
			logMessage("Sending disconnect message...");
			MwnpMessage disconnect = new MwnpMessage(new int[] { netId, 0 }, "MWNL2_DISCONNECT", null);
			try {
				messenger?.sendMessage(disconnect);
			} finally { }

			logMessage("Ending listener...");
			listener?.end();

			logMessage("Ending messenger...");
			messenger?.end();
			
			logStream.Close();
			logStream = null;

			disconnected = true;
			Console.Out.WriteLine("Disconnect complete.");
			
			BasicGameInfo gameInfo = (BasicGameInfo)(env?.getGameInfo());
			Console.Out.WriteLine("\n**GAME STATISTICS**");
			Console.Out.WriteLine("   Last round score: {0}\n", gameInfo?.getScore());
			Console.Out.WriteLine("   Best round score: {0}\n", gameInfo?.getBestScore());
			Console.Out.WriteLine("   Game high score: {0}\n", gameInfo?.getBestScore());
			Console.Out.WriteLine("   Number of times destroyed: {0}\n", gameInfo?.getNumDeaths());
		}		
	}
	
	public bool isDisconnected(){
		return this.disconnected;
	}
	
	public void logMessage(string message) {
		if (LOGGING) {
			var now = DateTime.Now;
			(logStream ?? Console.Out).Write("LOG: ");
			(logStream ?? Console.Out).Write(now.ToString());
			(logStream ?? Console.Out).Write(": ");
			(logStream ?? Console.Out).WriteLine(message);
			(logStream ?? Console.Out).Flush();
		}
	}
}
