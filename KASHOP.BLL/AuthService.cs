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

namespace KASHOP.BLL
{
    public class AuthService : IAuthService
    { 
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _config;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<ApplicationUser> _signinManager;
        private readonly IEmailService _emailSender;
        public AuthService(UserManager<ApplicationUser> userManager, IConfiguration config, ITokenService tokenService, SignInManager<ApplicationUser> signinManager,IEmailService emailSender)
        {
            _userManager = userManager;
            _config = config;
            _tokenService = tokenService;
            _signinManager = signinManager;
            _emailSender = emailSender;
        }
        public async Task<(bool success, List<string> errors)> RegisterAsync(RegisterRequest registerRequest)
        {
            var user=registerRequest.Adapt<ApplicationUser>();
            var result = await _userManager.CreateAsync(user, registerRequest.Password);

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var encodedToken = Uri.EscapeDataString(token);
            var confirmationLink = $"https://localhost:7139/api/auth/confirm-email?userId={user.Id}&token={encodedToken}";

           
            await _emailSender.SendEmailAsync(user.Email, "Confirm your email",
                "<div>" +
                $"<h1>Welcome to KASHOP!</h1>" +
                $"<p>Please confirm your email by clicking " +
                $"<a href='{confirmationLink}'>here</a>.</p>" +
                "</div>");
            Console.WriteLine($"Confirmation link: {confirmationLink}");
            if (!result.Succeeded)
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

        public async Task<(bool success, List<string> errors)> ConfirmEmailAsync(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return (false, new List<string> { "User not found." });
            }
            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (!result.Succeeded)
            {
                return (false, result.Errors.Select(e => e.Description).ToList());
            }
            return (true, new List<string>());
        }

    }
}
