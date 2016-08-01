using BLL.Entities;
using BLL.Services;
using BLL.Mappers;
using BLL.Validators;
using CustomNumberGenerators;
using DAL.Entities;
using DAL.Exceptions;
using DAL.Generator;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using DAL.SearchCriterias;

namespace ConfigurationLayer
{
    public class Configurator
    {
        public ConfiguredServices ConfigurateServices()
        {
            var addresses = new List<EndPointAddress>()
            {
                new EndPointAddress() {address = "127.0.0.1", port = 9000 },
                new EndPointAddress() {address = "127.0.0.1", port = 9001 },
                new EndPointAddress() {address = "127.0.0.1", port = 9002 }
                //new EndPointAddress() {address = "127.0.0.1", port = 9003 }
            };
            var isLogged = true;
            int slaveServiceCount = 3;
            int masterServiceCount = 1;
            string filePath = "state.xml";

            var slaveServices = new List<SlaveService>();
            var generator = new IdGenerator(new SimpleNumberGenerator());
            var repository = new MemoryRepository(generator);
            var xmlRepository = new XmlRepository(filePath);

            slaveServices = ConfigureSlaveServices(xmlRepository, isLogged, slaveServiceCount,addresses.ToArray());
            var masterService = ConfigureMasterService(repository, new UserValidator(), xmlRepository, addresses, isLogged);

            return new ConfiguredServices { slaveServices = slaveServices, masterService = masterService };
        }
        private T CreateInstance<T>(string domainName, params object[] par)
        {
            AppDomain domain = AppDomain.CreateDomain(domainName);
            var loader = (DomainAssemblyLoader)domain.CreateInstanceAndUnwrap(Assembly.GetExecutingAssembly().FullName, typeof(DomainAssemblyLoader).FullName);
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "BLL.dll");
            return (T)loader.LoadFrom(path, typeof(T), par);
        }
        private List<SlaveService> ConfigureSlaveServices(IFileRepository<SavedEntity> xmlRepository,bool isLogged,int slaveCount,EndPointAddress[] addresses)
        {
            var slaves = new List<SlaveService>();
            List<UserBll> users = null;
            try
            {
                users = new List<UserBll>(xmlRepository.GetState().Users.Select(e => e.ToBllUser()));
            }catch(XmlException ex)
            {
                users = new List<UserBll>();
            }
            for(int i = 0; i < slaveCount; i++)
            {
                var domain = string.Format("SlaveDomain-{0}", i.ToString());
                var slave = CreateInstance<SlaveService>(domain,i+1,users, isLogged,addresses[i]);
                slaves.Add(slave);
            }
            return slaves;
        }
        private MasterService ConfigureMasterService(IRepository<UserEntity> repository,IValidator<UserBll> validator,IFileRepository<SavedEntity> xmlRepository,IEnumerable<EndPointAddress> adresses,bool isLogged)
        {
            return CreateInstance<MasterService>("MasterDomain", repository, validator, xmlRepository, adresses, isLogged);
        }
    }
}
