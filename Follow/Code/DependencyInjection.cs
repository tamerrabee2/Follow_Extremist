using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Follow.Data.SqlServer;

namespace Follow.Code
{
  public static class DependencyInjection
    {
        public static void AddDependencyValues()
        {
            ConfigurationObjectManager.Register("Users", new UsersEntity());
            ConfigurationObjectManager.Register("UsersRoles", new UsersRolesEntity());
            ConfigurationObjectManager.Register("SystemRecords", new SystemRecordsEntity());
        }
    }
}
