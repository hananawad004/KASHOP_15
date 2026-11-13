using KASHOP.DAL.DTO.Requests;
using KASHOP.DAL.DTO.Responses;
using KASHOP.DAL.Models;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace KASHOP.BLL.Settings
{
    public class AuthService : IAuthService
    { 
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _config;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<ApplicationUser> _signinManager;
        public AuthService(UserManager<ApplicationUser> userManager, IConfiguration config, ITokenService tokenService, SignInManager<ApplicationUser> signinManager)
        {
            _userManager = userManager;
            _config = config;
            _tokenService = tokenService;
            _signinManager = signinManager;
        }
        public async Task<(bool success, List<string> errors)> RegisterAsync(RegisterRequest registerRequest)
        {
            var user=registerRequest.Adapt<ApplicationUser>();
            var result = await _userManager.CreateAsync(user, registerRequest.Password);
           if(!result.Succeeded)
            {
                return (false, result.Errors.Select(e => e.Description).ToList());

            }
            await _userManager.AddToRoleAsync(user, "User");
            return ((true, []));
            
        }
       public async Task<(bool success, List<string> errors,LoginResponse)> LoginAsync(LoginRequest loginRequest)
        {
            var user = await _userManager.FindByEmailAsync(loginRequest.Email);

            if (user == null)
            {
                return (false, ["Invalid email or password."], null);
            }
            var result = await _signinManager.CheckPasswordSignInAsync(user, loginRequest.Password, true);
            if(result.IsNotAllowed)
            {
                return (false, ["Email confirmation is required."], null);
            }
            if (result.IsLockedOut)
            {
                return (false, ["User account is locked out."], null);
            }
            if(!result.Succeeded)
            {
                return (false, ["Invalid email or password."], null);
            }   
            var token = await _tokenService.GenerateTokenAsync(user);
            var response = new LoginResponse
            {

                Token = token,
            };

            return (true, new List<string>(), response);
        }
      
    }
}
