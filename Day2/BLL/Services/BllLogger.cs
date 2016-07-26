using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public static class BllLogger
    {
        private static volatile Logger instance;
        private static readonly object sync = new object();
        public static Logger Instance
        {
            get
            {
                if (!ReferenceEquals(instance, null)) return instance;
                lock(sync)
                {
                    if (ReferenceEquals(instance, null))
                    {
                        instance = LogManager.GetCurrentClassLogger();
                    }
                }
                return instance;
            }
        }
    }
}
