using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Follow_Extremist.Core;
using Microsoft.EntityFrameworkCore;

namespace Follow_Extremist.Data.SqlServer
{
    public class TodayAttendanceViewEntity : IDataHelper<TodayAttendanceView>
    {
        private DBContext db;

        public TodayAttendanceViewEntity()
        {
            db = new DBContext();
        }

        #region Methods
        public int Add(TodayAttendanceView table)
        {
            throw new NotImplementedException();
        }

        public Task<int> AddAsync(TodayAttendanceView table)
        {
            throw new NotImplementedException();
        }

        public int Delete(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public int Edit(TodayAttendanceView table)
        {
            throw new NotImplementedException();
        }

        public Task<int> EditAsync(TodayAttendanceView table)
        {
            throw new NotImplementedException();
        }

        public TodayAttendanceView Find(int Id)
        {
            try
            {
                if (db.Database.CanConnect())
                {
                    return db.TodayAttendanceView.FirstOrDefault(x => x.AttendanceLogId == Id);
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        public async Task<TodayAttendanceView> FindAsync(int Id)
        {
            try
            {
                if (await db.Database.CanConnectAsync())
                {
                    return await db.TodayAttendanceView.FirstOrDefaultAsync(x => x.AttendanceLogId == Id);
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        public List<TodayAttendanceView> GetAllData()
        {
            try
            {
                if (db.Database.CanConnect())
                {
                    return db.TodayAttendanceView
                        .OrderByDescending(x => x.AttendanceDateTime)
                        .ToList();
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<TodayAttendanceView>> GetAllDataAsync()
        {
            try
            {
                if (await db.Database.CanConnectAsync())
                {
                    return await db.TodayAttendanceView
                        .OrderByDescending(x => x.AttendanceDateTime)
                        .ToListAsync();
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        public Task<List<TodayAttendanceView>> GetFilteredDataAsync()
        {
            throw new NotImplementedException();
        }

        public List<TodayAttendanceView> Search(string SearchItem)
        {
            try
            {
                if (db.Database.CanConnect())
                {
                    return db.TodayAttendanceView.Where(x =>
                        x.ElementName.Contains(SearchItem) ||
                        x.NationalId.Contains(SearchItem) ||
                        x.DeviceName.Contains(SearchItem) ||
                        x.Status.Contains(SearchItem)
                    ).OrderByDescending(x => x.AttendanceDateTime).ToList();
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<TodayAttendanceView>> SearchAsync(string SearchItem)
        {
            try
            {
                if (await db.Database.CanConnectAsync())
                {
                    return await db.TodayAttendanceView.Where(x =>
                        x.ElementName.Contains(SearchItem) ||
                        x.NationalId.Contains(SearchItem) ||
                        x.DeviceName.Contains(SearchItem) ||
                        x.Status.Contains(SearchItem)
                    ).OrderByDescending(x => x.AttendanceDateTime).ToListAsync();
                }
                return null;
            }
            catch
            {
                return null;
            }
        }
        #endregion
    }
}
