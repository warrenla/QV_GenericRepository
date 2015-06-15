using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace QV.SelfHost
{
    class Program
    {
        static void printInfo(ServiceHost host)
        {
            // OUTPUT URLS in console
            foreach (var bas in host.BaseAddresses)
            {
                Console.WriteLine(bas.AbsoluteUri.ToString());
            }
          
        }

        static void Main(string[] args)
        {
            try
            {
                ServiceHost host1 = new ServiceHost(typeof(QV.WcfServiceLibrary.QvDockService));
                printInfo(host1);
                ServiceHost host2 = new ServiceHost(typeof(QV.WcfServiceLibrary.QvDockDetailService));
                printInfo(host2);
                ServiceHost host3 = new ServiceHost(typeof(QV.WcfServiceLibrary.QvSiteService));
                printInfo(host3);
                ServiceHost host4 = new ServiceHost(typeof(QV.WcfServiceLibrary.QvSiteDetailService));
                printInfo(host4);
                host1.Open();
                host2.Open();
                host3.Open();
                host4.Open();
                Console.WriteLine("Hit any key to exit host.");
                Console.ReadKey();
                host1.Close();
                host2.Close();
                host3.Close();
                host4.Close();
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                Console.WriteLine("Hit any key to exit");
                Console.ReadKey();
            }

        }
    }
}
