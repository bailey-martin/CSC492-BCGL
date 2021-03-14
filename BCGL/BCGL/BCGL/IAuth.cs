using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BCGL
{
    public interface IAuth
    {
        Task<string> LoginWithEmailPassword(string email, string password);
    }
}
