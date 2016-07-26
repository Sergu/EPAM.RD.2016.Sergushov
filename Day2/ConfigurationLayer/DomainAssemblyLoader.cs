using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationLayer
{
    public class DomainAssemblyLoader : MarshalByRefObject
    {
        public object LoadFrom(string fileName, Type type, params object[] par)
        {
            var assembly = Assembly.LoadFrom(fileName);
            var types = assembly.GetTypes();
            var suitType = types.FirstOrDefault(t => t.Name == type.Name);
            var instance = Activator.CreateInstance(suitType, par);
            return instance;
        }
    }
}
