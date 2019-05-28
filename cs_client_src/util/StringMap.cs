using System.Collections.Generic;

namespace SpaceBattleArena
{
    public class StringMap<T> : Dictionary<string, T>
    {
        private static long serialVersionUID = 6086952182878503256L;

        public StringMap()
        {
        }

        public StringMap(int capacity) : base(capacity)
        {
        }

        public StringMap(IDictionary<string, T> other) : base(other)
        {
        }

        public StringMap(int capacity, float lf) : base(capacity)
        {
        }
    }
}