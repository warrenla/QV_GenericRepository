using QV.Data;
using QV.Data.Models;
using Repository.Pattern.Ef6;

namespace QV.Test.Unit_Test.Fake
{
    public class QvFakeContext : FakeDbContext
    {
        public QvFakeContext()
        {
            AddFakeDbSet<Site, QvFakeDbSets.SitesDbSet>();
            AddFakeDbSet<SiteDetail, QvFakeDbSets.SiteDetailsDbSet>();
            AddFakeDbSet<Dock, QvFakeDbSets.DocksDbSet>();
            AddFakeDbSet<DockDetail, QvFakeDbSets.DockDetailsDbSet>();

        }
    }
}