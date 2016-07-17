using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Attributes;
using ReflectionCreator;
using System.Collections.Generic;

namespace ReflectionCreatorTest
{
    [TestClass]
    public class UserCreatorTest
    {
        [TestMethod]
        public void CreateAdvancedUser_returnsAdvancedUsersCountofInstantiateAdvancedAttribute()
        {
            var advancedUsers = new AdvancedUser[] {
                new AdvancedUser(1, 3443454) { FirstName = "Pavelaaaa", LastName = "Pavlov" },
                new AdvancedUser(4,12) {FirstName = "Senia",LastName = "Sidorov" },
                new AdvancedUser(4,2329423) {FirstName = "Vasia",LastName="Ivanov" }
            };

            var userCreator = new UserCreator();

            var result = userCreator.CreateAdvancedUsers();
            CollectionAssert.Equals(result, advancedUsers);
        }
        [TestMethod]
        public void CreateUser_returnsUsersCountOfInstantiateUserAttribute()
        {
            var users = new User[] {
                new User(1) { FirstName = "Alexander", LastName = "Alexandrov" },
                new User(-1) {FirstName = "Semen",LastName = "Semenov" },
                new User(3) {FirstName = "Petr",LastName="Petrov" }
            };

            var userCreator = new UserCreator();

            var result = userCreator.CreateUsers();
            CollectionAssert.Equals(result, users);
        }
        [TestMethod]
        public void ValidateProperties_checkAdvancedUsers_returnValidUsers()
        {
            var validAdvancedUsers = new AdvancedUser[] {
                new AdvancedUser(4,12) {FirstName = "Senia",LastName = "Sidorov" },
                new AdvancedUser(4,2329423) {FirstName = "Vasia",LastName="Ivanov" }
            };

            var userCreator = new UserCreator();

            var result = new List<User>();

            foreach(var user in userCreator.CreateAdvancedUsers())
            {
                if(userCreator.ValidateProperties(user))
                    result.Add(user);
            }

            CollectionAssert.Equals(validAdvancedUsers, result);
        }
        [TestMethod]
        public void ValidateProperties_checkUsers_returnValidUsers()
        {
            var validUsers = new User[] {
                new User(-1) {FirstName = "Semen",LastName = "Semenov" },
                new User(3) {FirstName = "Petr",LastName="Petrov" }
            };

            var userCreator = new UserCreator();

            var result = new List<User>();

            foreach (var user in userCreator.CreateAdvancedUsers())
            {
                if (userCreator.ValidateProperties(user))
                    result.Add(user);
            }

            CollectionAssert.Equals(validUsers, result);
        }
        [TestMethod]
        public void ValidateFields_checkUser_returnTrueIfValid()
        {
            var validUsers = new User[] {
                new User(1) { FirstName = "Alexander", LastName = "Alexandrov" },
                new User(3) {FirstName = "Petr",LastName="Petrov" }
            };

            var userCreator = new UserCreator();

            var result = new List<User>();

            foreach (var user in userCreator.CreateAdvancedUsers())
            {
                if (userCreator.ValidateFields(user))
                    result.Add(user);
            }

            CollectionAssert.Equals(validUsers, result);
        }
    }
}
