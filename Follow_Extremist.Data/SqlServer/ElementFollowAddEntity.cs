using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Follow_Extremist.Core;

namespace Follow_Extremist.Data.SqlServer
{
    public class ElementFollowAddEntity : IDataHelper<ElementFollowAdd>
    {
        // variables 
        private DBContext db;
        private ElementFollowAdd table;

        //Constructors 

        public ElementFollowAddEntity()
        {
            db = new DBContext();
        }
        #region Methods 
        public int Add(ElementFollowAdd table)
        {
            try
            {
                if (db.Database.CanConnect())
                {
                    db.ElementFollowAdd.Add(table);
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

        public async Task <int> AddAsync(ElementFollowAdd table)
        {
            try
            {
                if (await db.Database.CanConnectAsync())
                {
                     await db.ElementFollowAdd.AddAsync(table);
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
                    table = Find (Id);
                    db.ElementFollowAdd.Remove(table);
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
                    db.ElementFollowAdd.Remove(table);
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

        public int Edit (ElementFollowAdd table)
        {
            try
            {
                if (db.Database.CanConnect())
                {
                    db = new DBContext();
                    db.ElementFollowAdd.Update(table);
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

        public async Task<int> EditAsync(ElementFollowAdd table)
        {
            try
            {
                if (await db.Database.CanConnectAsync())
                {
                    db = new DBContext();
                    await Task.Run(() => db.ElementFollowAdd.Update(table));
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

        public async Task<ElementFollowAdd> FindAsync(int Id)
        {
            try
            {
                if (await db.Database.CanConnectAsync())
                {
                    return await Task.Run(() => db.ElementFollowAdd.Where(x => x.Id == Id).First());
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

        public ElementFollowAdd Find(int Id)
        {
            try
            {
                if (db.Database.CanConnect())
                {
                    return db.ElementFollowAdd.Where(x => x.Id == Id).First();
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

        public List<ElementFollowAdd> GetAllData()
        {
            try
            {
                if (db.Database.CanConnect())
                {
                    return db.ElementFollowAdd.ToList();
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

        public async Task<List<ElementFollowAdd>> GetAllDataAsync()
        {
            try
            {
                if (await db.Database.CanConnectAsync())
                {
                    return await Task.Run(() => db.ElementFollowAdd.ToList()); 
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

        public  List<ElementFollowAdd> Search(string SearchItem)
        {
            try
            {
                if (db.Database.CanConnect())
                {
                    return db.ElementFollowAdd.Where(x => x.Id.ToString() == SearchItem
                    || x.ElementName.Contains(SearchItem)
                    || x.DateFollow.Date.ToString().Contains(SearchItem)
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

        public async Task<List<ElementFollowAdd>> SearchAsync(string SearchItem)
        {
            try
            {
                if (await db.Database.CanConnectAsync())
                {
                    return db.ElementFollowAdd.Where(x => x.Id.ToString() == SearchItem
                    || x.ElementName.Contains(SearchItem)
                    || x.DateFollow.Date.ToString().Contains(SearchItem)
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

        public Task<List<ElementFollowAdd>> GetFilteredDataAsync()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
