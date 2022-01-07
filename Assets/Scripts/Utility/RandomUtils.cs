using System;
using System.Linq;

namespace Utility
{
    public static class RandomUtils
    {
        private static readonly Random Random = new Random();
        
        public static string RandomString(int length = 8)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[Random.Next(s.Length)]).ToArray());
        }
    }
}