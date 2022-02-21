using BookStoreModel.Models;
using BookStoreRepository.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BookStoreRepository.Repository
{
    public class UserRepository:IUserRepository
    {
        public IConfiguration configuration { get; }
        public UserRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public bool Register(UserRegistration user)
        {
            try
            {
                SqlConnection con = new SqlConnection(this.configuration.GetConnectionString("BookStoreDB"));
                SqlCommand cmd = new SqlCommand("sp_AddUsers", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Name", user.Name);
                cmd.Parameters.AddWithValue("@Email", user.Email);
                cmd.Parameters.AddWithValue("@Password",EncryptPassword(user.Password));
                cmd.Parameters.AddWithValue("@Phone", user.Phone);
                con.Open();
                var result = cmd.ExecuteNonQuery();
                con.Close();
                if (result > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public string EncryptPassword(string password)
        {
            try
            {
                byte[] encode = new byte[password.Length];
                encode = Encoding.UTF8.GetBytes(password);
                string encPassword = Convert.ToBase64String(encode);
                return encPassword;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public string Login(LoginModel login)
        {
            SqlConnection con = new SqlConnection(this.configuration.GetConnectionString("BookStoreDB"));
            SqlCommand cmd = new SqlCommand("sp_UserLogin", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Email", login.Email);
            cmd.Parameters.AddWithValue("@Password", EncryptPassword(login.Password));
            con.Open();
            int result = (Int32)cmd.ExecuteScalar();
            con.Close();
            if (result > 0)
                return GenerateJwtToken(login.Email);
            else
                return null;
        }
        public string GenerateJwtToken(string email)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(configuration["Jwt:key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("Email", email) }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }

    
}
