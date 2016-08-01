using BLL.Entities;
using BLL.Mappers;
using BLL.Services;
using ConfigurationLayer;
using DAL.Entities;
using DAL.SearchCriterias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfUserStorageService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class UserStorageService : IUserStorageService
    {
        private ServiceProxy proxy;

        public UserStorageService() { }

        public UserStorageService(ServiceProxy proxy)
        {
            this.proxy = proxy;
        }
        public int Add(UserBll user)
        {
            return proxy.Add(user);
        }
        public void Delete(int id)
        {
            proxy.Delete(id);
        }
        public IEnumerable<UserBll> Search(ISearchCriteria criteria)
        {
            return proxy.Search(criteria);
        }
    }
}
