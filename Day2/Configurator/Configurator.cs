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

namespace ConfigurationLayer
{
    public class Configurator
    {
        private string FilePath;
        public MasterService masterService;
        public List<IService<UserBll>> slaveServices;
        public Configurator(int masterServiceCount,int slaveServiceCount,IServiceCountValidator validator, string filePath)
        {
            if (ReferenceEquals(filePath, null))
            {
                throw new NullReferenceException("file name is null");
            }
            if (!validator.Validate(masterServiceCount, slaveServiceCount))
            {
                throw new Exception("incorrect service count");
            }
            this.FilePath = filePath;
            //this.masterService = new MasterService();
            var slaveServices = new List<SlaveService>();

            var generator = new IdGenerator(new SimpleNumberGenerator());

            for(int i=0;i < slaveServiceCount; i++)
            {
                slaveServices.Add(new SlaveService());
            }
            var xmlRepository = new XmlRepository(FilePath);
            var repository = new MemoryRepository(generator, xmlRepository);


            this.slaveServices = new List<IService<UserBll>>(slaveServices.ToArray());
            this.masterService = new MasterService(repository, new UserValidator(), slaveServices);

        }
    }
}
