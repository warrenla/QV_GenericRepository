using System.Linq;
using QV.Data.Models;
using Repository.Pattern.Repositories;

namespace QV.Repository.Repositories
{
    // Exmaple: How to add custom methods to a repository.
    // The following are just examples... 
    public static class SiteRepository
    {
        public static int GetSiteCountDemo(this IRepository<Site> repository)
        {
            return repository.Queryable().Select(s => s.SiteId).Count();
        }

        public static int GetSiteDockCountDemo(this IRepository<Site> repository, int siteId)
        {
            return repository.Queryable().Where(s => s.SiteId == siteId).Select(s => s.SiteId).Count();
        }

        public static Site GetSiteDeepGrapho(this IRepository<Site> siteRepository, IRepository<SiteDetail> siteDetailRepository, int siteId)
        {
            //var result = from sr in siteRepository
            //    join sd in siteDetailRepository.Queryable().ToList() on sr.siteId equals sd
            //    select sr;

            return null;

            //return result;
        }
    }
}