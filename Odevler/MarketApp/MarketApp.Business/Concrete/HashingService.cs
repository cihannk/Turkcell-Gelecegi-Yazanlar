using MarketApp.Business.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace MarketApp.Business.Concrete
{
    public class HashingService : IHashingService
    {
        public byte[] ProduceSalt(int size)
        {
            byte[] salt = new byte[size / 8];
            using (var rngCsp = new RNGCryptoServiceProvider())
            {
                rngCsp.GetNonZeroBytes(salt);
            }
            return salt;
        }
        public byte[] Hash(string password, byte[] salt)
        {
            
            byte[] hashed = KeyDerivation.Pbkdf2(
            password: password,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 100000,
            numBytesRequested: 256 / 8);

            return hashed;
        }
        public bool CompareHash(string password, byte[] hash, byte[] salt)
        {
            var hashedValue = Hash(password, salt);
            if (hashedValue.SequenceEqual(hash))
            {
                return true;
            }
            return false;
        }
    }
}
