// using System;

// public class GraphicalClient<T> : Client {
// 	private bool LOGGING = true;
	
// 	private int netId;
// 	private MwnpListener listener;
// 	private MwnpMessenger messenger;
// 	private string shipClassname;
// 	private Spaceship<T> ship;
// 	private Type shipType;
	
// 	private string[] args;
// 	private JFrame frame;
	
// 	private bool disconnected = false;
	
// 	public GraphicalClient(string[] args) {
// 		this.args = args;
// 	}

	
// 	public void run() {
// 		frame = new JFrame("Robots......in SPACE!!!");
// 		frame.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
// 		frame.setVisible(true);
// 		try {
// 			// Connect to server
// 			this.logMessage("Connecting to server...");
// 			Socket server = new Socket(args[0], 2012);
			
// 			// Add disconnect hook
// 			Runtime.getRuntime().addShutdownHook(new ShutdownHook(this));

// 			// Start listening for messages
// 			this.logMessage("Starting listener...");
// 			this.listener = new MwnpListener(this, server, "Listener Thead");
// 			this.listener.start();
			
// 			// Start sending messages
// 			this.logMessage("Starting messenger...");
// 			this.messenger = new MwnpMessenger(this, server, "Messenger Thread");
// 			this.messenger.start();
			
// 			// Create ship
// 			this.shipClassname = args[1];
// 			Class<?> shipType = Class.forName(this.shipClassname);
// 			this.logMessage("Creating new " + shipType.getName());
// 			this.ship = (Spaceship<?>)shipType.getConstructor().newInstance();
			
// 			System.out.println("Type QUIT to disconnect from server and end program");
// 			Scanner kb = new Scanner(System.in);
// 			while (kb.hasNextLine() && !kb.nextLine().equalsIgnoreCase("QUIT"));
// 			kb.close();
// 		} catch (IOException ex) {
// 			System.err.println("Server connection failed.");
// 			System.err.println(ex.getMessage());
// 			System.err.println(ex.getStackTrace());
// 		} catch (ClassCastException ex) {
// 			System.err.println("Specified ship type does not implement Spaceship.");
// 			System.err.println(ex.getMessage());
// 		} catch (ClassNotFoundException ex) {
// 			System.err.println("Specified ship type not found.");
// 			System.err.println(ex.getMessage());
// 		} catch (Exception ex) {
// 			System.err.println(ex.getMessage());
// 			ex.printStackTrace();
// 		}
// 	}
	
// 	/**
// 	 * Reads a network message and takes appropriate action. 
// 	 * @param msg the message received from the network
// 	 * @throws IllegalAccessException 
// 	 * @throws ArgumentException 
// 	 */
// 	public void parseMessage(MwnpMessage msg) {
// 		if (msg.getCommand().equals("MWNL2_ASSIGNMENT")) {
// 			MwnpMessage dMsg = (MwnpMessage)msg;
// 			this.netId = ((Double)(dMsg.getData())).intValue();
// 		} else if (msg.getCommand().equals("MWNL2_AC")) {
// 			System.err.println("Already connected from this IP address.  Exiting...");
// 			System.exit(1);
// 		} else if (msg.getCommand().equals("REQUEST")) {
// 			stringstringMap map = (stringstringMap)msg.getData();
// 			int numImages = Integer.parseInt(map.get("IMAGELENGTH"));
// 			int width = Integer.parseInt(map.get("WORLDWIDTH"));
// 			int height = Integer.parseInt(map.get("WORLDHEIGHT"));
			
// 			MwnpMessage.RegisterGameType(map.get("GAMENAME"));
			
// 			RegistrationData data = ship.registerShip(numImages, width, height);
			
// 			MwnpMessage response = new MwnpMessage(new Integer[]{netId, 0}, data);
// 			messenger.sendMessage(response);
// 		} else if (msg.getCommand().equals("ENV")) {
// 			Environment<?> env = (Environment<?>)msg.getData();
			
// 			ShipCommand cmd = null;
// 			try {
// 				cmd = (ShipCommand)shipType.getMethod("getNextCommand",  Environment.class).invoke(ship,  env);
// 			} catch (InvocationTargetException | NoSuchMethodException
// 					| SecurityException ex) {
// 				System.err.println("Error Invoking getNextCommand:");
// 				System.err.println(ex.getMessage());
// 				ex.printStackTrace(System.err);
// 			}
			
// 			if (cmd == null) {
// 				cmd = new IdleCommand(0.1);
// 			} else {
// 				MwnpMessage response = new MwnpMessage(new Integer[]{netId, 0}, cmd);
// 				messenger.sendMessage(response);
// 			}
// 		} else if (msg.getCommand().equals("ERROR")) {
// 			System.out.println(msg.getData());
// 		}
// 	}
	
// 	public void disconnect() {
// 		System.out.println("Attempting to disconnect...");
// 		if (!disconnected) {
// 			System.out.println("Sending disconnect message...");
// 			MwnpMessage disconnect = new MwnpMessage(new Integer[]{netId, 0}, "MWNL2_DISCONNECT", null);
// 			messenger.sendMessage(disconnect);
// 			System.out.println("Ending listener...");
// 			listener.end();
// 			System.out.println("Ending messenger...");
// 			messenger.end();
// 		}
// 		disconnected = true;
// 		System.out.println("Disconnect complete.");
// 	}
	
// 	public bool isDisconnected(){
// 		return this.disconnected;
// 	}
	
// 	public void logMessage(string message) {
// 		if (LOGGING) {
// 			Calendar now = Calendar.getInstance();
// 			string msg = string.format("LOG: %s: %s\n", DateFormat.getDateTimeInstance().format(now.getTime()), message);
			
// 			frame.getContentPane().add(new JLabel(msg));
// 		}
// 	}
	
// 	public static void main(string[] args) {
// 		GraphicalClient<T> client = new GraphicalClient<T>(args);
// 		SwingUtilities.invokeLater(client);
// 	}

// }
