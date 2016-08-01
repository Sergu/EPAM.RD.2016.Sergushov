using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using WcfUserStorageService;
using WcfUserStorageService.Configuration;
using System.Text;
using System.Threading.Tasks;
using WcfUserStorageService;
using BLL.Services;
using ConfigurationLayer;
using System.Threading;
using System.ServiceModel.Description;
using System.Configuration;
using ConfigurationLayer.CustomTags.ServiceConfig;

namespace WcfServer
{
    class Program
    {
        static void Main(string[] args)
        {
            bool createdNew = false;
            Mutex mutex = new Mutex(true, "mutex", out createdNew);

            //var cfg = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            //var fileSection = (RegisterServicesConfigSection)cfg.GetSection("RegisterServices");

            Uri baseAddress = new Uri("http://localhost:8733/Design_Time_Addresses/WcfService/UserStorageService/");
            var services = new Configurator().ConfigurateServices();
            ServiceProxy proxy = new ServiceProxy(services.masterService,services.slaveServices);
            var service = new UserStorageService(proxy);

            using (ServiceHost host = new ServiceHost(service, baseAddress))
            {
                ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
                smb.HttpGetEnabled = true;
                smb.MetadataExporter.PolicyVersion = PolicyVersion.Policy15;
                host.Description.Behaviors.Add(smb);
                host.Open();

                mutex.ReleaseMutex();

                Console.WriteLine("service run on", baseAddress);
                Console.WriteLine("Press enter to stop the service.");
                Console.ReadLine();

                host.Close();
            }
            proxy.Save();
            Console.WriteLine("Master state was saved");
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }
    }
}
