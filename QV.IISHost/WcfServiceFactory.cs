using QV.WcfServiceLibrary;
using Microsoft.Practices.Unity;
using QV.WcfServiceLibrary.Contracts;
using Unity.Wcf;

namespace QV.IISHost
{
	public class WcfServiceFactory : UnityServiceHostFactory
    {
        protected override void ConfigureContainer(IUnityContainer container)
        {
			// register all your components with the container here
            // container
            //    .RegisterType<IService1, Service1>()
            //    .RegisterType<DataContext>(new HierarchicalLifetimeManager());
            container.RegisterType<IWCFQvSiteService, QV.WcfServiceLibrary.QvSiteService>();
            //container.RegisterType<IWCFQvDockDetailService, QV.WcfServiceLibrary.QvDockDetailService>();

        }
    }    
}