using System;
using System.Collections.Generic;

namespace Follow.Code
{
    public static class UsersRolesManager
    {
        private static readonly Dictionary<string, bool> RolesList = new Dictionary<string, bool>();

        public static void Register(string RoleKey, bool RoleValue)
        {
            RolesList[RoleKey] = RoleValue;
        }

        public static bool GetRole(string RoleKey)
        {
            if (RolesList.TryGetValue(RoleKey, out bool value))
            {
                return value;
            }
            return false;
        }

        public static void ClearRoles()
        {
            RolesList.Clear();
        }
    }
}
