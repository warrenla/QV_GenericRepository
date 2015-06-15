#region

using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Repository.Pattern.Ef6;
using QV.Data.Models;
using Repository.Pattern.Ef6;
#endregion

namespace QV.Test.Unit_Test.Fake
{

    // You could use a mocking lib to avoid this.

    public class QvFakeDbSets
    {
        public class SitesDbSet : FakeDbSet<Site>
        {
            public override Site Find(params object[] keyValues)
            {
                return this.SingleOrDefault(s => s.SiteId == (int)keyValues.FirstOrDefault());
            }

            public override Task<Site> FindAsync(CancellationToken cancellationToken, params object[] keyValues)
            {
                return new Task<Site>(() => Find(keyValues));
            }
        }

        public class SiteDetailsDbSet : FakeDbSet<SiteDetail>
        {
            public override SiteDetail Find(params object[] keyValues)
            {
                return this.SingleOrDefault(s => s.SiteDetailId == (int)keyValues.FirstOrDefault());
            }

            public override Task<SiteDetail> FindAsync(CancellationToken cancellationToken, params object[] keyValues)
            {
                return new Task<SiteDetail>(() => Find(keyValues));
            }
        }

        public class DocksDbSet : FakeDbSet<Dock>
        {
            public override Dock Find(params object[] keyValues)
            {
                return this.SingleOrDefault(s => s.DockId == (int)keyValues.FirstOrDefault());
            }

            public override Task<Dock> FindAsync(CancellationToken cancellationToken, params object[] keyValues)
            {
                return new Task<Dock>(() => Find(keyValues));
            }
        }

        public class DockDetailsDbSet : FakeDbSet<DockDetail>
        {
            public override DockDetail Find(params object[] keyValues)
            {
                return this.SingleOrDefault(s => s.DockDetailId == (int)keyValues.FirstOrDefault());
            }

            public override Task<DockDetail> FindAsync(CancellationToken cancellationToken, params object[] keyValues)
            {
                return new Task<DockDetail>(() => Find(keyValues));
            }
        }
    }
}