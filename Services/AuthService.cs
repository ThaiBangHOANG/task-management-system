using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.API.DTOs.Auth;
using TaskManagementSystem.API.Models;
using TaskManagementSystem.API.Services;
using TaskManagementSystem.Data;

public class AuthService
{
    private readonly AppDbContext _context;

    private readonly JwtService _jwtService;

    private readonly ILogger<AuthService> _logger;

    public AuthService(
        AppDbContext context,
        JwtService jwtService,
        ILogger<AuthService> logger 
    )
    {
        _context = context;
        _jwtService = jwtService;
        _logger = logger;
    }

    public async Task Register(string username, string password)
    {
        var user = new User
        {
            Username = username,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(password)
        };

        _context.Users.Add(user);

        await _context.SaveChangesAsync();
    }

    public async Task<string?> Login(LoginRequest request)
    {
        var user =
            await _context.Users
                .FirstOrDefaultAsync(
                    u => u.Username == request.Username
                );

        _logger.LogInformation("Username: {Username}", request.Username);
        _logger.LogInformation("Password: {Password}", request.Password);
        _logger.LogInformation("User found: {UserFound}", user != null);

        if (user != null)
        {
            var valid = BCrypt.Net.BCrypt.Verify(
                request.Password,
                user.PasswordHash
            );

            _logger.LogInformation("Password valid: {PasswordValid}", valid);
        }

        if (user == null)
            return null;

        var isPasswordValid =
            BCrypt.Net.BCrypt.Verify(
                request.Password,
                user.PasswordHash
            );

        if (!isPasswordValid)
            return null;

        return _jwtService.GenerateToken(user);
    }
}