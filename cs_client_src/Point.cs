using System;
using System.Collections.Generic;

public class Point {
	private double x;
	private double y;

	private double ConvertToRadians(double degrees)
	{
		return (Math.PI / 180) * degrees;
	}	
	private double ConvertToDegrees(double radians)
	{
		return 180 * (radians / Math.PI);
	}	
	
	/**
	 * Creates a new point with the given coordinates.
	 * @param x the x-coordinate
	 * @param y the y-coordinate
	 */
	public Point(double x, double y) {
		this.x = x;
		this.y = y;
	}
	
	/**
	 * Creates a new point from an array.  The first element of the array is
	 *   considered to be the x-coordinate and the second element is the 
	 *   y-coordinate.
	 * @param coords the coordinates of the point
	 */
	public Point(double[] coords) {
		this.x = coords[0];
		this.y = coords[1];
	}
	
	/**
	 * Gets the x-coordinate of this point.
	 * @return the x-coordinate
	 */
	public double getX() { return x; }
	
	/**
	 * Gets the y-coordinate of this point.
	 * @return the y-coordinate
	 */
	public double getY() { return y; }
	
	/**
	 * Gets the distance from this to another Point in space using the
	 *   standard distance formula.
	 * @param other the location to which to calculate the distance
	 * @return the distance between this point and other
	 */
	public double getDistanceTo(Point other) {
		return Math.Sqrt(Math.Pow(other.getX() - this.getX(), 2) + Math.Pow(other.getY() - this.getY(), 2));
	}
	
	/**
	 * Gets the absolute angle between this and another Point in space.
	 *   More specifically, calculates the counter-clockwise angle from due 
	 *   east (0 degrees) to a line containing both this and other.  
	 *   <p>
	 *   To determine the amount of rotation necessary to face the argument,
	 *     a ship's current {@link ObjectStatus#getOrientation() orientation} should be subtracted from the result 
	 *     of this method.
	 * @param other the location to which to calculate the angle
	 * @return the absolute angle from this to other
	 * 
	 * @version 1.2
	 */
	public int getAngleTo(Point other) {		
		double xDiff = other.getX() - this.getX();
		double yDiff = -1 * (other.getY() - this.getY()); // Our coordinates are flipped compared to regular math functions.
		
		return (int)Math.Round(ConvertToDegrees(Math.Atan2(yDiff, xDiff)) + 360) % 360;
	}
	
	/**
	 * Returns a new point which represents the location at the given angle and distance away from this point.
	 * 
	 * @param angle from due east (0 degrees)
	 * @param distance from this point
	 * @return a new Point the given angle/distance away from this point
	 * 
	 * @since 1.2
	 */
	public Point getPointAt(double angle, double distance) {
		return new Point(this.getX() + distance * Math.Cos(ConvertToRadians(angle)), 
						 this.getY() - distance * Math.Sin(ConvertToRadians(angle)));
	}
	
	/**
	 * Checks if this point is in an Ellipse with the given center point, major/minor axis lengths at the given orientation.
	 * 
	 * @param center of the ellipse
	 * @param major axis length
	 * @param minor axis length
	 * @param orientation angle of the major axis
	 * @return true if this point is within the specified ellipse
	 * 
	 * @since 1.2
	 */
	public bool isInEllipse(Point center, int major, int minor, int orientation)
	{
		double xDiff = this.getX() - center.getX();
		double yDiff = -1 * (this.getY() - center.getY()); // Our coordinates are flipped compared to regular math functions.
		
		double cos = Math.Cos(ConvertToRadians(orientation));
		double sin = Math.Sin(ConvertToRadians(orientation));

		return (Math.Pow(cos * xDiff + sin * yDiff, 2) / (major * major)) +
			   (Math.Pow(sin * xDiff - cos * yDiff, 2) / (minor * minor)) <= 1;
	}
	
	/**
	 * Maps the other point across world boundaries to return the closest form of the given point.
	 * 
	 * Note: This point may map outside the bounds of the world, it should only be used for orientation purposes and distance only.
	 *  
	 * @param other point to map from this one
	 * @param width of the world
	 * @param height of the world
	 * @return form of the point that is closest to this one
	 * 
	 * @since 1.2
	 */
	public Point getClosestMappedPoint(Point other, int width, int height) 
	{
		Point closest = null;
		foreach (Point p in other.getPointsOnTorus(width, height)) {
			if (closest == null || this.getDistanceTo(p) < this.getDistanceTo(closest)) {
				closest = p;
			}
		}
		
		return closest == null ? other : closest;
	}
	
	/**
	 * Wraps this point on a torus of size width, height of the edge of the Torus.
	 * 
	 * Returns a List of points (including this one) which represent the coordinates of this point projected beyond the bounds of the 'world' on a Torus.
	 * This can be used to determine if a point is actually close to a position when it exists beyond the bounds of the 'world'.
	 * 
	 * @param width of torus
	 * @param height of torus
	 * @return set of points projected beyond bounds of torus
	 * 
	 * @since 1.2
	 */
	private List<Point> getPointsOnTorus(int width, int height) {
		List<Point> points = new List<Point>();
		
		points.Add(new Point(this.getX(), this.getY()));		
		points.Add(new Point(this.getX() + width, this.getY() + height));
		points.Add(new Point(this.getX() + width, this.getY()));				
		points.Add(new Point(this.getX(), this.getY() + height));
		points.Add(new Point(this.getX() - width, this.getY() - height));
		points.Add(new Point(this.getX() - width, this.getY()));
		points.Add(new Point(this.getX(), this.getY() - height));
		
		return points;
	}
	
	/**
	 * Returns true if the given point's X & Y values are within the tolerance of this point's X & Y values.
	 * This is a 'box' test.
	 * 
	 * @param other location to test against
	 * @param tolerance amount to test in range against
	 * @return true if the points are close to one another.
	 * 
	 * @since 1.2
	 */
	public bool isCloseTo(Point other, double tolerance) {
		return (Math.Abs(this.getX() - other.getX()) <= tolerance &&
				Math.Abs(this.getY() - other.getY()) <= tolerance);
	}
	
	override public string ToString() {
		return string.Format("({0}, {1})", getX(), getY());
	}
		
	override public bool Equals(object other) {
		if (other?.GetType() != this.GetType()) 
			return false;
		var otherPt = other as Point;
		return (this.getX() == otherPt.getX() && this.getY() == otherPt.getY());
	}
}
