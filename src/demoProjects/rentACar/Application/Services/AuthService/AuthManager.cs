using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Core.Security.Entities;
using Core.Security.Enums;
using Core.Security.JWT;
using Core.Security.OtpAuthenticator;
using Microsoft.EntityFrameworkCore;
using RentACar.Application.Services.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Application.Services.AuthService
{
    public class AuthManager : IAuthService
    {
        private readonly IUserOperationClaimRepository _userOperationClaimRepository;
        private readonly ITokenHelper _tokenHelper; 
        private readonly IRefreshTokenRepository _refreshTokenRepository;

        public AuthManager(IUserOperationClaimRepository userOperationClaimRepository, ITokenHelper tokenHelper, IRefreshTokenRepository refreshTokenRepository)
        {
            _userOperationClaimRepository = userOperationClaimRepository;
            _tokenHelper = tokenHelper;
            _refreshTokenRepository = refreshTokenRepository;
        }

        public async Task<RefreshToken> AddRefreshToken(RefreshToken refreshToken)
        {
            RefreshToken addedRefreshToken = await _refreshTokenRepository.AddAsync(refreshToken);
            return addedRefreshToken;
        }
        public async Task<AccessToken> CreateAccessToken(User user)
        {
           IPaginate<UserOperationClaim> userOperationClaims = 
                await _userOperationClaimRepository.GetListAsync(x=>x.UserId == user.Id,include:x=>x.Include(x=>x.OperationClaim));
           IList<OperationClaim> operationClaims = 
                userOperationClaims.Items.Select(x=> new OperationClaim { Id = x.OperationClaim.Id, Name = x.OperationClaim.Name}).ToList();
            AccessToken accessToken = _tokenHelper.CreateToken(user, operationClaims);
            return accessToken;
        }
        public async Task<RefreshToken> CreateRefreshToken(User user, string ipAddress)
        {
            RefreshToken refreshToken = _tokenHelper.CreateRefreshToken(user, ipAddress);
            return await Task.FromResult(refreshToken);
        }
        public async Task SendAuthenticatorCode(User user)
        {
            if (user.AuthenticatorType is AuthenticatorType.Email) await SendAuthenticatorCodeWithEmail(user);
        }
        public async Task DeleteOldRefreshTokens(int userId)
        {
            //IList<RefreshToken> refreshTokens = (await _refreshTokenRepository.GetListAsync(r =>
            //                                         r.UserId == userId &&
            //                                         r.Revoked == null && r.Expires >= DateTime.UtcNow &&
            //                                         r.Created.AddDays(_tokenOptions.RefreshTokenTTL) <=
            //                                         DateTime.UtcNow)
            //                                    ).Items;
            //foreach (RefreshToken refreshToken in refreshTokens) await _refreshTokenRepository.DeleteAsync(refreshToken);
        }
        public async Task VerifyAuthenticatorCode(User user, string authenticatorCode)
        {
            if (user.AuthenticatorType is AuthenticatorType.Email)
                await VerifyAuthenticatorCodeWithEmail(user, authenticatorCode);
            else if (user.AuthenticatorType is AuthenticatorType.Otp)
                await VerifyAuthenticatorCodeWithOtp(user, authenticatorCode);
        }
       
        private async Task SendAuthenticatorCodeWithEmail(User user)
        {
            //EmailAuthenticator emailAuthenticator = await _emailAuthenticatorRepository.GetAsync(e => e.UserId == user.Id);

            //if (!emailAuthenticator.IsVerified) throw new BusinessException("Email Authenticator must be is verified.");

            //string authenticatorCode = await _emailAuthenticatorHelper.CreateEmailActivationCode();
            //emailAuthenticator.ActivationKey = authenticatorCode;
            //await _emailAuthenticatorRepository.UpdateAsync(emailAuthenticator);

            //var toEmailList = new List<MailboxAddress>
            //{
            //    new($"{user.FirstName} {user.LastName}",user.Email)
            //};

            //_mailService.SendMail(new Mail
            //{
            //    ToList = toEmailList,
            //    Subject = "Authenticator Code - RentACar",
            //    TextBody = $"Enter your authenticator code: {authenticatorCode}"
            //});
        }
        private async Task VerifyAuthenticatorCodeWithEmail(User user, string authenticatorCode)
        {
            //EmailAuthenticator emailAuthenticator = await _emailAuthenticatorRepository.GetAsync(e => e.UserId == user.Id);

            //if (emailAuthenticator.ActivationKey != authenticatorCode)
            //    throw new BusinessException("Authenticator code is invalid.");

            //emailAuthenticator.ActivationKey = null;
            //await _emailAuthenticatorRepository.UpdateAsync(emailAuthenticator);
        }

        private async Task VerifyAuthenticatorCodeWithOtp(User user, string authenticatorCode)
        {
            //OtpAuthenticator otpAuthenticator = await _otpAuthenticatorRepository.GetAsync(e => e.UserId == user.Id);

            //bool result = await _otpAuthenticatorHelper.VerifyCode(otpAuthenticator.SecretKey, authenticatorCode);

            //if (!result)
            //    throw new BusinessException("Authenticator code is invalid.");
        }
    }
}
