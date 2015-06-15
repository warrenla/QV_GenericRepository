using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using QV.Data.Models.Mapping;
using Repository.Pattern.Ef6;

namespace QV.Data.Models
{
    public partial class Qv21Context : DataContext
    {
        private static  bool _rebuildDb = false;
        static Qv21Context()
        {
            if (_rebuildDb == true)
            {
                Database.SetInitializer<Qv21Context>(new QvlDbInitializer());
            }
        }

        public Qv21Context(bool rebuildDb )
            : base("Name=QV21Context")
        {
            _rebuildDb = rebuildDb;
            if (_rebuildDb == true)
            {
                Database.SetInitializer<Qv21Context>(new QvlDbInitializer());
            }
        }

        public DbSet<Dock> Docks { get; set; }
        public DbSet<DockDetail> DockDetails { get; set; }
        public DbSet<Site> Sites { get; set; }
        public DbSet<SiteDetail> SiteDetails { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new DockMap());
            modelBuilder.Configurations.Add(new DockDetailMap());
            modelBuilder.Configurations.Add(new SiteMap());
            modelBuilder.Configurations.Add(new SiteDetailMap());
        }
    }
}
