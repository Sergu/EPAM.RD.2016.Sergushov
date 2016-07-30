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
        public void ConfigurateServices(int masterServiceCount, int slaveServiceCount, string filePath, bool isLog,EndPointAddress[] addresses)
        {
            var slaveServices = new List<SlaveService>();

            var generator = new IdGenerator(new SimpleNumberGenerator());
            var repository = new MemoryRepository(generator);
            var xmlRepository = new XmlRepository(filePath);
            var isLogged = true;

            slaveServices = ConfigureSlaveServices(xmlRepository, isLogged, slaveServiceCount,addresses);

            var visa1 = new VisaRecord() { Country = "Austria", EndDate = DateTime.Now, StartDate = DateTime.Now };
            var visa2 = new VisaRecord() { Country = "Bulgaria", EndDate = DateTime.Now, StartDate = DateTime.Now };
            var visaRecords1 = new VisaRecord[]
            {
                visa1,
                visa2
            };
            var user = new UserBll()
            {
                FirstName = "nick",
                LastName = "foligno",
                Gender = UserGender.male,
                VisaRecords = visaRecords1
            };

            var masterService = ConfigureMasterService(repository, new UserValidator(), xmlRepository, addresses, isLogged);
            masterService.Add(user);
            Thread.Sleep(500);
            masterService.Add(user);
            Thread.Sleep(500);
            var searchedUsers = slaveServices[1].Search(new FirstNameCriteria("NICK"));

            masterService.Delete(3);
            Thread.Sleep(500);
            masterService.Add(user);


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
