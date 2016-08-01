using BLL.Entities;
using BLL.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationLayer
{
    public class ConfiguredServices
    {
        public MasterService masterService { get; set; }
        public IEnumerable<IService<UserBll>> slaveServices { get; set; }
    }
}
