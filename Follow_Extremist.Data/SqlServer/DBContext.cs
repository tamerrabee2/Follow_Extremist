using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Follow_Extremist.Core;
using Microsoft.EntityFrameworkCore;

namespace Follow_Extremist.Data.SqlServer
{
    public class DBContext : DbContext
    {
        public DBContext()
        {

        }

        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {

        }

        // set connection 
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var conn = !string.IsNullOrEmpty(SqlCon.SqlConnection)
                    ? SqlCon.SqlConnection
                    : @"Server=DESKTOP-2B87UHT\MSSQLSERVER2019;Database=FollowExtremistDatabase;Trusted_Connection=True;TrustServerCertificate=True;";

                optionsBuilder.UseSqlServer(conn, sqlOptions =>
                {
                    sqlOptions.UseCompatibilityLevel(110);
                });
            }

            optionsBuilder.ConfigureWarnings(w => w.Ignore(Microsoft.EntityFrameworkCore.Diagnostics.RelationalEventId.PendingModelChangesWarning));
        }

        // Tables 
        public DbSet<ElementInfo> ElementInfo { get; set; }
        public DbSet<SystemRecords> SystemRecords { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<UsersRoles> UsersRoles { get; set; }
        public DbSet<ElementAddInfo> ElementAddInfo { get; set; }
        public DbSet<ElementFollowAdd> ElementFollowAdd { get; set; }
        public DbSet<ElementCases> ElementCases { get; set; }

        // Fingerprint Attendance Tables
        public DbSet<FingerprintDevice> FingerprintDevice { get; set; }
        public DbSet<ElementFingerprint> ElementFingerprint { get; set; }
        public DbSet<AttendanceLog> AttendanceLog { get; set; }
        public DbSet<ElementWantedStatus> ElementWantedStatus { get; set; }
        public DbSet<PrintSetting> PrintSetting { get; set; }

        // Views 
        public DbSet<ElementInfoView> ElementInfoView { get; set; }
        public DbSet<TodayAttendanceView> TodayAttendanceView { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ElementInfoView>().HasNoKey().ToView("ElementInfoView");
            modelBuilder.Entity<TodayAttendanceView>().HasNoKey().ToView("vw_TodayAttendance");

            // Try without specifying the schema
            modelBuilder.Entity<Users>().ToTable("Users");

            // Relationships
            modelBuilder.Entity<ElementWantedStatus>()
                .HasOne(w => w.ElementInfo)
                .WithOne(e => e.ElementWantedStatus)
                .HasForeignKey<ElementWantedStatus>(w => w.ElementId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ElementFingerprint>()
                .HasOne(f => f.ElementInfo)
                .WithMany(e => e.ElementFingerprints)
                .HasForeignKey(f => f.ElementId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<AttendanceLog>()
                .HasOne(a => a.ElementInfo)
                .WithMany(e => e.AttendanceLogs)
                .HasForeignKey(a => a.ElementId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<AttendanceLog>()
                .HasOne(a => a.FingerprintDevice)
                .WithMany(d => d.AttendanceLogs)
                .HasForeignKey(a => a.DeviceId)
                .OnDelete(DeleteBehavior.SetNull);

            base.OnModelCreating(modelBuilder);
        }

    }
}
