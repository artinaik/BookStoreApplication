using BookStoreModel.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStoreManager.Interface
{
    public interface IUserManager
    {
        public bool Register(UserRegistration user);
        public string Login(LoginModel login);
    }
}
