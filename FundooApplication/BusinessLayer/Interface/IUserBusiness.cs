using CommonLayer;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interface
{
    public interface IUserBusiness
    {

        public UserEntity UserRegistration(UserPostModel user);
        public string UserLogin(UserLoginModel user);
        public bool ResetUserPassword(string email, string password, string confirmPassword);
        public string ForgotPassword(string email);

    }
}
