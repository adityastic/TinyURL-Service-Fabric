using System;
using System.Text;

namespace TinyURLStatefulService
{
    public static class TinyURLUtils
    {
        private static readonly char[] CharacterMap =
            "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789".ToCharArray();

        public static string IdToShortURL(int n)
        {
            var ShortURL = new StringBuilder();
            while (n > 0)
            {
                ShortURL.Append(CharacterMap[n % 62]);
                n /= 62;
            }

            return Reverse(ShortURL.ToString());
        }

        public static string Reverse(string s)
        {
            var charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

        public static int ShortURLtoID(string shortURL)
        {
            var id = 0;
            for (var i = 0; i < shortURL.Length; i++)
            {
                if ('a' <= shortURL[i] &&
                    shortURL[i] <= 'z')
                    id = id * 62 + shortURL[i] - 'a';
                if ('A' <= shortURL[i] &&
                    shortURL[i] <= 'Z')
                    id = id * 62 + shortURL[i] - 'A' + 26;
                if ('0' <= shortURL[i] &&
                    shortURL[i] <= '9')
                    id = id * 62 + shortURL[i] - '0' + 52;
            }

            return id;
        }
    }
}