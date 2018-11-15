using System;
using System.Configuration;
using System.Data.Entity;
using Epam.AzureWorkShop.Entities;

namespace Epam.AzureWorkShop.Dal
{
    public class SqlContext : DbContext
    {
        public SqlContext()
        {
            Database.Connection.ConnectionString =  ConfigurationManager.ConnectionStrings["SqlDatabase"].ConnectionString;
            
            Database.SetInitializer(new DropCreateDatabaseAlways<SqlContext>());
        }

        public DbSet<ImageMetadata> Metadata { get; set; }
        
        public DbSet<UserCredentials> Users { get; set; }
    }
}