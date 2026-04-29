using TaskManagementSystem.Data;
using TaskManagementSystem.API.Models;
using System.Security.Cryptography;
using System.Text;

namespace TaskManagementSystem.API.Data
{
    public static class DbSeeder
    {
        public static void Seed(AppDbContext context)
        {
            if (!context.Users.Any())
            {
                var admin = new User
                {
                    Username = "admin",
                    PasswordHash = HashPassword("123456"),
                    Role = "Admin"
                };

                context.Users.Add(admin);
                context.SaveChanges();
            }
        }

        private static string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();

            var bytes = Encoding.UTF8.GetBytes(password);

            var hash = sha256.ComputeHash(bytes);

            return Convert.ToBase64String(hash);
        }
    }
}