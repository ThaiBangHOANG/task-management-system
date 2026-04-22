using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.API.Models;
using TaskManagementSystem.Data;

public class AuthService
{
    private readonly AppDbContext _context;

    public AuthService(AppDbContext context)
    {
        _context = context;
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

    public async Task<User?> Login(string email, string password)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(x => x.Email == email);

        if (user == null)
            return null;

        var valid = BCrypt.Net.BCrypt.Verify(
            password,
            user.PasswordHash
        );

        if (!valid)
            return null;

        return user;
    }
}