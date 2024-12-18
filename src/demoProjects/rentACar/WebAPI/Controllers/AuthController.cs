﻿using Core.Security.Dtos;
using Core.Security.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentACar.Application.Features.Auths.Commands.Login;
using RentACar.Application.Features.Auths.Commands.Register;
using RentACar.Application.Features.Auths.Dtos;

namespace RentACar.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseController
    {
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] UserForRegisterDto userForRegisterDto)
        {
            RegisterCommand registerCommand = new()
            {
                UserForRegisterDto = userForRegisterDto,
                IdAddress = GetIpAddress()
            };
            RegisteredDto result = await Mediator.Send(registerCommand);
            SetRefreshTokenToCookie(result.RefreshToken);
            return Created("", result.AccessToken);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserForLoginDto userForLoginDto)
        {
            LoginCommand loginCommand = new() { UserForLoginDto = userForLoginDto, IPAddress = GetIpAddress() };
            LoggedDto result = await Mediator.Send(loginCommand);

            if (result.RefreshToken is not null) SetRefreshTokenToCookie(result.RefreshToken);

            return Ok(result.CreateResponseDto());
        }
        private void SetRefreshTokenToCookie(RefreshToken refreshToken)
        {
            CookieOptions cookieOptions = new() { HttpOnly = true ,Expires = DateTime.Now.AddDays(7)};
            Response.Cookies.Append("refreshToken", refreshToken.Token, cookieOptions);
        }
    }
}
