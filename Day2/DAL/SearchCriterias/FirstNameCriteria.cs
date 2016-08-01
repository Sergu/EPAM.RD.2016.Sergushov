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
    public class FirstNameCriteria : ISearchCriteria
    {
        [DataMember]
        public string Name { get; set; }
        public FirstNameCriteria(string firstName)
        {
            this.Name = firstName;
        }
        public bool IsSuitable(UserEntity entity)
        {
            if (entity.FirstName == Name)
                return true;
            return false;
        }
    }
}
