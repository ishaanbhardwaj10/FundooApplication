using CommonLayer;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interface
{
    public interface IUserRepository
    {
        public UserEntity UserRegistration(UserPostModel user);
        public string UserLogin(UserLoginModel loginModel);
        public bool ResetUserPassword(string email, string password, string confirmPassword);
        public string ForgotPassword(string email);
    }
}
