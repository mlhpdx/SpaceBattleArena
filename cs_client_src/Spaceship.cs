public interface Spaceship<T> {
	/**
	 * Registers a ship with the server so it can begin issuing commands.
	 * @param numImages the number of images available for the ship's 
	 * 			appearance on the server
	 * @param worldWidth the width of the current world in pixels
	 * @param worldHeight the height of the current world in pixels
	 * @return the necessary registration information for the server
	 */
	RegistrationData registerShip(int numImages, int worldWidth, int worldHeight);
	
	/**
	 * Issues a command to be executed by this ship on the server.
	 * <p>
	 * Commands are executed one at a time, except for non-blocking commands.
	 * Commands cannot be issued except when requested by the server.  The
	 * server will process each command, then send the new environment
	 * (representing the result of the issued command and any actions taken by
	 * other entities in the game world) back to the ship and request a new 
	 * command.
	 * @param env the current environment provided by the server
	 * @return a command to be executed by the ship
	 */
	ShipCommand getNextCommand(Environment<T> env);
	
	/**
	 * Notifies a ship that it has been destroyed and respawned.  Ships can use this to track their deaths.
	 * 
	 * @since 0.95
	 * @version 1.1
	 *
	 * @param lastDestroyedBy string containing the name of the player's ship or the object type name and #ID of the entity which destroyed your ship.
	 */
	void shipDestroyed(string lastDestroyedBy);
	
	
	//public void processError(ErrorData error);
}
