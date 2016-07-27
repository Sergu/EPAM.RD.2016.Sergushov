using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConfigurationLayer;
using BLL.Services;
using BLL.Entities;
using DAL.Entities;
using DAL.SearchCriterias;

namespace ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            MasterService masterService = null;
            List<IService<UserBll>> slaveServices = null;

            bool isLog = true;
            int masterServiceCount = 1;
            int slaveServiceCount = 4;
            string filePath = "state.xml";

            var configurator = new Configurator();

            configurator.ConfigurateServices(masterServiceCount, slaveServiceCount,filePath,isLog);

            var visa = new VisaRecord() { Country = "England", EndDate = DateTime.Now, StartDate = DateTime.Now };
            var visaRecords = new VisaRecord[]
            {
                new VisaRecord() {Country = "Austria", EndDate = DateTime.Now, StartDate = DateTime.Now },
                new VisaRecord() {Country = "Bulgaria", EndDate = DateTime.Now, StartDate = DateTime.Now },
                visa
            };
            UserBll userBll = new UserBll
            {
                FirstName = "Vasia",
                LastName = "Petrov",
                Gender = UserGender.male,
                VisaRecords = visaRecords
            };
            masterService.Add(userBll);
            masterService.SaveServiceState();

            var result = slaveServices[1].Search(new VisaCriteria(visa));

            try
            {
                slaveServices[0].Delete(1);
            }
            catch(Exception ex)
            {

            }
        }
    }
}
