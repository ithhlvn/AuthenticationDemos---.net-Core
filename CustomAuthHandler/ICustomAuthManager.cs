using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomAuthHandler
{
    public interface ICustomAuthManager
    {
        string Authenticate(string userName, string pwd);
        IDictionary<string, string> ValidUsers { get; }
    }
}
