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
using System.Configuration;
using ConfigurationLayer.CustomTags.ServiceConfig;
using ConfigurationLayer.CustomTags.FileConfig;
using ConfigurationLayer.CustomTags.AddressConfig;

namespace ConfigurationLayer
{
    public class Configurator
    {
        public ConfiguredServices ConfigurateServices()
        {
            var isLogged = true;
            int slaveServiceCount;
            int masterServiceCount;

            GetServiceCountFromConfig(out masterServiceCount,out slaveServiceCount);
            string filePath = GetFileFromConfig();
            var addresses = GetEndPointsFromConfig();

            var slaveServices = new List<SlaveService>();
            var generator = new IdGenerator(new SimpleNumberGenerator());
            var repository = new MemoryRepository(generator);
            var xmlRepository = new XmlRepository(filePath);

            slaveServices = ConfigureSlaveServices(xmlRepository, isLogged, slaveServiceCount,addresses.ToArray());
            var masterService = ConfigureMasterService(repository, new UserValidator(), xmlRepository, addresses, isLogged);

            return new ConfiguredServices { slaveServices = slaveServices, masterService = masterService };
        }
        private void GetServiceCountFromConfig(out int masterCount, out int slaveCount)
        {
            masterCount = 1;
            slaveCount = 3;
            var cfg = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var serviceSection = (RegisterServicesConfigSection)cfg.GetSection("RegisterServices");
            foreach (ServiceElement service in serviceSection.Services)
            {
                if(service.ServiceType.ToLower() == "master")
                {
                    masterCount = service.Count;
                }
                if(service.ServiceType.ToLower() == "slave")
                {
                    slaveCount = service.Count;
                }
            }
        }
        private IEnumerable<EndPointAddress> GetEndPointsFromConfig()
        {
            var addresses = new List<EndPointAddress>();
            var cfg = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var addressSection = (RegisterAddressConfigSection)cfg.GetSection("RegisterAddresses");
            
            foreach(AddressElement address in addressSection.Addresses)
            {
                var endPoint = new EndPointAddress { address = address.Address, port = address.Port };
                addresses.Add(endPoint);
            }

            return addresses;
        }
        private string GetFileFromConfig()
        {
            var cfg = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var fileSection = (RegisterFileConfigSection)cfg.GetSection("RegisterFiles");

            var fileElement = (FileElement)fileSection.Files[0];
            return fileElement.FileName;
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
