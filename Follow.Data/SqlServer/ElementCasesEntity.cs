using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Follow.Core;

namespace Follow.Data.SqlServer
{
    public class ElementCasesEntity : IDataHelper<ElementCases>
    {
        // variables 
        private DBContext db;
        private ElementCases table;

        //Constructors 
        public ElementCasesEntity()
        {
            db = new DBContext();
        }

        #region Methods 
        public int Add(ElementCases table)
        {
            try
            {
                if (db.Database.CanConnect())
                {
                    db.ElementCases.Add(table);
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

        public async Task<int> AddAsync(ElementCases table)
        {
            try
            {
                if (await db.Database.CanConnectAsync())
                {
                    await db.ElementCases.AddAsync(table);
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
                    table = Find(Id);
                    db.ElementCases.Remove(table);
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

        public async Task<int> DeleteAsync(int Id)
        {
            try
            {
                if (await db.Database.CanConnectAsync())
                {
                    table = await FindAsync(Id);
                    db.ElementCases.Remove(table);
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

        public int Edit(ElementCases table)
        {
            try
            {
                if (db.Database.CanConnect())
                {
                    db = new DBContext();
                    db.ElementCases.Update(table);
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

        public async Task<int> EditAsync(ElementCases table)
        {
            try
            {
                if (await db.Database.CanConnectAsync())
                {
                    db = new DBContext();
                    await Task.Run(() => db.ElementCases.Update(table));
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

        public async Task<ElementCases> FindAsync(int Id)
        {
            try
            {
                if (await db.Database.CanConnectAsync())
                {
                    return await Task.Run(() => db.ElementCases.Where(x => x.ElementInfoId == Id).FirstOrDefault());
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

        public ElementCases Find(int Id)
        {
            try
            {
                if (db.Database.CanConnect())
                {
                    return db.ElementCases.Where(x => x.Id == Id).First();
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

        public List<ElementCases> GetAllData()
        {
            try
            {
                if (db.Database.CanConnect())
                {
                    return db.ElementCases.ToList();
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

        public async Task<List<ElementCases>> GetAllDataAsync()
        {
            try
            {
                if (await db.Database.CanConnectAsync())
                {
                    return await Task.Run(() => db.ElementCases.ToList());
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

        public List<ElementCases> Search(string SearchItem)
        {
            try
            {
                if (db.Database.CanConnect())
                {
                    return db.ElementCases.Where(x => x.Id.ToString() == SearchItem
                    || x.ElementName.Contains(SearchItem)
                    || x.CasesData.Contains(SearchItem)
                    ).ToList();
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

        public async Task<List<ElementCases>> SearchAsync(string SearchItem)
        {
            try
            {
                if (await db.Database.CanConnectAsync())
                {
                    return db.ElementCases.Where(x => x.Id.ToString() == SearchItem
                    || x.ElementName.Contains(SearchItem)
                    || x.CasesData.Contains(SearchItem)
                    ).ToList();
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

        public Task<List<ElementCases>> GetFilteredDataAsync()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
