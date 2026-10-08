using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Follow_Extremist.Core;

namespace Follow_Extremist.Data.SqlServer
{
    public class ElementAddInfoEntity : IDataHelper<ElementAddInfo>
    {
        // variables 
        private DBContext db;
        private ElementAddInfo table;

        //Constructors 

        public ElementAddInfoEntity()
        {
            db = new DBContext();
        }
        #region Methods 
        public int Add(ElementAddInfo table)
        {
            try
            {
                if (db.Database.CanConnect())
                {
                    db.ElementAddInfo.Add(table);
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

        public async Task <int> AddAsync(ElementAddInfo table)
        {
            try
            {
                if (await db.Database.CanConnectAsync())
                {
                     await db.ElementAddInfo.AddAsync(table);
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
                    db.ElementAddInfo.Remove(table);
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
                    db.ElementAddInfo.Remove(table);
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

        public int Edit (ElementAddInfo table)
        {
            try
            {
                if (db.Database.CanConnect())
                {
                    db = new DBContext();
                    db.ElementAddInfo.Update(table);
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

        public async Task<int> EditAsync(ElementAddInfo table)
        {
            try
            {
                if (await db.Database.CanConnectAsync())
                {
                    db = new DBContext();
                    await Task.Run(() => db.ElementAddInfo.Update(table));
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

        public async Task<ElementAddInfo> FindAsync(int Id)
        {
            try
            {
                if (await db.Database.CanConnectAsync())
                {
                    return await Task.Run(() => db.ElementAddInfo.Where(x => x.Id == Id).First());
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

        public ElementAddInfo Find(int Id)
        {
            try
            {
                if (db.Database.CanConnect())
                {
                    return db.ElementAddInfo.Where(x => x.Id == Id).First();
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

        public List<ElementAddInfo> GetAllData()
        {
            try
            {
                if (db.Database.CanConnect())
                {
                    return db.ElementAddInfo.ToList();
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

        public async Task<List<ElementAddInfo>> GetAllDataAsync()
        {
            try
            {
                if (await db.Database.CanConnectAsync())
                {
                    return await Task.Run(() => db.ElementAddInfo.ToList()); 
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

        public  List<ElementAddInfo> Search(string SearchItem)
        {
            try
            {
                if (db.Database.CanConnect())
                {
                    return db.ElementAddInfo.Where(x => x.Id.ToString() == SearchItem
                    || x.NameRelationElement.Contains(SearchItem)
                    || x.ElementRelationNationalID.Contains(SearchItem)
                    || x.Relationship.Contains(SearchItem)
                    || x.Age.Contains(SearchItem)
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

        public async Task<List<ElementAddInfo>> SearchAsync(string SearchItem)
        {
            try
            {
                if (await db.Database.CanConnectAsync())
                {
                    return db.ElementAddInfo.Where(x => x.Id.ToString() == SearchItem
                    || x.NameRelationElement.Contains(SearchItem)
                    || x.ElementRelationNationalID.Contains(SearchItem)
                    || x.Relationship.Contains(SearchItem)
                    || x.Age.Contains(SearchItem)
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

        public Task<List<ElementAddInfo>> GetFilteredDataAsync()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
