using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using System.Runtime.Serialization;

namespace BLL.Entities
{
    [Serializable]
    [DataContract]
    public class UserBll
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public string LastName { get; set; }
        [DataMember]
        public UserGender Gender { get; set; }
        [DataMember]
        public VisaRecord[] VisaRecords { get; set; }
    }
}
