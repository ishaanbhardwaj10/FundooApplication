using BusinessLayer.Interface;
using CommonLayer;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class UserBusiness : IUserBusiness
    {
        private readonly IUserRepository userRepository;

        public UserBusiness(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public UserEntity UserRegistration(UserPostModel user)
        {
            try
            {
                return userRepository.UserRegistration(user);
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }

        public string UserLogin(UserLoginModel user)
        {
            try
            {
                return userRepository.UserLogin(user);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public bool ResetUserPassword(string email, string password, string confirmPassword)
        {
            try
            {
                return userRepository.ResetUserPassword(email, password, confirmPassword);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }


        public string ForgotPassword(string email)
        {
            try
            {
                return userRepository.ForgotPassword(email);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }


    }
}
