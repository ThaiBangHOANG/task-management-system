using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.API.Models;
using TaskManagementSystem.API.Services;
using TaskManagementSystem.Data;

public class AuthService
{
    private readonly AppDbContext _context;

    private readonly JwtService _jwtService;

    public AuthService(
        AppDbContext context,
        JwtService jwtService
    )
    {
        _context = context;
        _jwtService = jwtService;
    }

    public async Task Register(string email, string password)
    {
        var user = new User
        {
            Email = email,
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
                    u => u.Email == request.Email
                );

        Console.WriteLine("User found: " + (user != null));

        if (user != null)
        {
            var valid = BCrypt.Net.BCrypt.Verify(
                request.Password,
                user.PasswordHash
            );

            Console.WriteLine("Password valid: " + valid);
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