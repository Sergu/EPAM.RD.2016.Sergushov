using BLL.Entities;
using DAL.SearchCriterias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Mappers;
using System.Net.Sockets;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;

namespace BLL.Services
{
    public class SlaveService : MarshalByRefObject, IService<UserBll>
    {
        private List<UserBll> users;
        private bool isLogged = true;
        private EndPointAddress address;
        private int id;

        public SlaveService(int id,IEnumerable<UserBll> users,bool isLogged,EndPointAddress address)
        {
            this.id = id;
            this.address = address;
            this.isLogged = isLogged;
            this.users = new List<UserBll>(users);
            if (this.isLogged)
                BllLogger.Instance.Trace("slave {0} : service created. domain : {1}",this.id,AppDomain.CurrentDomain.FriendlyName);
            var thread = new Thread(new ThreadStart(Listen));
            thread.Name = "listener";
            thread.Start();
        }
        public int Add(UserBll entity)
        {
            if (isLogged)
                BllLogger.Instance.Warn("slave {0} : try to add user entity",id);
            throw new Exception();
        }
        public void Delete(int id)
        {
            if (isLogged)
                BllLogger.Instance.Warn("slave {0} : try to delete user entity",id);
            throw new Exception();
        }
        public IEnumerable<UserBll> Search(ISearchCriteria criteria)
        {
            List<UserBll> suitableUsers = new List<UserBll>();
            foreach(var user in users)
            {
                if(criteria.IsSuitable(user.ToUserEntity()))
                {
                    suitableUsers.Add(user);
                }
            }
            if (isLogged)
                BllLogger.Instance.Trace("slave {0} : searched {1} users",id, suitableUsers.Count());
            return suitableUsers;
        }
        private void Listen()
        {
            TcpListener listener = null;
            TcpClient client = null;
            try
            {
                listener = new TcpListener(IPAddress.Parse(address.address), address.port);
                listener.Start();
                if(isLogged)
                    BllLogger.Instance.Trace("slave {0} : begin listen",id);
                while (true)
                {
                    client = listener.AcceptTcpClient();
                    var networkStream = client.GetStream();
                    var formatter = new BinaryFormatter();
                    var message = (Message)formatter.Deserialize(networkStream);
                    var m = message as Message;

                    if (message != null)
                    {
                        TranslateMessage(message);
                    }
                    else
                        if(isLogged)
                            BllLogger.Instance.Trace("slave recieve null mesage");
                }
            }
            //catch (SocketException ex)
            //{

            //}
            finally
            {
                client.Close();
                listener.Stop();
            }
        }
        private void TranslateMessage(Message message)
        {
            if(message.operation == Operation.add)
            {
                var user = message.param as UserBll;
                if (!ReferenceEquals(user, null))
                {
                    users.Add(user);
                    if (isLogged)
                        BllLogger.Instance.Trace("slave {0} : add user with id {1}", id, user.Id);
                }
                else
                {
                    if (isLogged)
                        BllLogger.Instance.Error("slave recieve incorrect type of user");
                }              
            }
            if(message.operation == Operation.remove)
            {
                if(message.param is int)
                {
                    var deletedUserId = (int)message.param;
                    var user = users.FirstOrDefault(u => u.Id == deletedUserId);
                    if (!ReferenceEquals(user, null))
                    {
                        users.Remove(user);
                        if (isLogged)
                            BllLogger.Instance.Trace("slave {0} : remove user with id {1}", id, user.Id);
                    }
                }
                else
                {
                    if (isLogged)
                        BllLogger.Instance.Error("slave recieve incorrect type of user");
                }
            }
        }
    }
}
