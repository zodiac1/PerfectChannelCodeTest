using Entities.Interfaces;

namespace Entities
{
    public class User : IUser
    {
        public string Username { get; set; }

        public User(string username)
        {
            Username = username;
        }
    }
}