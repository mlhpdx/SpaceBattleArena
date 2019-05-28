public class Triple<T, U, V> {
	private T first;
	private U second;
	private V third;
	
	public Triple(T first, U second, V third) {
		this.first = first;
		this.second = second;
		this.third = third;
	}
	
	public T getFirst()  { return first; }
	public U getSecond() { return second; }
	public V getThird() { return third; }
	
	override public string ToString() {
		return string.Format("({0}, {1}, {2})", first.ToString(), second.ToString(), third.ToString());
	}
}
