using Domain.Dtos;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.Services
{
    public class AuthenticationServices:IAuthenticationServices
    {
        private readonly IConfiguration _configuration;

        private readonly IUsersRepository _usersRepository;
        public AuthenticationServices
        (
            IUsersRepository usersRepository,
            IConfiguration configuration
        ) 
        { 
            _usersRepository = usersRepository;
            _configuration = configuration;
        }
        public string Login(LoginRequestDto request) 
        {
            try
            {
                var user = _usersRepository
                        .Find(u => u.UserName == request.UserName && u.Password == request.Password)
                        .FirstOrDefault();
                if (user == null) return "404";
                return JwtServices.JwtToken(user.Id.ToString(), _configuration);
            }
            catch (Exception ex)
            {
                return $"500: {ex.Message}";
            }
        }
    }
}
