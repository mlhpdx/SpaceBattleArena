using System.Collections.Generic;

public class StringStringMap : StringMap<string> {
	private static long serialVersionUID = 279650296598224037L;

	public StringStringMap() { }

	public StringStringMap(int capacity) : base(capacity) { }

	public StringStringMap(IDictionary<string, string> other) : base(other) { }

	public StringStringMap(int capacity, float ft) : base(capacity) { }
}
