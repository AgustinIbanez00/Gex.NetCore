using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Gex.Extensions.Hashing;
public static class HashingExtensions
{
    public static HashedPassword Hash(this string password)
    {
        byte[] salt = new byte[128 / 8];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(salt);
        }
        string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 10000,
            numBytesRequested: 256 / 8));
        return new HashedPassword() { Password = hashed, Salt = Convert.ToBase64String(salt) };
    }

    public static bool CheckHash(string attemptedPassword, string hash, string salt)
    {
        string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
             password: attemptedPassword,
             salt: Convert.FromBase64String(salt),
             prf: KeyDerivationPrf.HMACSHA256,
             iterationCount: 10000,
             numBytesRequested: 256 / 8));
        return hash == hashed;
    }

    public static byte[] GetHash(string password, string salt)
    {
        byte[] unhashedBytes = Encoding.Unicode.GetBytes(string.Concat(salt, password));
        SHA256Managed sha256 = new();
        byte[] hashedBytes = sha256.ComputeHash(unhashedBytes);
        return hashedBytes;
    }
}

