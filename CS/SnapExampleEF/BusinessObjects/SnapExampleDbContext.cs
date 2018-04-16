using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using DevExpress.Persistent.BaseImpl.EF;
using DevExpress.ExpressApp.EF.Updating;
using System.Data.Common;

namespace SnapExampleEF.BusinessObjects {
    public class SnapExampleDbContext : DbContext {
        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Person>().ToTable("Parties_Person");
            modelBuilder.Entity<ModuleInfo>().ToTable("ModulesInfo");

            modelBuilder.Entity<DocumentData>()
            	.HasMany(r => r.DataSources)
            	.WithOptional(p => p.DocumentData)
            	.WillCascadeOnDelete(true);
        }

        public SnapExampleDbContext(String connectionString)
            : base(connectionString) {
        }
        public SnapExampleDbContext(DbConnection connection)
            : base(connection, false) {
        }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<DocumentData> DocumentDatas { get; set; }
        public DbSet<DocumentDataSource> DocumentDataSources { get; set; }
        public DbSet<ModuleInfo> ModulesInfo { get; set; }
        public DbSet<Party> Parties { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<PhoneNumber> PhoneNumbers { get; set; }
        public DbSet<Task> Tasks { get; set; }
    }
}
