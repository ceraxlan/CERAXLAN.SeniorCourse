using Core.Persistence.Paging;
using Core.Security.Entities;
using Core.Security.JWT;
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
    }
}
