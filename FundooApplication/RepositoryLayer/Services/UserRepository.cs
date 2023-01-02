using CommonLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Services
{
    public class UserRepository : IUserRepository
    {
        DBContext dBContext;
        private readonly IConfiguration config;
        
        public UserRepository(DBContext dBContext, IConfiguration config) 
        { 
            this.dBContext = dBContext;
            this.config = config;
        }

        public UserEntity UserRegistration(UserPostModel user)
        {
            try
            {
                UserEntity userEntity = new UserEntity();
                userEntity.UserID = new UserEntity().UserID;
                userEntity.FirstName = user.FirstName;
                userEntity.LastName = user.LastName;
                userEntity.EmailID = user.EmailID;
                userEntity.Password = EncryptPassword(user.Password);
                dBContext.userTable.Add(userEntity);
                dBContext.SaveChanges();
                return userEntity;
            }
            catch(Exception e)
            {
                throw e;
            }

        }

        public string UserLogin(UserLoginModel userLogin) 
        {
            try
            {
                var userEntity = this.dBContext.userTable.FirstOrDefault(x => x.EmailID == userLogin.EmailID); //&& x.Password == userLogin.Password);
                if (userEntity != null)
                {
                    string decryptedPass = DecryptPassword(userEntity.Password);
                    if (decryptedPass == userLogin.Password) //&& userEntity != null) 
                    {
                        var token = this.GenerateToken(userEntity.EmailID, userEntity.UserID);
                        return token;
                    }
                    return null;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e) { throw e; }
            
        }


        public bool ResetUserPassword(string email, string password, string confirmPassword)
        {
            try
            {
                if (password.Equals(confirmPassword))
                {
                    var user = this.dBContext.userTable.Where(x => x.EmailID == email).FirstOrDefault();
                    string newEncryptedPassword = EncryptPassword(password);
                    user.Password = newEncryptedPassword;
                    dBContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public string ForgotPassword(string email)
        {
            try
            {
                var result = this.dBContext.userTable.FirstOrDefault(x => x.EmailID == email);
                if (result != null)
                {
                    var token = this.GenerateToken(result.EmailID, result.UserID);
                    MSMQ ms = new MSMQ();
                    ms.SendMessage(token, result.EmailID, result.FirstName + " " + result.LastName);
                    return token.ToString();
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public string GenerateToken(string EmailID, long userID)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.config[("Jwt:Key")]));
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Email, EmailID),
                        new Claim("userID", userID.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddHours(2),
                    SigningCredentials = new SigningCredentials(tokenKey, SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public string EncryptPassword(string password)
        {
            try
            {
                byte[] encData_byte = new byte[password.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(password);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in base64Encode" + ex.Message);
            }
        }



        public string DecryptPassword(string encodedData)
        {
            System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
            System.Text.Decoder utf8Decode = encoder.GetDecoder();
            byte[] todecode_byte = Convert.FromBase64String(encodedData);
            int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
            char[] decoded_char = new char[charCount];
            utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
            string result = new String(decoded_char);
            return result;
        }



        

    }
}
