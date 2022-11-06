using Core.CrossCuttingConcerns.Exceptions;
using Core.Security.Entities;
using RentACar.Application.Services.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Application.Features.Auths.Rules
{
    public class AuthBusinessRules
    {
        private readonly IUserRepository _userRepository;
        public AuthBusinessRules(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task EmailCanNotBeDuplicatedWhenRegistered(string email)
        {
            User? user = await _userRepository.GetAsync(x => x.Email == email);
            if (user != null) throw new BusinessException("Mail already exists");
        }
    }
}
