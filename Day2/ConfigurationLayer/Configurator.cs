using BLL.Entities;
using BLL.Services;
using BLL.Validators;
using CustomNumberGenerators;
using DAL.Generator;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationLayer
{
    public class Configurator
    {
        //private string FilePath;
        //public MasterService masterService;
        //public List<IService<UserBll>> slaveServices;
        //public ILogger logger;
        public void ConfigurateServices(int masterServiceCount, int slaveServiceCount, string filePath, MasterService masterService, List<IService<BLL.Entities.UserBll>> slaveServices, bool isLog)
        {
            if (ReferenceEquals(filePath, null))
            {
                throw new NullReferenceException("file name is null");
            }

            var tempSlaveServices = new List<SlaveService>();

            var generator = new IdGenerator(new SimpleNumberGenerator());

            for (int i = 0; i < slaveServiceCount; i++)
            {
       
                var domain = String.Format("SlaveDomain-{0}", i.ToString());
                //var slaveService = (SlaveService)domain.CreateInstanceAndUnwrap("BLL", typeof(SlaveService).FullName);
                var slave = CreateInstance<SlaveService>(domain);
                tempSlaveServices.Add(slave);
            }
            var xmlRepository = new XmlRepository(filePath);
            var repository = new MemoryRepository(generator);


            //slaveServices = new List<IService<UserBll>>(tempSlaveServices.ToArray());

            //var masterDomain = AppDomain.CreateDomain("domainMaster");
            //masterService = (MasterService)masterDomain.CreateInstanceAndUnwrap("BLL",
            //    typeof(MasterService).FullName,
            //    false,
            //    BindingFlags.Default,
            //    null,
            //    new object[] { repository, new UserValidator(), tempSlaveServices, xmlRepository },
            //    null,
            //    null);
            //masterService = new MasterService(repository, new UserValidator(), tempSlaveServices,xmlRepository);
        }
        private T CreateInstance<T>(string domainName, params object[] par)
        {
            AppDomain domain = AppDomain.CreateDomain(domainName);
            var loader = (DomainAssemblyLoader)domain.CreateInstanceAndUnwrap(Assembly.GetExecutingAssembly().FullName, typeof(DomainAssemblyLoader).FullName);
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "BLL.dll");
            return (T)loader.LoadFrom(path, typeof(T), par);
        }
    }
}
