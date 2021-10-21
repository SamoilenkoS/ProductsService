using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;

namespace ProductsBusinessLayer.Services.HashService
{
    public class HashService : IHashService
    {
        private const string SaltString = "Xd4OjSmqa8n9/AnQIzgbVg==";

        public string HashString(string stringToHash)
        {
            byte[] salt = Convert.FromBase64String(SaltString);

            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: stringToHash,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));
        }
    }
}
