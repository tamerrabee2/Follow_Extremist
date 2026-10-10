using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Follow_Extremist.Data.SqlServer
{
    public class DBContextFactory : IDesignTimeDbContextFactory<DBContext>
    {
        public DBContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DBContext>();
            var connectionString = !string.IsNullOrEmpty(SqlCon.SqlConnection)
                ? SqlCon.SqlConnection
                : @"Server=DESKTOP-2B87UHT\MSSQLSERVER2019;Database=FollowExtremistDatabase;Trusted_Connection=True;TrustServerCertificate=True;";

            optionsBuilder.UseSqlServer(connectionString, sqlOptions =>
            {
                sqlOptions.UseCompatibilityLevel(110);
            });

            return new DBContext(optionsBuilder.Options);
        }
    }
}
