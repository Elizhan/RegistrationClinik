using Microsoft.EntityFrameworkCore;
using RegistrationClinik.Models;
using System;

namespace RegistrationClinik.Infras
{
    public class ApplicationConnect : DbContext
    {
        public DbSet<DBTable> DBTables { get; set; }
        public DbSet<DBTableB> DBTablesB { get; set; }

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
    }
}
