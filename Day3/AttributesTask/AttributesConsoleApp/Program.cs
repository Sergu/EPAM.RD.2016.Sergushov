using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Attributes;
using System.ComponentModel;
using ReflectionCreator;

namespace AttributesConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var passedValidation = new List<object>();
            var incorrectObjects = new List<object>();
            var creator = new UserCreator();
            var advancedUsers = creator.CreateAdvancedUsers();
            var users = creator.CreateUsers();

            foreach(var user in users)
            {
                if (creator.ValidateProperties(user) && creator.ValidateFields(user))
                    passedValidation.Add(user);
                else
                    incorrectObjects.Add(user);
            }
        }
    }
}
