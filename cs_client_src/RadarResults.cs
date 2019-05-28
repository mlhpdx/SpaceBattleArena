using System.Collections.Generic;

public class RadarResults : List<ObjectStatus> {
	private static long serialVersionUID = -5552710352589416751L;

	/**
	 * Gets the number of objects detected by this radar sweep.
	 * <p>
	 * <i>Note: for a level 3 sweep, this will be either 0 or 1.</i>
	 * @return the number of objects detected by the radar sweep
	 */
	public int getNumObjects() {
		return this.Count; 
	}
	
	/**
	 * Gets the details read by this radar sweep for a particular object whose 
	 *   ID is previously known. 
	 * @param id the ID of the object to lookup
	 * @return the radar details for the given object, or null if the object
	 *   was not detected in this sweep
	 */
	public ObjectStatus getById(int id) {
		foreach (ObjectStatus s in this) {
			if (s.getId() == id) 
				return s;
		}
		return null;
	}
	
	/**
	 * Gets the details read by this radar sweep for a particular object whose 
	 *   location is previously known. 
	 * @param pos the position of the object to lookup
	 * @return the radar details for the given object, or null if the object
	 *   was not detected in this sweep
	 */
	public ObjectStatus getByPosition(Point pos) {
		foreach (ObjectStatus s in this) {
			if (s.getPosition() != null && s.getPosition().Equals(pos))
				return s;
		}
		return null;
	}
	
	/**
	 * Gets the details read by this radar sweep for all objects of the given
	 *   type.  Case-insensitive comparison is used for type names.
	 *
	 * <p>Could be Ship, Planet, BlackHole, Star, Nebula, Asteroid, Torpedo,
	 * Bauble, Bubble, or Outpost.     
	 *
	 * @param type the type of object for which to return results
	 * @return a list of radar details for all objects of the given type (may be empty)
	 */
	public List<ObjectStatus> getByType(string type) {
		List<ObjectStatus> list = new List<ObjectStatus>();
		foreach (ObjectStatus s in this) {
			if (s.getType() != null && string.Equals(s.getType(), type, System.StringComparison.OrdinalIgnoreCase)) {
				list.Add(s);
			}
		}
		return list;
	}
}
