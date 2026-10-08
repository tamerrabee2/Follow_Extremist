using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Follow.Core;
using Microsoft.EntityFrameworkCore;

namespace Follow.Data.SqlServer
{
    public class UsersEntity : IDataHelper<Users>
    {
        // variables 
        private DBContext db;
        private Users table;

        //Constructors 

        public UsersEntity()
        {
            db = new DBContext();
        }
        #region Methods 
        public int Add(Users table)
        {
            try
            {
                if (db.Database.CanConnect())
                {
                    db.Users.Add(table);
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

        public async Task <int> AddAsync(Users table)
        {
            try
            {
                if (await db.Database.CanConnectAsync())
                {
                     await db.Users.AddAsync(table);
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
                    db.Users.Remove(table);
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
                    db.Users.Remove(table);
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

        public int Edit (Users table)
        {
            try
            {
                if (db.Database.CanConnect())
                {
                    db = new DBContext();
                    db.Users.Update(table);
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

        public async Task<int> EditAsync(Users table)
        {
            try
            {
                if (await db.Database.CanConnectAsync())
                {
                    db = new DBContext();
                    await Task.Run(() => db.Users.Update(table));
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

        public async Task<Users> FindAsync(int Id)
        {
            try
            {
                if (await db.Database.CanConnectAsync())
                {
                    return await Task.Run(() => db.Users.Where(x => x.Id == Id).First());
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

        public Users Find(int Id)
        {
            try
            {
                if (db.Database.CanConnect())
                {
                    return db.Users.Where(x => x.Id == Id).First();
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

        public List<Users> GetAllData()
        {
            try
            {
                if (db.Database.CanConnect())
                {
                    return db.Users.ToList();
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

        public async Task<List<Users>> GetAllDataAsync()
        {
            try
            {
                if (await db.Database.CanConnectAsync())
                {
                    // Check if Users table exists
                    //var conn = db.Database.GetDbConnection();
                    //if (conn.State != System.Data.ConnectionState.Open)
                    //    await conn.OpenAsync();
                        
                    //using (var cmd = conn.CreateCommand())
                    //{
                    //    cmd.CommandText = "SELECT OBJECT_ID('Users', 'U')";
                    //    var result = await cmd.ExecuteScalarAsync();
                    //    Console.WriteLine($"Users table exists: {result != DBNull.Value}");
                    //}
                    
                    return await Task.Run(() => db.Users.ToList()); 
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

        public  List<Users> Search(string SearchItem)
        {
            try
            {
                if (db.Database.CanConnect())
                {
                    return db.Users.Where(x => x.Id.ToString() == SearchItem
                    || x.FullName.Contains(SearchItem)
                    || x.UserName.Contains(SearchItem)
                    || x.Password.Contains(SearchItem)
                    || x.Email.Contains(SearchItem)
                    || x.AddedDate.ToString().Contains(SearchItem)
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

        public async Task<List<Users>> SearchAsync(string SearchItem)
        {
            try
            {
                if (await db.Database.CanConnectAsync())
                {
                    return db.Users.Where(x => x.Id.ToString() == SearchItem
                    || x.FullName.Contains(SearchItem)
                    || x.UserName.Contains(SearchItem)
                    || x.Password.Contains(SearchItem)
                    || x.Phone.Contains(SearchItem)
                    || x.Email.Contains(SearchItem)
                    || x.AddedDate.Date.ToString().Contains(SearchItem)
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

        public Task<List<Users>> GetFilteredDataAsync()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
