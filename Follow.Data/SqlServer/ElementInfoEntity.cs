using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Follow.Core;

namespace Follow.Data.SqlServer
{
    public class ElementInfoEntity : IDataHelper<ElementInfo>
    {
        // variables 
        private DBContext db;
        private ElementInfo table;

        //Constructors 

        public ElementInfoEntity()
        {
            db = new DBContext();
        }
        #region Methods 
        public int Add(ElementInfo table)
        {
            try
            {
                if (db.Database.CanConnect())
                {
                    db.ElementInfo.Add(table);
                    db.SaveChanges();
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            catch
            {

                return 0;
            }
        }

        public async Task <int> AddAsync(ElementInfo table)
        {
            try
            {
                if (await db.Database.CanConnectAsync())
                {
                     await db.ElementInfo.AddAsync(table);
                     await db.SaveChangesAsync();
                    return 1;
                }
                else
                {
                    return 0;
                }
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
                    table =Find(Id);
                    db.ElementInfo.Remove(table);
                    db.SaveChanges();
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            catch
            {

                return 0;
            }
        }

        public  async Task<int> DeleteAsync(int Id)
        {
            try
            {
                if (await db.Database.CanConnectAsync())
                {
                    table = await FindAsync(Id);
                    db.ElementInfo.Remove(table);
                    db.SaveChanges();
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            catch 
            {

                return 0;
            }
        }

        public int Edit (ElementInfo table)
        {
            try
            {
                if (db.Database.CanConnect())
                {
                    db = new DBContext();
                    db.ElementInfo.Update(table);
                    db.SaveChanges();
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            catch 
            {

                return 0;
            }
        }

        public async Task<int> EditAsync(ElementInfo table)
        {
            try
            {
                if (await db.Database.CanConnectAsync())
                {
                    db = new DBContext();
                    await Task.Run(() => db.ElementInfo.Update(table));
                    await db.SaveChangesAsync();
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            catch 
            {

                return 0;
            }
        }

        public async Task<ElementInfo> FindAsync(int Id)
        {
            try
            {
                if (await db.Database.CanConnectAsync())
                {
                    return await Task.Run(() => db.ElementInfo.Where(x => x.Id == Id).First());
                }
                else
                {
                    return null;
                }
            }
            catch 
            {

                return null;
            }
        }

        public ElementInfo Find(int Id)
        {
            try
            {
                if (db.Database.CanConnect())
                {
                    return db.ElementInfo.Where(x => x.Id == Id).First();
                }
                else
                {
                    return null;
                }
            }
            catch
            {

                return null;
            }
        }

        public List<ElementInfo> GetAllData()
        {
            try
            {
                if (db.Database.CanConnect())
                {
                    return db.ElementInfo.ToList();
                }
                else
                {
                    return null;
                }
            }
            catch 
            {

                return null;
            }
        }

        public async Task<List<ElementInfo>> GetAllDataAsync()
        {
            try
            {
                if (await db.Database.CanConnectAsync())
                {
                    return await Task.Run(() => db.ElementInfo.ToList()); 
                }
                else
                {
                    return null;
                }
            }
            catch 
            {

                return null;
            }
        }

        public  List<ElementInfo> Search(string SearchItem)
        {
            try
            {
                if (db.Database.CanConnect())
                {
                    DateTime searchDate;
                    bool isDate = DateTime.TryParseExact(SearchItem, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out searchDate);

                    var query = db.ElementInfo.AsQueryable();
                    if (isDate)
                    {
                        query = query.Where(x => x.DateFollowNow.Date == searchDate
                                         || x.DateFollowStart.Date == searchDate
                                         || x.DateFollowNext.Date == searchDate);
                    }
                    else
                    {
                        query = query.Where(x => x.Id.ToString() == SearchItem
                                         || x.ElementName.Contains(SearchItem)
                                         || x.Job.Contains(SearchItem)
                                         || x.Mobile.Contains(SearchItem)
                                         || x.Mobile2.Contains(SearchItem)
                                         || x.MotherName.Contains(SearchItem)
                                         || x.NationalId.Contains(SearchItem)
                                         || x.Qualification.Contains(SearchItem)
                                         || x.FacebookAcount.Contains(SearchItem)
                                         || x.FacebookID.Contains(SearchItem)
                                         || x.RegulatoryStatus.Contains(SearchItem)
                                         || x.PrisonedOrnot.Contains(SearchItem));
                    }
                    var results = query.ToList();
                    Debug.WriteLine($"SearchItem: {SearchItem}, Results Count: {results.Count}");
                    return results;

                }
                else
                {
                    Debug.WriteLine("Database connection failed.");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Search method exception: {ex.Message}");
                return null;
            }
        }

        public async Task<List<ElementInfo>> SearchAsync(string SearchItem)
        {
            try
            {
                if (await db.Database.CanConnectAsync())
                {
                    DateTime searchDate;
                    bool isDate = DateTime.TryParseExact(SearchItem, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out searchDate);

                    var query = db.ElementInfo.AsQueryable();
                    if (isDate)
                    {
                        query = query.Where(x => x.DateFollowNow.Date == searchDate
                                         || x.DateFollowStart.Date == searchDate
                                         || x.DateFollowNext.Date == searchDate);
                    }
                    else
                    {
                        query = query.Where(x => x.Id.ToString() == SearchItem
                                         || x.ElementName.Contains(SearchItem)
                                         || x.Job.Contains(SearchItem)
                                         || x.Mobile.Contains(SearchItem)
                                         || x.Mobile2.Contains(SearchItem)
                                         || x.MotherName.Contains(SearchItem)
                                         || x.NationalId.Contains(SearchItem)
                                         || x.Qualification.Contains(SearchItem)
                                         || x.FacebookAcount.Contains(SearchItem)
                                         || x.FacebookID.Contains(SearchItem)
                                         || x.RegulatoryStatus.Contains(SearchItem)
                                         || x.PrisonedOrnot.Contains(SearchItem));
                    }
                    var results = query.ToList();
                    Debug.WriteLine($"SearchItem: {SearchItem}, Results Count: {results.Count}");
                    return results;
                    

                }
                else
                {
                    Debug.WriteLine("Database connection failed.");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Search method exception: {ex.Message}");

                return null;
            }
        }

        public async Task<List<ElementInfo>> GetFilteredDataAsync()
        {
            try
            {
                if (await db.Database.CanConnectAsync())
                {
                    return db.ElementInfo.Select(x => new ElementInfo
                    {
                        Id = x.Id,
                        ElementName = x.ElementName,
                        NationalId = x.NationalId,
                        DateFollowStart = x.DateFollowStart,
                        FollowDaysCount = x.FollowDaysCount,
                        DateFollowNow = x.DateFollowNow,
                        DateFollowNext = x.DateFollowNext
                    }).ToList();
                }
                else
                {
                    return null;
                }
            }
            catch 
            {

                return null;
            }
        }
        #endregion
    }
}
