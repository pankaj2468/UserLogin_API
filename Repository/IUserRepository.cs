using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserLogin_API
{
    public interface IUserRepository
    {
        User GetUserByCredentials(string username, string password);
       List<UserRegisterDetails> GetUserDetails();
        bool RegisterUser(UserRegister model);

    }
}
