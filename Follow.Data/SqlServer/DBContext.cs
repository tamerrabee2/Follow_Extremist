using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Follow.Core;
using Microsoft.EntityFrameworkCore;

namespace Follow.Data.SqlServer
{
    public class DBContext : DbContext
    {
        public DBContext()
        {

        }
        // set connection 
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //var sqlcon = @"Server=.;Database=FollowDataBase;Trusted_Connection=True;";
            if (!string.IsNullOrEmpty(SqlCon.SqlConnection))
            {
                optionsBuilder.UseSqlServer(SqlCon.SqlConnection);
            }
        }

        // Tables 
        public DbSet<SystemRecords> SystemRecords { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<UsersRoles> UsersRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Users>().ToTable("Users");
            base.OnModelCreating(modelBuilder);
        }

    }
}
