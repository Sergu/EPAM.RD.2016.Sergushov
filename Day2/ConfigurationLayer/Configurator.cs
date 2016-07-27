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

namespace ConfigurationLayer
{
    public class Configurator
    {
        public void ConfigurateServices(int masterServiceCount, int slaveServiceCount, string filePath, bool isLog)
        {
            var slaveServices = new List<SlaveService>();

            var generator = new IdGenerator(new SimpleNumberGenerator());
            var repository = new MemoryRepository(generator);
            var xmlRepository = new XmlRepository(filePath);
            var isLogged = false;
            var endPoints = new List<EndPointAddress>()
            {
                new EndPointAddress() {address = "localhost", port = 9000 },
                new EndPointAddress() {address = "localhost", port = 9001 },
                new EndPointAddress() {address = "localhost", port = 9002 },
                new EndPointAddress() {address = "localhost", port = 9003 }
            };

            slaveServices = ConfigureSlaveServices(xmlRepository, isLogged, slaveServiceCount);
            var masterService = ConfigureMasterService(repository, new UserValidator(), xmlRepository, endPoints, isLogged);

        }
        private T CreateInstance<T>(string domainName, params object[] par)
        {
            AppDomain domain = AppDomain.CreateDomain(domainName);
            var loader = (DomainAssemblyLoader)domain.CreateInstanceAndUnwrap(Assembly.GetExecutingAssembly().FullName, typeof(DomainAssemblyLoader).FullName);
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "BLL.dll");
            return (T)loader.LoadFrom(path, typeof(T), par);
        }
        private List<SlaveService> ConfigureSlaveServices(IFileRepository<SavedEntity> xmlRepository,bool isLogged,int slaveCount)
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
                var slave = CreateInstance<SlaveService>(domain, users, isLogged);
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
