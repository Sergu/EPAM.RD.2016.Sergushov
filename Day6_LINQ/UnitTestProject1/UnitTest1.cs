using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        private List<User> userListFirst = new List<User>
        {
            new User
            {
                Age = 21,
                Gender = Gender.Man,
                Name = "User1",
                Salary = 21000
            },

            new User
            {
                Age = 30,
                Gender = Gender.Female,
                Name = "Liza",
                Salary = 30000
            },

            new User
            {
                Age = 18,
                Gender = Gender.Man,
                Name = "Max",
                Salary = 19000
            },
            new User
            {
                Age = 32,
                Gender = Gender.Female,
                Name = "Ann",
                Salary = 36200
            },
            new User
            {
                Age = 45,
                Gender = Gender.Man,
                Name = "Alex",
                Salary = 54000
            }
        };

        private List<User> userListSecond = new List<User>
        {
            new User
            {
                Age = 23,
                Gender = Gender.Man,
                Name = "Max",
                Salary = 24000
            },

            new User
            {
                Age = 30,
                Gender = Gender.Female,
                Name = "Liza",
                Salary = 30000
            },

            new User
            {
                Age = 23,
                Gender = Gender.Man,
                Name = "Max",
                Salary = 24000
            },
            new User
            {
                Age = 32,
                Gender = Gender.Female,
                Name = "Kate",
                Salary = 36200
            },
            new User
            {
                Age = 45,
                Gender = Gender.Man,
                Name = "Alex",
                Salary = 54000
            },
            new User
            {
                Age = 28,
                Gender = Gender.Female,
                Name = "Kate",
                Salary = 21000
            }
        };

        [TestMethod]
        public void SortByName()
        {
            var actualDataFirstList = new List<User>();
            var expectedData = userListFirst[4];

            actualDataFirstList = new List<User>(userListFirst.ToArray().OrderBy(u => u.Name));
            //ToDo Add code first list

            Assert.IsTrue(actualDataFirstList[0].Equals(expectedData));
        }

        [TestMethod]
        public void SortByNameDescending()
        {
            var actualDataSecondList = new List<User>();
            var expectedData = userListSecond[0];


            actualDataSecondList = new List<User>(userListSecond.ToArray().OrderByDescending(u => u.Name));
            //ToDo Add code first list

            Assert.IsTrue(actualDataSecondList[0].Equals(expectedData));
        }

        [TestMethod]
        public void SortByNameAndAge()
        {
            var actualDataSecondList = new List<User>();
            var expectedData = userListSecond[4];

            //ToDo Add code second list
            actualDataSecondList = new List<User>(userListSecond.ToArray()
                                                    .OrderBy(u => u.Age)
                                                    .OrderBy(u => u.Name));

            Assert.IsTrue(actualDataSecondList[0].Equals(expectedData));
        }

        [TestMethod]
        public void RemovesDuplicate()
        {
            var actualDataSecondList = new List<User>();
            var expectedData = new List<User> {userListSecond[0], userListSecond[1], userListSecond[3], userListSecond[4],userListSecond[5]};

            //ToDo Add code second list
            actualDataSecondList = new List<User>(userListSecond.Distinct());

            CollectionAssert.AreEqual(expectedData, actualDataSecondList);
        }

        [TestMethod]
        public void ReturnsDifferenceFromFirstList()
        {
            var actualData = new List<User>();
            var expectedData = new List<User> { userListFirst[0], userListFirst[2], userListFirst[3] };

            //ToDo Add code first list
            actualData = new List<User>(userListFirst.Except(userListSecond));

            CollectionAssert.AreEqual(expectedData, actualData);
        }

        [TestMethod]
        public void SelectsValuesByNameMax()
        {
            var actualData = new List<User>();
            var expectedData = new List<User> { userListSecond[0], userListSecond[2] };

            //ToDo Add code for second list
            actualData = new List<User>(userListSecond.Where(u => u.Name == "Max").Select(u => u));

            CollectionAssert.AreEqual(expectedData, actualData);
        }

        [TestMethod]
        public void ContainOrNotContainName()
        {
            var isContain = false;

            string name = "Max";
            var suitUser = new User { Name = name, Salary = 24000, Gender = Gender.Man, Age = 23 };

            isContain = userListSecond.Contains(suitUser);
            //ToDo Add code for second list

            Assert.IsTrue(isContain);

            name = "asdasda";
            isContain = userListSecond.Contains(new User { Name = name});
            //ToDo add code for second list
            Assert.IsFalse(isContain);
        }

        [TestMethod]
        public void AllListWithName()
        {
            bool isAll = false;

            string name = "Max";
            //ToDo Add code for second list
            //var a = userListSecond.ToArray().;

            Assert.IsTrue(isAll);
        }

        [TestMethod]
        public void ReturnsOnlyElementByNameMax()
        {
            var actualData = new User();
            
            try
            {
                //ToDo Add code for second list
                string name = "Max";
                actualData = userListSecond.Single(u => u.Name == name);

                Assert.Fail();
            }
            catch (InvalidOperationException ie)
            {
                Assert.AreEqual("Последовательность содержит более одного соответствующего элемента", ie.Message);
            }
            catch (Exception e)
            {
                Assert.Fail("Unexpected exception of type {0} caught: {1}", e.GetType(), e.Message);
            }
        }

        [TestMethod]
        public void ReturnsOnlyElementByNameNotOnList()
        {
            var actualData = new User();

            try
            {
                //ToDo Add code for second list
                string name = "Ldfsdfsfd";
                actualData = userListSecond.First(u => u.Name == name);

                Assert.Fail();
            }
            catch (InvalidOperationException ie)
            {
                Assert.AreEqual("Последовательность не содержит соответствующий элемент", ie.Message);
            }
            catch (Exception e)
            {
                Assert.Fail("Unexpected exception of type {0} caught: {1}", e.GetType(), e.Message);
            }
        }

        [TestMethod]
        public void ReturnsOnlyElementOrDefaultByNameNotOnList()
        {
            var actualData = new User();

            //ToDo Add code for second list

            string name = "Ldfsdfsfd";

            actualData = userListSecond.SingleOrDefault(u => u.Name == name);

            Assert.IsTrue(actualData == null);
        }


        [TestMethod]
        public void ReturnsTheFirstElementByNameNotOnList()
        {
            var actualData = new User();

            try
            {
                //ToDo Add code for second list
                string name = "Ldfsdfsfd";
                userListSecond.First(u => u.Name == name);

                Assert.Fail();
            }
            catch (InvalidOperationException ie)
            {
                Assert.AreEqual("Последовательность не содержит соответствующий элемент", ie.Message);
            }
            catch (Exception e)
            {
                Assert.Fail("Unexpected exception of type {0} caught: {1}", e.GetType(), e.Message);
            }
        }

        [TestMethod]
        public void ReturnsTheFirstElementOrDefaultByNameNotOnList()
        {
            var actualData = new User();

            //ToDo Add code for second list
            string name = "Ldfsdfsfd";
            actualData = userListSecond.FirstOrDefault(u => u.Name == name);

            Assert.IsTrue(actualData == null);
        }

        [TestMethod]
        public void GetMaxSalaryFromFirst()
        {
            var expectedData = 54000;
            var actualData = new User();

            //ToDo Add code for first list
            actualData = userListFirst.ToArray().OrderByDescending(u => u.Salary).FirstOrDefault();

            Assert.IsTrue(expectedData == actualData.Salary);
        }

        [TestMethod]
        public void GetCountUserWithNameMaxFromSecond()
        {
            var expectedData = 2;
            var actualData = 0;

            //ToDo Add code for second list
            actualData = userListSecond.ToArray().Where(u => u.Name == "Max").Count();

            Assert.IsTrue(expectedData == actualData);
        }

        [TestMethod]
        public void Join()
        {
            var NameInfo = new[]
            {
                new {name = "Max", Info = "info about Max"},
                new {name = "Alan", Info = "About Alan"},
                new {name = "Alex", Info = "About Alex"}
            }.ToList();

            var expectedData = 3;
            var actualData = -1;

            userListSecond.Join()
            //ToDo Add code for second list

            Assert.IsTrue(expectedData == actualData);
        }
    }
}
