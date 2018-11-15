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
            
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<SqlContext>());
        }

        public DbSet<ImageMetadata> Metadata { get; set; }
        
        public DbSet<UserCredentials> Users { get; set; }
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ImageMetadata>()
                        .HasKey(p => p.ImageId);

            modelBuilder.Entity<UserCredentials>()
                        .HasKey(p => p.Id);
        }
    }
}