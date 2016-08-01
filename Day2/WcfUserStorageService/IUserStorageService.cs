using BLL.Entities;
using DAL.SearchCriterias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfUserStorageService
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени интерфейса "IUserStorageService" в коде и файле конфигурации.
    [ServiceContract]
    [ServiceKnownType(typeof(FirstNameCriteria))]
    [ServiceKnownType(typeof(LastNameCriteria))]
    [ServiceKnownType(typeof(VisaCriteria))]
    public interface IUserStorageService
    {
        [OperationContract]
        int Add(UserBll user);

        [OperationContract]
        void Delete(int id);

        [OperationContract]
        IEnumerable<UserBll> Search(ISearchCriteria criteria);
    }
}
