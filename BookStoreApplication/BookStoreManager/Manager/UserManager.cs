using BookStoreManager.Interface;
using BookStoreModel.Models;
using BookStoreRepository.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStoreManager.Manager
{
    public class UserManager:IUserManager
    {
        IUserRepository userRepository;
        public UserManager(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
        public bool Register(UserRegistration user)
        {
            try
            {
                return (userRepository.Register(user));
            }
            catch (Exception)
            {

                throw;
            }
        }
        public string Login(LoginModel login)
        {
            try
            {
                return (userRepository.Login(login));
            }
            catch (Exception)
            {

                throw;
            }         
        }
    }
}
