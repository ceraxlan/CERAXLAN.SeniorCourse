﻿using Core.Security.Entities;
using RentACar.Application.Services.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Application.Services.UserService
{
    public class UserManager : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserManager(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User?> GetByEmail(string email)
        {
            User? user = await _userRepository.GetAsync(u => u.Email == email);
            return user;
        }

        public async Task<User> GetById(int id)
        {
            User? user = await _userRepository.GetAsync(u => u.Id == id);
            return user;
        }

        public async Task<User> Update(User user)
        {
            User updatedUser = await _userRepository.UpdateAsync(user);
            return updatedUser;
        }
    }
}
