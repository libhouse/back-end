using System;
using System.Security.Cryptography;

namespace LibHouse.Infrastructure.Authentication.Helpers.String
{
    internal static class RandomStringGenerator
    {
        internal static string GenerateRandomString(int length)
        {
            RandomNumberGenerator randomGenerator = RandomNumberGenerator.Create();
            byte[] data = new byte[length];
            randomGenerator.GetBytes(data);
            return BitConverter.ToString(data);
        }
    }
}