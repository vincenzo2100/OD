using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LAB01
{
    public class CeaserCipher
    {
        public static string Encrypt(string text, int key)
        {
            string result = "";
            foreach (char c in text)
            {
                char d = char.IsUpper(c) ? 'A' : 'a';
                if (char.IsLetter(c))
                    result += (char)(((c + key - d) % 26) + d);
                else
                    result += c;
            }
            return result;
        }

        public static string Decrypt(string text, int key)
        {
             return Encrypt(text, 26 - key);
        }
    }
}