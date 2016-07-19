using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Entities;
using BLL.Mappers;
using BLL.Services;
using BLL.Validators;
using DAL.Entities;
using DAL.Generator;
using DAL.Repositories;
using DAL.SearchCriterias;
using ConfigurationLayer.configVlidators;
using CustomNumberGenerators;
using NLog;
using System.Reflection;

namespace ConfigurationLayer
{
    public static class Configurator
    {
        //private string FilePath;
        //public MasterService masterService;
        //public List<IService<UserBll>> slaveServices;
        //public ILogger logger;
        public static void ConfigurateServices(int masterServiceCount, int slaveServiceCount, IServiceCountValidator validator, string filePath,out MasterService masterService,out List<IService<UserBll>> slaveServices,bool isLog)
        {
            if (ReferenceEquals(filePath, null))
            {
                throw new NullReferenceException("file name is null");
            }
            if (!validator.Validate(masterServiceCount, slaveServiceCount))
            {
                throw new Exception("incorrect service count");
            }
            BllLogger.IsLogged = isLog;

            var tempSlaveServices = new List<SlaveService>();

            var generator = new IdGenerator(new SimpleNumberGenerator());

            for (int i = 0; i < slaveServiceCount; i++)
            {
                var domain = AppDomain.CreateDomain("domain" + i.ToString());
                var slaveService = (SlaveService)domain.CreateInstanceAndUnwrap("BLL", typeof(SlaveService).FullName);
                tempSlaveServices.Add(new SlaveService());
            }
            var xmlRepository = new XmlRepository(filePath);
            var repository = new MemoryRepository(generator);


            slaveServices = new List<IService<UserBll>>(tempSlaveServices.ToArray());

            var masterDomain = AppDomain.CreateDomain("domainMaster");
            masterService = (MasterService)masterDomain.CreateInstanceAndUnwrap("BLL",
                typeof(MasterService).FullName,
                false,
                BindingFlags.Default,
                null,
                new object[] { repository,new UserValidator(),tempSlaveServices,xmlRepository},
                null,
                null);
            //masterService = new MasterService(repository, new UserValidator(), tempSlaveServices,xmlRepository);
        }
    }
}
