using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Follow_Extremist.Data.SqlServer;

namespace Follow_Extremist.Code
{
  public static class DependencyInjection
    {
        public static void AddDependencyValues()
        {
            ConfigurationObjectManager.Register("ElementInfo", new ElementInfoEntity());
            ConfigurationObjectManager.Register("Users", new UsersEntity());
            ConfigurationObjectManager.Register("UsersRoles", new UsersRolesEntity());
            ConfigurationObjectManager.Register("SystemRecords", new SystemRecordsEntity());
            ConfigurationObjectManager.Register("ElementAddInfo", new ElementAddInfoEntity());
            ConfigurationObjectManager.Register("ElementFollowAdd", new ElementFollowAddEntity());
            ConfigurationObjectManager.Register("ElementCases", new ElementCasesEntity());
            ConfigurationObjectManager.Register("ElementInfoView", new ElementInfoViewEntity());
            ConfigurationObjectManager.Register("FingerprintDevice", new FingerprintDeviceEntity());
            ConfigurationObjectManager.Register("ElementFingerprint", new ElementFingerprintEntity());
            ConfigurationObjectManager.Register("AttendanceLog", new AttendanceLogEntity());
            ConfigurationObjectManager.Register("ElementWantedStatus", new ElementWantedStatusEntity());
            ConfigurationObjectManager.Register("PrintSetting", new PrintSettingEntity());
            ConfigurationObjectManager.Register("TodayAttendanceView", new TodayAttendanceViewEntity());
        }
    }
}
