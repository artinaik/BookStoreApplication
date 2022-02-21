using BookStoreModel.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStoreRepository.Interface
{
    public interface IUserRepository
    {
        public bool Register(UserRegistration user);
        public string Login(LoginModel login);
    }
}
