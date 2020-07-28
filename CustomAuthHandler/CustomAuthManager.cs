using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomAuthHandler
{
    public class CustomAuthManager : ICustomAuthManager
    {
        IDictionary<string, string> users = new Dictionary<string, string>()
        {{"user1","test1" },{"user2","test2" },{"user3","test3" } };

        IDictionary<string, string> validUsers = new Dictionary<string, string>();

        public IDictionary<string, string> ValidUsers => validUsers;

        public string Authenticate(string userName, string pwd)
        {
            if (!(users.Any(u => u.Key == userName && u.Value == pwd)))
            {
                return "Failure";
            }

            string token = Guid.NewGuid().ToString();
            validUsers.Add(token, userName);
            return token;
        }
    }
}
