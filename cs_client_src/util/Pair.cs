public class Pair<T, U> {
	private T first;
	private U second;
	
	public Pair(T first, U second) {
		this.first = first;
		this.second = second;
	}
	
	public T getFirst()  { return first; }
	public U getSecond() { return second; }
	
	override public string ToString() {
		return string.Format("({0}, {1})", first.ToString(), second.ToString());
	}
}
