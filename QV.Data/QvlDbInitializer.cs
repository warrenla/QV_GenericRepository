using System;
using System.Collections.Generic;
using System.Data.Entity;
using Repository.Pattern.Infrastructure;

namespace QV.Data.Models
{
    public class QvlDbInitializer : DropCreateDatabaseAlways<Qv21Context>
    {
        public override void InitializeDatabase(Qv21Context context)
        {
            // Use this command so you can have SQL Server Management Studio open so you can query the db as you need while building out this functionality.
            // This ensures you do not get the error:  Cannot drop database because it is currently in use:
            try
            {
                context.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction
                    ,
                    string.Format("ALTER DATABASE [{0}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE",
                        context.Database.Connection.Database));
            }
            catch (Exception)
            {
                // iF THE DATABASE HAS NEVER BEEN CREATED IT WILL KICK OUT AN ERROR, FOR MY NEEDS IN THIS DEMO I CAN IGNORE FOR NOW.
            }

            base.InitializeDatabase(context);
        }
        protected override void Seed(Qv21Context context)
        {
            var sites = SeedSites(context);

            foreach (var site in sites)
            {
                SeedSiteDetails(context, site);
                var docks = SeedDocks(context, site);
                foreach (var dock in docks)
                {
                    //Seed a dock detail.
                    SeedDockDetails(context, dock);
                }
            }
        }
        public List<Site> SeedSites(Qv21Context context)
        {
            var sites = new List<Site>();

            sites.Add(new Site
            {
                Active = true,
                Name = "Dallas ISD",
                PropertyName = "Test PropertyName DISD",
                ShortName = "DISD",
                ObjectState = ObjectState.Added

                // Properties = SiteProperties.RECEIVER
            });

            sites.Add(new Site
            {
                Active = true,
                Name = "Austin Depot",
                PropertyName = "Test PropertyName AD",
                ShortName = "AD",
                ObjectState = ObjectState.Added

                //  Properties = SiteProperties.BILLTO
            });

            sites.Add(new Site
            {
                Active = false,
                Name = "VarCo Oil Well",
                PropertyName = "Test PropertyName VOW",
                ShortName = "VOW",
                ObjectState = ObjectState.Added

                //   Properties = SiteProperties.DROP_ZONE
            });

            foreach (var s in sites)
            {
                context.Sites.Add(s);
            }

            context.SaveChanges();

            return sites;
        }



        private void SeedDockDetails(Qv21Context context, Dock dock)
        {
            var dockDetail = new DockDetail
            {
                DockId = dock.DockId,
                Data = string.Format("Test Data - {0}", dock.DockId),
                Key = string.Format("Test Key - {0}", dock.DockId),
                ObjectState = ObjectState.Added

            };
            context.DockDetails.Add(dockDetail);
            context.SaveChanges();
        }

        private IList<Dock> SeedDocks(Qv21Context context, Site site)
        {
            IList<Dock> docks = new List<Dock>();
            docks.Add(new Dock
            {
                SiteId = site.SiteId,
                Active = site.SiteId % 3 != 0,
                Name = string.Format("SiteDock: {0}", site.ShortName),
                Sequence = 1,
                Type = site.SiteId % 2 == 0 ? "admin" : "well",
                ObjectState = ObjectState.Added

            });

            foreach (var d in docks)
            {
                context.Docks.Add(d);
            }

            context.SaveChanges();

            return docks;
        }

        private void SeedSiteDetails(Qv21Context context, Site site)
        {
            var siteDetails = new SiteDetail
            {
                Data = string.Format("Test Data - {0}", site.SiteId),
                Key = string.Format("Test Key - {0}", site.SiteId),
                SiteId = site.SiteId,
                ObjectState = ObjectState.Added

            };
            context.SiteDetails.Add(siteDetails);
            context.SaveChanges();
        }
    }
}