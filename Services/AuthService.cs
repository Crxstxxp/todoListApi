using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using todoListApi.Models;
using todoListApi.DTOs;

namespace todoListApi.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<Users> _userManager;
        private readonly SignInManager<Users> _signInManager;
        private readonly IConfiguration _configuration;

        //private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AuthService(UserManager<Users> userManager, SignInManager<Users> signInManager, IConfiguration configuration, IMapper mapper)
        {
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        public async Task<AuthenticationResult> Register(RegisterRequest request)
        {
            var user = _mapper.Map<Users>(request);

            var existingUser = await _userManager.FindByEmailAsync(request.Email);
            if (existingUser != null)
            {
                return new AuthenticationResult
                {
                    Errors = ["El correo electrónico ya está registrado."]
                }; 
            }

            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
            {
                return new AuthenticationResult
                {
                    Errors = result.Errors.Select(e => e.Description)
                };
            }

            return new AuthenticationResult
            {
                Token = GenerateJwtToken(user)
            };
        }

        public async Task<AuthenticationResult> Login(LoginRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
                return new AuthenticationResult { Errors = ["Credenciales incorrectas"] };

            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            
            if (!result.Succeeded)
                return new AuthenticationResult { Errors = ["Credenciales incorrectas"] };

            return new AuthenticationResult { Token = GenerateJwtToken(user) };
        }

        private string GenerateJwtToken(Users user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id),
            new Claim(JwtRegisteredClaimNames.Email, user.Email!),
            new Claim("FullName", user.FullName)
        };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}