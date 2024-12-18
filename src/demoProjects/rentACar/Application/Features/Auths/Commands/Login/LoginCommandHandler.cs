﻿using Core.Security.Entities;
using Core.Security.Enums;
using Core.Security.JWT;
using MediatR;
using RentACar.Application.Features.Auths.Dtos;
using RentACar.Application.Features.Auths.Rules;
using RentACar.Application.Services.AuthService;
using RentACar.Application.Services.UserService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Application.Features.Auths.Commands.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoggedDto>
    {
        private readonly IUserService _userService;
        private readonly IAuthService _authService;
        private readonly AuthBusinessRules _authBusinessRules;

        public LoginCommandHandler(IUserService userService, IAuthService authService, AuthBusinessRules authBusinessRules)
        {
            _userService = userService;
            _authService = authService;
            _authBusinessRules = authBusinessRules;
        }

        public async Task<LoggedDto> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            User? user = await _userService.GetByEmail(request.UserForLoginDto.Email);
            await _authBusinessRules.UserShouldBeExists(user);
            await _authBusinessRules.UserPasswordShouldBeMatch(user.Id, request.UserForLoginDto.Password);

            LoggedDto loggedDto = new();

            if (user.AuthenticatorType is not AuthenticatorType.None)
            {
                if (request.UserForLoginDto.AuthenticatorCode is null)
                {
                    await _authService.SendAuthenticatorCode(user);
                    loggedDto.RequiredAuthenticatorType = user.AuthenticatorType;
                    return loggedDto;
                }

                await _authService.VerifyAuthenticatorCode(user, request.UserForLoginDto.AuthenticatorCode);
            }

            AccessToken createdAccessToken = await _authService.CreateAccessToken(user);

            RefreshToken createdRefreshToken = await _authService.CreateRefreshToken(user, request.IPAddress);
            RefreshToken addedRefreshToken = await _authService.AddRefreshToken(createdRefreshToken);
            await _authService.DeleteOldRefreshTokens(user.Id);

            loggedDto.AccessToken = createdAccessToken;
            loggedDto.RefreshToken = addedRefreshToken;
            return loggedDto;
        }
    }
}
