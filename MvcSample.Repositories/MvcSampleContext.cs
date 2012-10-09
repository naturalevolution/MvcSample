using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using MvcSample.Domain;

namespace MvcSample.Repositories {
    public class MvcSampleContext : DbContext {
        public DbSet<Knight> Knights { get; set; }
        public DbSet<Princess> Princesses { get; set; }
        public DbSet<Castle> Castles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

    }
}
