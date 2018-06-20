using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;

using Entities.Interfaces;

using Logging;

namespace DataAccess.DataStores
{
    /// <summary>
    /// Holds information about users
    /// </summary>
    public class UserStore : IDataStore<IUser>
    {
        private IDictionary<string, IUser> users;

        public UserStore()
        {
            users = new Dictionary<string, IUser>();
        }

        public UserStore(IDictionary<string, IUser> users)
        {
            this.users = users;
        }

        public string Id { get; set;}

        public IEnumerable<IUser> GetAll()
        {
            return users.Values.ToList();
        }

        public IUser Find(object username)
        {
            string usernameString = (string)username;

            if (users.ContainsKey(usernameString))
                return users[usernameString];
            else
                return null;
        }

        public void Add(IUser user)
        {
            if (!users.ContainsKey(user.Username))
            {
                Log.Info(string.Format("[{0}][{1}][{2}] Creating new user '" + user.Username + "'", Assembly.GetExecutingAssembly().GetName().Name, this.GetType().Name, MethodBase.GetCurrentMethod().Name));

                users.Add(user.Username, user);
            }
            else
                users[user.Username] = user;
        }

        public void Remove(IUser user)
        {
            if (users.ContainsKey(user.Username))
                users.Remove(user.Username);
        }

        public void Update(IUser user)
        {
            throw new NotImplementedException();
        }

        public void Save(IUser user)
        {
            throw new NotImplementedException();
        }

        public void Refresh()
        {
            throw new NotImplementedException();
        }

        public void Reset()
        {
            users.Clear();
        }

        public int Count()
        {
            return users.Count;
        }
    }
}