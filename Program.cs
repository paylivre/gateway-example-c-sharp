using System;
using System.Text;
using Isopoh.Cryptography.Argon2;
using Isopoh.Cryptography.SecureArray;

namespace gatewayExampleCSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            var password = "h97HSGfA7p0NHWUYM5HJW3v5ok8ne6Iwhttps://playground.gateway.paylivre.com/?merchant_id=99&operati on=5&email=john.doe@gmail.com&document=55913301005&merchant_transaction_id=pl12345&amount=43298& currency=USD&type=1&account_id=C1001&callback_url=https://www.jackpot-merchant.com&redirect_url= https://www.jackpot-merchant.com/thank-you";
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            byte[] salt = new byte[16];

            var Rng = System.Security.Cryptography.RandomNumberGenerator.Create();
            Rng.GetBytes(salt);

            var config = new Argon2Config
            {
                Type = Argon2Type.DataIndependentAddressing,
                Version = Argon2Version.Nineteen,
                TimeCost = 2,
                MemoryCost = 16,
                Lanes = 2,
                Threads = 2,
                Password = passwordBytes,
                Salt = salt,
            };

            var argon2A = new Argon2(config);
            string hashString;
            using (SecureArray<byte> hashA = argon2A.Hash())
            {
                hashString = config.EncodeString(hashA.Buffer);
            }
            Console.WriteLine(hashString);

        }
    }
}
