using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;
namespace PasswordHashing.Models
{
    public class Hasher
    {
        public string Password { get; set; }
        public string HashedPassword { get; set; }
        public string Algorithm { get; set; }

        public Hasher() {}

        public void GetHashedPassword()
        {
            byte[] SaltBytes = null;

            Random r = new Random();
            int saltLength = r.Next(4, 16);

            SaltBytes = new byte[saltLength];

            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            rng.GetNonZeroBytes(SaltBytes);
            rng.Dispose();

            byte[] plainPassword = ASCIIEncoding.UTF8.GetBytes(Password);
            byte[] plainPasswordSalt = new byte[plainPassword.Length + SaltBytes.Length];

            for (int i = 0; i < plainPassword.Length; i++)
            {
                plainPasswordSalt[i] = plainPassword[i];
            }

            for (int i = 0; i < SaltBytes.Length; i++)
            {
                plainPasswordSalt[plainPassword.Length + i] = SaltBytes[i];
            }

            byte[] hashValue = null;

            switch (Algorithm)
            {
                case "SHA1":
                    SHA1Managed mySHA1 = new SHA1Managed();
                    hashValue = mySHA1.ComputeHash(plainPasswordSalt);
                    break;
                case "SHA256":
                    SHA256Managed mySHA256 = new SHA256Managed();
                    hashValue = mySHA256.ComputeHash(plainPasswordSalt);
                    break;
                case "SHA512":
                    SHA512Managed mySHA512 = new SHA512Managed();
                    hashValue = mySHA512.ComputeHash(plainPasswordSalt);
                    break;
            }


            byte[] result = new byte[hashValue.Length + SaltBytes.Length];

            for (int i = 0; i < hashValue.Length; i++)
            {
                result[i] = hashValue[i];
            }

            for (int i = 0; i < SaltBytes.Length; i++)
            {
                result[hashValue.Length + i] = SaltBytes[i];
            }

            this.HashedPassword = Convert.ToBase64String(result);

        }
    }
}
