using System;
using System.Security.Cryptography;
using System.Text;

namespace RTS.Services
{
    public static class PasswordHasher
    {
        public static string ComputeHash(string input)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
                return Convert.ToBase64String(bytes);
            }
        }

        public static bool VerifyHash(string input, string storedHash)
        {
            string hashOfInput = ComputeHash(input);
            return StringComparer.OrdinalIgnoreCase.Compare(hashOfInput, storedHash) == 0;
        }

        public static void GenerateAndDisplayHash(string password)
        {
            string hashedPassword = ComputeHash(password);
            Console.WriteLine($"Password: {password}");
            Console.WriteLine($"Hashed Password (SHA256): {hashedPassword}");
        }
    }
}