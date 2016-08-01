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
    public class VisaCriteria : ISearchCriteria
    {
        [DataMember]
        public VisaRecord visa { get; set; }
        public VisaCriteria(VisaRecord visa)
        {
            this.visa = visa;
        }
        public bool IsSuitable(UserEntity entity)
        {
            if (entity.VisaRecords.Contains(visa))
                return true;
            return false;
        }
    }
}
