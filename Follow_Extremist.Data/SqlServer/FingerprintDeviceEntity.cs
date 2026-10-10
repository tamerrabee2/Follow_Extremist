using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Follow_Extremist.Core;

namespace Follow_Extremist.Data.SqlServer
{
    public class FingerprintDeviceEntity : IDataHelper<FingerprintDevice>
    {
        private DBContext db;
        private FingerprintDevice table;

        public FingerprintDeviceEntity()
        {
            db = new DBContext();
        }

        #region Methods
        public int Add(FingerprintDevice table)
        {
            try
            {
                if (db.Database.CanConnect())
                {
                    db.FingerprintDevice.Add(table);
                    db.SaveChanges();
                    return 1;
                }
                return 0;
            }
            catch
            {
                return 0;
            }
        }

        public async Task<int> AddAsync(FingerprintDevice table)
        {
            try
            {
                if (await db.Database.CanConnectAsync())
                {
                    await db.FingerprintDevice.AddAsync(table);
                    await db.SaveChangesAsync();
                    return 1;
                }
                return 0;
            }
            catch
            {
                return 0;
            }
        }

        public int Delete(int Id)
        {
            try
            {
                if (db.Database.CanConnect())
                {
                    table = Find(Id);
                    if (table != null)
                    {
                        db.FingerprintDevice.Remove(table);
                        db.SaveChanges();
                        return 1;
                    }
                }
                return 0;
            }
            catch
            {
                return 0;
            }
        }

        public async Task<int> DeleteAsync(int Id)
        {
            try
            {
                if (await db.Database.CanConnectAsync())
                {
                    table = await FindAsync(Id);
                    if (table != null)
                    {
                        db.FingerprintDevice.Remove(table);
                        await db.SaveChangesAsync();
                        return 1;
                    }
                }
                return 0;
            }
            catch
            {
                return 0;
            }
        }

        public int Edit(FingerprintDevice table)
        {
            try
            {
                if (db.Database.CanConnect())
                {
                    db = new DBContext();
                    db.FingerprintDevice.Update(table);
                    db.SaveChanges();
                    return 1;
                }
                return 0;
            }
            catch
            {
                return 0;
            }
        }

        public async Task<int> EditAsync(FingerprintDevice table)
        {
            try
            {
                if (await db.Database.CanConnectAsync())
                {
                    db = new DBContext();
                    await Task.Run(() => db.FingerprintDevice.Update(table));
                    await db.SaveChangesAsync();
                    return 1;
                }
                return 0;
            }
            catch
            {
                return 0;
            }
        }

        public FingerprintDevice Find(int Id)
        {
            try
            {
                if (db.Database.CanConnect())
                {
                    return db.FingerprintDevice.FirstOrDefault(x => x.Id == Id);
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        public async Task<FingerprintDevice> FindAsync(int Id)
        {
            try
            {
                if (await db.Database.CanConnectAsync())
                {
                    return await Task.Run(() => db.FingerprintDevice.FirstOrDefault(x => x.Id == Id));
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        public List<FingerprintDevice> GetAllData()
        {
            try
            {
                if (db.Database.CanConnect())
                {
                    return db.FingerprintDevice.ToList();
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<FingerprintDevice>> GetAllDataAsync()
        {
            try
            {
                if (await db.Database.CanConnectAsync())
                {
                    return await Task.Run(() => db.FingerprintDevice.ToList());
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        public List<FingerprintDevice> Search(string SearchItem)
        {
            try
            {
                if (db.Database.CanConnect())
                {
                    return db.FingerprintDevice.Where(x =>
                        x.Id.ToString() == SearchItem ||
                        x.DeviceName.Contains(SearchItem) ||
                        x.IpAddress.Contains(SearchItem) ||
                        x.Location.Contains(SearchItem) ||
                        x.Status.Contains(SearchItem)
                    ).ToList();
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<FingerprintDevice>> SearchAsync(string SearchItem)
        {
            try
            {
                if (await db.Database.CanConnectAsync())
                {
                    return await Task.Run(() => db.FingerprintDevice.Where(x =>
                        x.Id.ToString() == SearchItem ||
                        x.DeviceName.Contains(SearchItem) ||
                        x.IpAddress.Contains(SearchItem) ||
                        x.Location.Contains(SearchItem) ||
                        x.Status.Contains(SearchItem)
                    ).ToList());
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        public Task<List<FingerprintDevice>> GetFilteredDataAsync()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
