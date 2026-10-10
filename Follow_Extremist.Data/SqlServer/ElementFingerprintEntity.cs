using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Follow_Extremist.Core;
using Microsoft.EntityFrameworkCore;

namespace Follow_Extremist.Data.SqlServer
{
    public class ElementFingerprintEntity : IDataHelper<ElementFingerprint>
    {
        private DBContext db;
        private ElementFingerprint table;

        public ElementFingerprintEntity()
        {
            db = new DBContext();
        }

        #region Methods
        public int Add(ElementFingerprint table)
        {
            try
            {
                if (db.Database.CanConnect())
                {
                    db.ElementFingerprint.Add(table);
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

        public async Task<int> AddAsync(ElementFingerprint table)
        {
            try
            {
                if (await db.Database.CanConnectAsync())
                {
                    await db.ElementFingerprint.AddAsync(table);
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
                        db.ElementFingerprint.Remove(table);
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
                        db.ElementFingerprint.Remove(table);
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

        public int Edit(ElementFingerprint table)
        {
            try
            {
                if (db.Database.CanConnect())
                {
                    db = new DBContext();
                    db.ElementFingerprint.Update(table);
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

        public async Task<int> EditAsync(ElementFingerprint table)
        {
            try
            {
                if (await db.Database.CanConnectAsync())
                {
                    db = new DBContext();
                    await Task.Run(() => db.ElementFingerprint.Update(table));
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

        public ElementFingerprint Find(int Id)
        {
            try
            {
                if (db.Database.CanConnect())
                {
                    return db.ElementFingerprint.Include(x => x.ElementInfo).FirstOrDefault(x => x.Id == Id);
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        public async Task<ElementFingerprint> FindAsync(int Id)
        {
            try
            {
                if (await db.Database.CanConnectAsync())
                {
                    return await db.ElementFingerprint.Include(x => x.ElementInfo).FirstOrDefaultAsync(x => x.Id == Id);
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        public List<ElementFingerprint> GetAllData()
        {
            try
            {
                if (db.Database.CanConnect())
                {
                    return db.ElementFingerprint.Include(x => x.ElementInfo).ToList();
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<ElementFingerprint>> GetAllDataAsync()
        {
            try
            {
                if (await db.Database.CanConnectAsync())
                {
                    return await db.ElementFingerprint.Include(x => x.ElementInfo).ToListAsync();
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        public List<ElementFingerprint> Search(string SearchItem)
        {
            try
            {
                if (db.Database.CanConnect())
                {
                    return db.ElementFingerprint.Include(x => x.ElementInfo).Where(x =>
                        x.Id.ToString() == SearchItem ||
                        x.ElementId.ToString() == SearchItem ||
                        x.FingerName.Contains(SearchItem) ||
                        (x.ElementInfo != null && x.ElementInfo.ElementName.Contains(SearchItem))
                    ).ToList();
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<ElementFingerprint>> SearchAsync(string SearchItem)
        {
            try
            {
                if (await db.Database.CanConnectAsync())
                {
                    return await db.ElementFingerprint.Include(x => x.ElementInfo).Where(x =>
                        x.Id.ToString() == SearchItem ||
                        x.ElementId.ToString() == SearchItem ||
                        x.FingerName.Contains(SearchItem) ||
                        (x.ElementInfo != null && x.ElementInfo.ElementName.Contains(SearchItem))
                    ).ToListAsync();
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        public Task<List<ElementFingerprint>> GetFilteredDataAsync()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
