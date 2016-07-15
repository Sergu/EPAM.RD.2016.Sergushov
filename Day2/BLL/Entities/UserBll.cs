using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;

namespace BLL.Entities
{
    public class UserBll
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public UserGender Gender { get; set; }
        public VisaRecord[] VisaRecords { get; set; }
    }
}
