using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using System.Runtime.Serialization;

namespace DAL.SearchCriterias
{
    [Serializable]
    [DataContract]
    public class LastNameCriteria : ISearchCriteria
    {
        [DataMember]
        public string lastName { get; set; }
        public LastNameCriteria(string lastName)
        {
            this.lastName = lastName;
        }
        public bool IsSuitable(UserEntity entity)
        {
            if (entity.LastName == lastName)
                return true;
            return false;
        }
    }
}
