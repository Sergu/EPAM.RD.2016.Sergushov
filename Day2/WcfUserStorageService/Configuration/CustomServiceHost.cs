using BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;

namespace WcfUserStorageService.Configuration
{
    public class CustomServiceHost : ServiceHost
    {
        public CustomServiceHost(ServiceProxy proxy, Type serviceType, params Uri[] baseAddresses)
        : base(serviceType, baseAddresses)
        {
            if (proxy == null)
            {
                throw new ArgumentNullException(nameof(proxy));
            }

            foreach (var cd in this.ImplementedContracts.Values)
            {
                cd.Behaviors.Add(new CustomInstanceProvider(proxy));
            }
        }
    }
}