using Microsoft.EntityFrameworkCore;
using RegistrationClinik.Models;
using System;

namespace RegistrationClinik.Infras
{
    public class ApplicationConnect : DbContext
    {
        public DbSet<DBTable> DBTables { get; set; }
        public DbSet<DBArchive> DBArchives { get; set; }

        public ApplicationConnect()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseMySql("server=localhost;user=root;password=;database=ClinikRegistationDB;",
                 new MySqlServerVersion(new Version(5, 7, 29))
             );
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{

        //    optionsBuilder.UseMySql("server=192.168.0.132;user=root;password=;database=ClinikRegistationDB;",
        //         new MySqlServerVersion(new Version(5, 7, 29))
        //     );
        //}
    }
}
