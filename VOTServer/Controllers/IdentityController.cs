using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using VOTServer.Core.Interface;
using VOTServer.Models;
using VOTServer.Options;
using VOTServer.Requests;
using VOTServer.Responses;

namespace VOTServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly IRepository<User> userRepository;
        private readonly IRepository<UserSecurity> userSecurityRepository;
        private readonly IOptions<JWTOptions> jwtOptions;

        public IdentityController(IUserRepository repository, IRepository<UserSecurity> repository1, IOptions<JWTOptions> options)
        {
            userRepository = repository;
            userSecurityRepository = repository1;
            jwtOptions = options;
        }

        [HttpPost("Login")]
        public async Task<LoginResponse> Login([FromBody] LoginRequest request)
        {
            if (request.UserId <= 0 || string.IsNullOrEmpty(request.Password))
            {
                Response.StatusCode = 400;
                return new LoginResponse { StatusCode = 400, Message = "Invalid user id or password." };
            }
            var passwordHash = Convert.ToBase64String(KeyDerivation.Pbkdf2(request.Password, Encoding.ASCII.GetBytes(jwtOptions.Value.SigningKey), KeyDerivationPrf.HMACSHA256, 1000, 256 / 8));
            var us = await userSecurityRepository.GetEntityByIdAsync(request.UserId);
            if (us == null || us.PasswordHash != passwordHash)
            {
                Response.StatusCode = 400;
                return new LoginResponse { StatusCode = 400, Message = "Invalid user id or password." };
            }
            var u = await userRepository.GetEntityByIdAsync(request.UserId);
            return new LoginResponse
            {
                Content = u,
                StatusCode = 200,
                Message = "OK",
                Token = GetJwtToken(u)
            };
        }

        [HttpGet("RefreshToken")]
        [Authorize]
        public async Task<LoginResponse> RefreshToken()
        {
            var id = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
            var lrt = User.Claims.FirstOrDefault(x => x.Type == "lrt");
            if (string.IsNullOrWhiteSpace(id.Value))
            {
                Response.StatusCode = 401;
                return new LoginResponse { StatusCode = 401, Message = "Unauthorized" };
            }
            // User cannot get new token when password has been changed.
            if (long.TryParse(id.Value, out long nid))
            {
                var u = await userRepository.GetEntityByIdAsync(nid);
                var us = await userSecurityRepository.GetEntityByIdAsync(nid);
                if (long.TryParse(lrt.Value, out long nlrt))
                {
                    if (DateTimeOffset.FromUnixTimeSeconds(nlrt).DateTime > us.PasswordUpdateTime)
                    {
                        return new LoginResponse
                        {
                            Content = null,
                            StatusCode = 200,
                            Message = "OK",
                            Token = GetJwtToken(u)
                        };
                    }
                }
            }
            Response.StatusCode = 401;
            return new LoginResponse { StatusCode = 401, Message = "Unauthorized" };
        }

        [HttpPost("ChangePassword")]
        public async Task<LoginResponse> ChangePassword([FromBody] ChangePasswordRequest request)
        {
            if (string.IsNullOrEmpty(request.Password) || request.Password.Length < 8)
            {
                Response.StatusCode = 400;
                return new LoginResponse { StatusCode = 400, Message = "Password need be longer than 8 characters." };
            }
            if (User.Identity.IsAuthenticated)
            {
                if (!User.IsInRole("Administrators") || User.HasClaim(x => x.Type == ClaimTypes.NameIdentifier && long.Parse(x.Value) != request.UserId))
                {
                    Response.StatusCode = 401;
                    return new LoginResponse { StatusCode = 401, Message = "You cannot change others password." };
                }
            }
            var us = await userSecurityRepository.GetEntityByIdAsync(request.UserId);
            if (us.PhoneNumber != request.PhoneNumber)
            {
                Response.StatusCode = 401;
                return new LoginResponse { StatusCode = 401, Message = "You cannot change others password." };
            }
            var passwordHash = Convert.ToBase64String(KeyDerivation.Pbkdf2(request.Password, Encoding.ASCII.GetBytes(jwtOptions.Value.SigningKey), KeyDerivationPrf.HMACSHA256, 1000, 256 / 8));
            us.PasswordHash = passwordHash;
            us.PasswordUpdateTime = DateTime.Now;
            await userSecurityRepository.UpdateAsync(us);
            var u = await userRepository.GetEntityByIdAsync(request.UserId);
            return new LoginResponse
            {
                Content = u,
                StatusCode = 200,
                Message = "OK",
                Token = GetJwtToken(u)
            };
        }

        [HttpPost("ChangePhone")]
        [Authorize]
        public async Task<LoginResponse> ChangePhone([FromBody] ChangePasswordRequest request)
        {
            if (string.IsNullOrEmpty(request.Password) || request.Password.Length < 8)
            {
                Response.StatusCode = 400;
                return new LoginResponse { StatusCode = 400, Message = "Password need be longer than 8 characters." };
            }
            if (User.Identity.IsAuthenticated)
            {
                if (!User.IsInRole("Administrators") || User.HasClaim(x => x.Type == ClaimTypes.NameIdentifier && long.Parse(x.Value) != request.UserId))
                {
                    Response.StatusCode = 401;
                    return new LoginResponse { StatusCode = 401, Message = "You cannot change others phone number." };
                }
            }
            var us = await userSecurityRepository.GetEntityByIdAsync(request.UserId);
            var passwordHash = Convert.ToBase64String(KeyDerivation.Pbkdf2(request.Password, Encoding.ASCII.GetBytes(jwtOptions.Value.SigningKey), KeyDerivationPrf.HMACSHA256, 1000, 256 / 8));
            if (us.PasswordHash != passwordHash)
            {
                Response.StatusCode = 401;
                return new LoginResponse { StatusCode = 401, Message = "You cannot change others phone number." };
            }
            us.PhoneNumber = request.PhoneNumber;
            us.PasswordUpdateTime = DateTime.Now;
            await userSecurityRepository.UpdateAsync(us);
            var u = await userRepository.GetEntityByIdAsync(request.UserId);
            return new LoginResponse
            {
                Content = u,
                StatusCode = 200,
                Message = "OK",
                Token = GetJwtToken(u)
            };
        }

        [HttpPost("Register")]
        public async Task<LoginResponse> Register([FromBody] RegisterRequest request)
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = 400;
                return new LoginResponse { StatusCode = 400, Message = "Bad Request. Check your information." };
            }
            var passwordHash = Convert.ToBase64String(KeyDerivation.Pbkdf2(request.Password, Encoding.ASCII.GetBytes(jwtOptions.Value.SigningKey), KeyDerivationPrf.HMACSHA256, 1000, 256 / 8));
            User u = new() { EmailAddress = request.EmailAddress, UserRoleId = 3, UserName = request.Username };
            await userRepository.AddAsync(u);
            UserSecurity us = new() { PasswordHash = passwordHash, PhoneNumber = request.Password, Id = u.Id };
            await userSecurityRepository.AddAsync(us);
            u = await userRepository.GetEntityByIdAsync(u.Id);
            return new LoginResponse
            {
                Content = u,
                StatusCode = 200,
                Message = "OK",
                Token = GetJwtToken(u)
            };
        }

        private string GetJwtToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, user.UserRole.Name),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim("lrt", DateTimeOffset.Now.ToUnixTimeSeconds().ToString())
            };
            var token = new JwtSecurityToken(
                issuer: jwtOptions.Value.Issuer,
                audience: jwtOptions.Value.Audience,
                claims: claims,
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Value.SigningKey)), SecurityAlgorithms.HmacSha256),
                expires: DateTime.Now.AddMonths(1));
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
