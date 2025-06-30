using ICDify.Application.DTOs.Auth;
using ICDify.Application.Interfaces;
using ICDify.Infrastructure.Persistence;
using ICDify.Infrastructure.Persistence.Entities;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace ICDify.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _db;
        private readonly IConfiguration _config;
        private readonly IPasswordHasher<UserEntity> _hasher;

        public AuthService(ApplicationDbContext db, IConfiguration config, IPasswordHasher<UserEntity> hasher)
        {
            _db = db;
            _config = config;
            _hasher = hasher;
        }

        public async Task<AuthResponse> RegisterAsync(RegisterRequest request)
        {
            var user = new UserEntity { Email = request.Email };
            user.PasswordHash = _hasher.HashPassword(user, request.Password);
            _db.Users.Add(user);
            await _db.SaveChangesAsync();
            return await GenerateToken(user);
        }

        public async Task<AuthResponse> LoginAsync(LoginRequest request)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Email == request.Email)
                       ?? throw new UnauthorizedAccessException("Invalid credentials.");

            var result = _hasher.VerifyHashedPassword(user, user.PasswordHash, request.Password);
            if (result == PasswordVerificationResult.Failed)
                throw new UnauthorizedAccessException("Invalid credentials.");

            return await GenerateToken(user);
        }

        private Task<AuthResponse> GenerateToken(UserEntity user)
        {
            var claims = new[]
            {
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_config["Jwt:Key"] ?? throw new InvalidOperationException("Jwt key missing"))
            );

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );

            return Task.FromResult(new AuthResponse(
                new JwtSecurityTokenHandler().WriteToken(token),
                user.Email,
                user.Role));
        }
    }
}
