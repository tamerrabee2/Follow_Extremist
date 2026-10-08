using Follow_Extremist.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Follow_Extremist.Data.SqlServer
{
  public  class ElementInfoViewEntity : IDataHelper<ElementInfoView>
    {
        // variables 
        private DBContext db;
        private ElementInfoView table;

        //Constructors 
        public ElementInfoViewEntity()
        {
            db = new DBContext();
        }



        #region Methods 
        public int Add(ElementInfoView table)
        {
            throw new NotImplementedException();
        }

        public Task<int> AddAsync(ElementInfoView table)
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

        public int Edit(ElementInfoView table)
        {
            throw new NotImplementedException();
        }

        public Task<int> EditAsync(ElementInfoView table)
        {
            throw new NotImplementedException();
        }

        public ElementInfoView Find(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<ElementInfoView> FindAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public List<ElementInfoView> GetAllData()
        {
            try
            {
                if (db.Database.CanConnect())
                {
                    return db.ElementInfoView.ToList();
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

        public async Task<List<ElementInfoView>> GetAllDataAsync()
        {
            try
            {
                if (await db.Database.CanConnectAsync())
                {
                    return await Task.Run(() => db.ElementInfoView.ToList());
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

        public Task<List<ElementInfoView>> GetFilteredDataAsync()
        {
            throw new NotImplementedException();
        }

        public List<ElementInfoView> Search(string SearchItem)
        {
            try
            {
                if (db.Database.CanConnect())
                {
                    return db.ElementInfoView.Where(x => 
                     x.ElementName.Contains(SearchItem)
                    || x.BirthDate.Date.ToString().Contains(SearchItem)
                    || x.Address.Contains(SearchItem)
                    || x.DateFollowNow.Date.ToString().Contains(SearchItem)
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

        public async Task<List<ElementInfoView>> SearchAsync(string SearchItem)
        {
            try
            {
                if (await db.Database.CanConnectAsync())
                {
                    return db.ElementInfoView.Where(x =>
                     x.ElementName.Contains(SearchItem)
                    || x.BirthDate.Date.ToString().Contains(SearchItem)
                    || x.Address.Contains(SearchItem)
                    || x.DateFollowNow.Date.ToString().Contains(SearchItem)
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
        #endregion

    }
}
