using System;
using System.Collections.Generic;
using System.Reflection;
namespace SpaceBattleArena
{
    public abstract class ShipCommand
    {
        public abstract string getName();

        public static int getInitialEnergyCost() { return 0; }

        public static int getOngoingEnergyCost() { return 0; }

        public static bool isBlocking() { return true; }

        public static bool executesImmediately() { return false; }

        public List<object> getMessage()
        {
            List<object> list = new List<object>();
            list.Add(getName());
            var args = getArgs();
            if (args?.Count > 0)
                list.Add(getArgs());
            return list;
        }

        protected StringMap<object> getArgs()
        {
            StringMap<object> map = new StringMap<object>();
            foreach (FieldInfo f in GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic))
            {
                object val = f.GetValue(this);
                if (val != null)
                    map.Add(f.Name, val);
            }
            return map;
        }

        override public bool Equals(object other)
        {
            if (other?.GetType() != this.GetType())
                return false;

            // commands are equal if all of their fields are equal
            try
            {
                foreach (FieldInfo f in GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic))
                {
                    // f.setAccessible(true);
                    object thisVal = f.GetValue(this);
                    object otherVal = f.GetValue(other);
                    if (!thisVal.Equals(otherVal))
                        return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        override public string ToString()
        {
            try
            {
                return string.Format("{0}[{1}]", getName(), getArgs());
            }
            catch (Exception ex)
            {
                return getName();
            }
        }
    }
}