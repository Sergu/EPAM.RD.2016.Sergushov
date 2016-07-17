using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Attributes;
using System.ComponentModel;

namespace ReflectionCreator
{
    public class UserCreator
    {
        private Assembly assembly;
        public UserCreator()
        {
            assembly = Assembly.Load("Attributes");
        }
        public IEnumerable<AdvancedUser> CreateAdvancedUsers()
        {
            var createdUsers = new List<AdvancedUser>();
            var attributes = (IEnumerable<InstantiateAdvancedUserAttribute>)assembly.GetCustomAttributes(typeof(InstantiateAdvancedUserAttribute));
            Type type = typeof(AdvancedUser);
            foreach (var attribute in attributes)
            {
                if (ReferenceEquals(attribute.ExternalId, null))
                    attribute.ExternalId = GetIdFromAttributeByParam(type, "externalId");
                if (ReferenceEquals(attribute.Id, null))
                    attribute.Id = GetIdFromAttributeByParam(type, "id");
                var user = (AdvancedUser)Activator.CreateInstance(typeof(AdvancedUser), new object[] { attribute.Id, attribute.ExternalId });
                user.FirstName = attribute.FirstName;
                user.LastName = attribute.LastName;
                createdUsers.Add(user);
            }
            return createdUsers;
        }
        public IEnumerable<User> CreateUsers()
        {
            var users = new List<User>();
            var type = typeof(User);
            var attributes = (IEnumerable<InstantiateUserAttribute>)type.GetCustomAttributes(typeof(InstantiateUserAttribute));
            foreach(var attr in attributes)
            {
                if (ReferenceEquals(attr.Id, null))
                    attr.Id = GetIdFromAttributeByParam(typeof(User), "id");
                var user = (User)Activator.CreateInstance(typeof(User), new object[] { attr.Id });
                user.FirstName = attr.FirstName;
                user.LastName = attr.LastName;
                users.Add(user);
            }
            return users;
        }
        private int GetIdFromAttributeByParam(Type type, string param)
        {
            var constructors = type.GetConstructors();
            var matchAttributes = (IEnumerable<MatchParameterWithPropertyAttribute>)constructors
                .FirstOrDefault(c => c.GetCustomAttributes(typeof(MatchParameterWithPropertyAttribute)) != null)
                .GetCustomAttributes(typeof(MatchParameterWithPropertyAttribute));

            var matchAttribute = matchAttributes.FirstOrDefault(a => a.Param == param);

            var defaultAttribute = (DefaultValueAttribute)type.GetProperty(matchAttribute.Property).GetCustomAttribute(typeof(DefaultValueAttribute));

            var result = (int)defaultAttribute.Value;

            return result;
        }
        public bool ValidateProperties(object obj)
        {
            var properties = obj.GetType().GetProperties(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
            foreach(var property in properties)
            {
                if (property.PropertyType == typeof(int))
                {
                    var attr = (IntValidatorAttribute)property.GetCustomAttribute(typeof(IntValidatorAttribute));
                    if (!ReferenceEquals(attr, null))
                    {
                        var propValue = (int)property.GetValue(obj);
                        if (!((propValue > attr.Min) && (propValue < attr.Max)))
                            return false;
                    }
                }
                if(property.PropertyType == typeof(string))
                {
                    var attr = (StringValidatorAttribute)property.GetCustomAttribute(typeof(StringValidatorAttribute));
                    if (!ReferenceEquals(attr, null))
                    {
                        var propValue = (string)property.GetValue(obj);
                        if (propValue.Length > attr.MaxLength)
                            return false;
                    }
                }
            }
            return true;
        }
        public bool ValidateFields(object obj)
        {
            var fields = obj.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
            foreach (var field in fields)
            {
                if (field.FieldType == typeof(int))
                {
                    var attr = (IntValidatorAttribute)field.GetCustomAttribute(typeof(IntValidatorAttribute));
                    if (!ReferenceEquals(attr, null))
                    {
                        var propValue = (int)field.GetValue(obj);
                        if (!((propValue > attr.Min) && (propValue < attr.Max)))
                            return false;
                    }
                }
                if (field.FieldType == typeof(string))
                {
                    var attr = (StringValidatorAttribute)field.GetCustomAttribute(typeof(StringValidatorAttribute));
                    if (!ReferenceEquals(attr, null))
                    {
                        var propValue = (string)field.GetValue(obj);
                        if (propValue.Length > attr.MaxLength)
                            return false;
                    }
                }
            }
            return true;
        }
    }
}
