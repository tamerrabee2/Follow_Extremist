using System;
using System.Collections.Generic;

namespace Follow.Code
{
    public static class ConfigurationObjectManager
    {
        private static readonly Dictionary<string, object> ObjectList = new Dictionary<string, object>();

        public static void Register(string ObjectName, object ObjectValue)
        {
            ObjectList[ObjectName] = ObjectValue;
        }

        public static object GetObject(string ObjectName)
        {
            if (ObjectList.TryGetValue(ObjectName, out var value))
            {
                return value;
            }

            // Fallback for Design Mode (Visual Studio Designer) or uninitialized state
            try
            {
                if (ObjectList.Count == 0)
                {
                    DependencyInjection.AddDependencyValues();
                }
            }
            catch
            {
                // Suppress design-time instantiation errors
            }

            if (ObjectList.TryGetValue(ObjectName, out value))
            {
                return value;
            }

            return null;
        }
    }
}
