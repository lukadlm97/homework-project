﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Homework.Enigmatry.Logging.Shared.Contracts;
using Homework.Enigmatry.Shop.Application.Constants;
using Homework.Enigmatry.Shop.Application.Contracts;
using Homework.Enigmatry.Shop.Application.Models;
using Homework.Enigmatry.Shop.Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Homework.Enigmatry.Shop.Infrastructure.Services.Token
{
    public class TokenService:ITokenService
    {
        private readonly TokenSettings _tokenSettings;
        private readonly LogTraceData _logTraceData;

        public TokenService(IOptions<TokenSettings> options,LogTraceData logTraceData)
        {
            _tokenSettings = options.Value;
            _logTraceData = logTraceData;
        }
        public string CreateToken(Customer customer, string role)
        {
            _logTraceData.RequestPath.Add(string.Format("{0} -> {1}", nameof(TokenService), nameof(CreateToken)));

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes(_tokenSettings.JwtSecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(Constants.UserIdLabel, customer.Id.ToString()),
                    new Claim(ClaimTypes.Name, customer.Username),
                    new Claim(ClaimTypes.Role, role),
                }),
                Expires = DateTime.UtcNow.Add(_tokenSettings.AccessTokenExpireTime),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
