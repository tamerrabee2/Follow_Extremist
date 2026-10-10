using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Follow_Extremist.Core;

namespace Follow_Extremist.Data.SqlServer
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

        public List<ElementInfo> Search(string SearchItem)
        {
            try
            {
                db = new DBContext();
                if (db.Database.CanConnect())
                {
                    if (string.IsNullOrWhiteSpace(SearchItem))
                    {
                        return db.ElementInfo.ToList();
                    }

                    SearchItem = SearchItem.Trim();
                    bool isDate = DateTime.TryParse(SearchItem, out DateTime searchDate);
                    bool isInt = int.TryParse(SearchItem, out int searchId);
                    bool isYear = isInt && searchId >= 1900 && searchId <= 2100;

                    var query = db.ElementInfo.AsQueryable();

                    query = query.Where(x =>
                        (isInt && x.Id == searchId)
                        || (isYear && x.BirthDate.Year == searchId)
                        || x.Id.ToString().Contains(SearchItem)
                        || (x.ElementName != null && x.ElementName.Contains(SearchItem))
                        || (x.NationalId != null && x.NationalId.Contains(SearchItem))
                        || (x.MotherName != null && x.MotherName.Contains(SearchItem))
                        || (x.Job != null && x.Job.Contains(SearchItem))
                        || (x.Qualification != null && x.Qualification.Contains(SearchItem))
                        || (x.Address != null && x.Address.Contains(SearchItem))
                        || (x.BirthPlace != null && x.BirthPlace.Contains(SearchItem))
                        || (x.Phone != null && x.Phone.Contains(SearchItem))
                        || (x.Mobile != null && x.Mobile.Contains(SearchItem))
                        || (x.Mobile2 != null && x.Mobile2.Contains(SearchItem))
                        || (x.Mobile3 != null && x.Mobile3.Contains(SearchItem))
                        || (x.FollowState != null && x.FollowState.Contains(SearchItem))
                        || (x.ReasonEndFollow != null && x.ReasonEndFollow.Contains(SearchItem))
                        || (x.Notes != null && x.Notes.Contains(SearchItem))
                        || (x.RegulatoryStatus != null && x.RegulatoryStatus.Contains(SearchItem))
                        || (x.FacebookAcount != null && x.FacebookAcount.Contains(SearchItem))
                        || (x.FacebookID != null && x.FacebookID.Contains(SearchItem))
                        || (x.PrisonedOrnot != null && x.PrisonedOrnot.Contains(SearchItem))
                        || (x.CaseData != null && x.CaseData.Contains(SearchItem))
                        || (isDate && (x.DateFollowNow.Date == searchDate.Date
                                    || x.DateFollowStart.Date == searchDate.Date
                                    || x.DateFollowNext.Date == searchDate.Date
                                    || x.BirthDate.Date == searchDate.Date))
                        || (x.ElementCases != null && x.ElementCases.Any(c =>
                               (c.CasesData != null && c.CasesData.Contains(SearchItem))
                            || (c.ElementStateJailOrNot != null && c.ElementStateJailOrNot.Contains(SearchItem))
                            || (c.ElementName != null && c.ElementName.Contains(SearchItem))))
                        || (x.ElementAddInfo != null && x.ElementAddInfo.Any(a =>
                               (a.NameRelationElement != null && a.NameRelationElement.Contains(SearchItem))
                            || (a.Relationship != null && a.Relationship.Contains(SearchItem))
                            || (a.ElementRelationNationalID != null && a.ElementRelationNationalID.Contains(SearchItem))))
                    );

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
                db = new DBContext();
                if (await db.Database.CanConnectAsync())
                {
                    if (string.IsNullOrWhiteSpace(SearchItem))
                    {
                        return await Task.Run(() => db.ElementInfo.ToList());
                    }

                    SearchItem = SearchItem.Trim();
                    bool isDate = DateTime.TryParse(SearchItem, out DateTime searchDate);
                    bool isInt = int.TryParse(SearchItem, out int searchId);
                    bool isYear = isInt && searchId >= 1900 && searchId <= 2100;

                    var query = db.ElementInfo.AsQueryable();

                    query = query.Where(x =>
                        (isInt && x.Id == searchId)
                        || (isYear && x.BirthDate.Year == searchId)
                        || x.Id.ToString().Contains(SearchItem)
                        || (x.ElementName != null && x.ElementName.Contains(SearchItem))
                        || (x.NationalId != null && x.NationalId.Contains(SearchItem))
                        || (x.MotherName != null && x.MotherName.Contains(SearchItem))
                        || (x.Job != null && x.Job.Contains(SearchItem))
                        || (x.Qualification != null && x.Qualification.Contains(SearchItem))
                        || (x.Address != null && x.Address.Contains(SearchItem))
                        || (x.BirthPlace != null && x.BirthPlace.Contains(SearchItem))
                        || (x.Phone != null && x.Phone.Contains(SearchItem))
                        || (x.Mobile != null && x.Mobile.Contains(SearchItem))
                        || (x.Mobile2 != null && x.Mobile2.Contains(SearchItem))
                        || (x.Mobile3 != null && x.Mobile3.Contains(SearchItem))
                        || (x.FollowState != null && x.FollowState.Contains(SearchItem))
                        || (x.ReasonEndFollow != null && x.ReasonEndFollow.Contains(SearchItem))
                        || (x.Notes != null && x.Notes.Contains(SearchItem))
                        || (x.RegulatoryStatus != null && x.RegulatoryStatus.Contains(SearchItem))
                        || (x.FacebookAcount != null && x.FacebookAcount.Contains(SearchItem))
                        || (x.FacebookID != null && x.FacebookID.Contains(SearchItem))
                        || (x.PrisonedOrnot != null && x.PrisonedOrnot.Contains(SearchItem))
                        || (x.CaseData != null && x.CaseData.Contains(SearchItem))
                        || (isDate && (x.DateFollowNow.Date == searchDate.Date
                                    || x.DateFollowStart.Date == searchDate.Date
                                    || x.DateFollowNext.Date == searchDate.Date
                                    || x.BirthDate.Date == searchDate.Date))
                        || (x.ElementCases != null && x.ElementCases.Any(c =>
                               (c.CasesData != null && c.CasesData.Contains(SearchItem))
                            || (c.ElementStateJailOrNot != null && c.ElementStateJailOrNot.Contains(SearchItem))
                            || (c.ElementName != null && c.ElementName.Contains(SearchItem))))
                        || (x.ElementAddInfo != null && x.ElementAddInfo.Any(a =>
                               (a.NameRelationElement != null && a.NameRelationElement.Contains(SearchItem))
                            || (a.Relationship != null && a.Relationship.Contains(SearchItem))
                            || (a.ElementRelationNationalID != null && a.ElementRelationNationalID.Contains(SearchItem))))
                    );

                    var results = await Task.Run(() => query.ToList());
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

        public async Task<List<ElementInfo>> AdvancedSearchAsync(ElementSearchCriteria criteria)
        {
            try
            {
                db = new DBContext();
                if (await db.Database.CanConnectAsync())
                {
                    var query = db.ElementInfo.AsQueryable();

                    if (criteria != null)
                    {
                        if (!string.IsNullOrWhiteSpace(criteria.ElementName))
                        {
                            string val = criteria.ElementName.Trim();
                            query = query.Where(x => x.ElementName != null && x.ElementName.Contains(val));
                        }

                        if (!string.IsNullOrWhiteSpace(criteria.NationalId))
                        {
                            string val = criteria.NationalId.Trim();
                            query = query.Where(x => x.NationalId != null && x.NationalId.Contains(val));
                        }

                        if (!string.IsNullOrWhiteSpace(criteria.MotherName))
                        {
                            string val = criteria.MotherName.Trim();
                            query = query.Where(x => x.MotherName != null && x.MotherName.Contains(val));
                        }

                        if (!string.IsNullOrWhiteSpace(criteria.PhoneOrMobile))
                        {
                            string val = criteria.PhoneOrMobile.Trim();
                            query = query.Where(x => (x.Phone != null && x.Phone.Contains(val))
                                                  || (x.Mobile != null && x.Mobile.Contains(val))
                                                  || (x.Mobile2 != null && x.Mobile2.Contains(val))
                                                  || (x.Mobile3 != null && x.Mobile3.Contains(val)));
                        }

                        if (!string.IsNullOrWhiteSpace(criteria.Address))
                        {
                            string val = criteria.Address.Trim();
                            query = query.Where(x => x.Address != null && x.Address.Contains(val));
                        }

                        if (!string.IsNullOrWhiteSpace(criteria.BirthPlace))
                        {
                            string val = criteria.BirthPlace.Trim();
                            query = query.Where(x => x.BirthPlace != null && x.BirthPlace.Contains(val));
                        }

                        if (!string.IsNullOrWhiteSpace(criteria.Job))
                        {
                            string val = criteria.Job.Trim();
                            query = query.Where(x => x.Job != null && x.Job.Contains(val));
                        }

                        if (!string.IsNullOrWhiteSpace(criteria.Qualification))
                        {
                            string val = criteria.Qualification.Trim();
                            query = query.Where(x => x.Qualification != null && x.Qualification.Contains(val));
                        }

                        if (!string.IsNullOrWhiteSpace(criteria.RegulatoryStatus))
                        {
                            string val = criteria.RegulatoryStatus.Trim();
                            query = query.Where(x => x.RegulatoryStatus != null && x.RegulatoryStatus.Contains(val));
                        }

                        if (!string.IsNullOrWhiteSpace(criteria.PrisonedOrnot) && criteria.PrisonedOrnot != "الكل")
                        {
                            string val = criteria.PrisonedOrnot.Trim();
                            query = query.Where(x => x.PrisonedOrnot != null && x.PrisonedOrnot == val);
                        }

                        if (!string.IsNullOrWhiteSpace(criteria.FollowState) && criteria.FollowState != "الكل")
                        {
                            string val = criteria.FollowState.Trim();
                            query = query.Where(x => x.FollowState != null && (x.FollowState.Trim() == val || x.FollowState.Contains(val)));
                        }

                        if (!string.IsNullOrWhiteSpace(criteria.CaseData))
                        {
                            string val = criteria.CaseData.Trim();
                            query = query.Where(x => (x.CaseData != null && x.CaseData.Contains(val))
                                                  || (x.ElementCases != null && x.ElementCases.Any(c => c.CasesData != null && c.CasesData.Contains(val))));
                        }

                        if (!string.IsNullOrWhiteSpace(criteria.Notes))
                        {
                            string val = criteria.Notes.Trim();
                            query = query.Where(x => x.Notes != null && x.Notes.Contains(val));
                        }

                        if (criteria.UseBirthDateFilter && criteria.BirthDateFrom.HasValue && criteria.BirthDateTo.HasValue)
                        {
                            DateTime bFrom = criteria.BirthDateFrom.Value.Date;
                            DateTime bTo = criteria.BirthDateTo.Value.Date;
                            query = query.Where(x => x.BirthDate.Date >= bFrom && x.BirthDate.Date <= bTo);
                        }
                        else if (criteria.BirthYear.HasValue && criteria.BirthYear.Value >= 1900 && criteria.BirthYear.Value <= 2100)
                        {
                            int bYear = criteria.BirthYear.Value;
                            query = query.Where(x => x.BirthDate.Year == bYear);
                        }

                        if (criteria.UseDateFilter && criteria.DateFollowFrom.HasValue && criteria.DateFollowTo.HasValue)
                        {
                            DateTime from = criteria.DateFollowFrom.Value.Date;
                            DateTime to = criteria.DateFollowTo.Value.Date;
                            query = query.Where(x => x.DateFollowNow.Date >= from && x.DateFollowNow.Date <= to);
                        }
                    }

                    var results = await Task.Run(() => query.ToList());
                    Debug.WriteLine($"AdvancedSearch Results Count: {results.Count}");
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
                Debug.WriteLine($"AdvancedSearchAsync exception: {ex.Message}");
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
