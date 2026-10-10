using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Follow_Extremist.Core;
using Microsoft.EntityFrameworkCore;

namespace Follow_Extremist.Data.SqlServer
{
    public class ElementWantedStatusEntity : IDataHelper<ElementWantedStatus>
    {
        private DBContext db;
        private ElementWantedStatus table;

        public ElementWantedStatusEntity()
        {
            db = new DBContext();
        }

        #region Methods
        public int Add(ElementWantedStatus table)
        {
            try
            {
                if (db.Database.CanConnect())
                {
                    db.ElementWantedStatus.Add(table);
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

        public async Task<int> AddAsync(ElementWantedStatus table)
        {
            try
            {
                if (await db.Database.CanConnectAsync())
                {
                    await db.ElementWantedStatus.AddAsync(table);
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
                        db.ElementWantedStatus.Remove(table);
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
                        db.ElementWantedStatus.Remove(table);
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

        public int Edit(ElementWantedStatus table)
        {
            try
            {
                if (db.Database.CanConnect())
                {
                    db = new DBContext();
                    db.ElementWantedStatus.Update(table);
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

        public async Task<int> EditAsync(ElementWantedStatus table)
        {
            try
            {
                if (await db.Database.CanConnectAsync())
                {
                    db = new DBContext();
                    await Task.Run(() => db.ElementWantedStatus.Update(table));
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

        public ElementWantedStatus Find(int Id)
        {
            try
            {
                if (db.Database.CanConnect())
                {
                    return db.ElementWantedStatus.Include(x => x.ElementInfo).FirstOrDefault(x => x.Id == Id);
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        public async Task<ElementWantedStatus> FindAsync(int Id)
        {
            try
            {
                if (await db.Database.CanConnectAsync())
                {
                    return await db.ElementWantedStatus.Include(x => x.ElementInfo).FirstOrDefaultAsync(x => x.Id == Id);
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        public List<ElementWantedStatus> GetAllData()
        {
            try
            {
                if (db.Database.CanConnect())
                {
                    return db.ElementWantedStatus.Include(x => x.ElementInfo).ToList();
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<ElementWantedStatus>> GetAllDataAsync()
        {
            try
            {
                if (await db.Database.CanConnectAsync())
                {
                    return await db.ElementWantedStatus.Include(x => x.ElementInfo).ToListAsync();
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        public List<ElementWantedStatus> Search(string SearchItem)
        {
            try
            {
                if (db.Database.CanConnect())
                {
                    return db.ElementWantedStatus.Include(x => x.ElementInfo).Where(x =>
                        x.Id.ToString() == SearchItem ||
                        x.ElementId.ToString() == SearchItem ||
                        x.WantedReason.Contains(SearchItem) ||
                        x.WantedBy.Contains(SearchItem) ||
                        (x.ElementInfo != null && (x.ElementInfo.ElementName.Contains(SearchItem) || x.ElementInfo.NationalId.Contains(SearchItem)))
                    ).ToList();
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<ElementWantedStatus>> SearchAsync(string SearchItem)
        {
            try
            {
                if (await db.Database.CanConnectAsync())
                {
                    return await db.ElementWantedStatus.Include(x => x.ElementInfo).Where(x =>
                        x.Id.ToString() == SearchItem ||
                        x.ElementId.ToString() == SearchItem ||
                        x.WantedReason.Contains(SearchItem) ||
                        x.WantedBy.Contains(SearchItem) ||
                        (x.ElementInfo != null && (x.ElementInfo.ElementName.Contains(SearchItem) || x.ElementInfo.NationalId.Contains(SearchItem)))
                    ).ToListAsync();
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        public Task<List<ElementWantedStatus>> GetFilteredDataAsync()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
