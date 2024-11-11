using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LAB01
{
    public class VigenereCipher
    {
        public static string Encrypt(string text, string key)
        {
            string result = "";
            key = key.ToUpper();
            int keyIndex = 0;

            foreach (char c in text)
            {
                if (char.IsLetter(c))
                {
                    char offset = char.IsUpper(c) ? 'A' : 'a';
                    int letterPos = (c - offset + (key[keyIndex % key.Length] - 'A')) % 26;
                    result += (char)(letterPos + offset);
                    keyIndex++;
                }
                else
                {
                    result += c;
                }
            }

            return result;
        }

        public static string Decrypt(string text, string key)
        {
            string result = "";
            key = key.ToUpper();
            int keyIndex = 0;

            foreach (char c in text)
            {
                if (char.IsLetter(c))
                {
                    char offset = char.IsUpper(c) ? 'A' : 'a';
                    int letterPos = (c - offset - (key[keyIndex % key.Length] - 'A') + 26) % 26;
                    result += (char)(letterPos + offset);
                    keyIndex++;
                }
                else
                {
                    result += c;
                }
            }

            return result;
        }
    }


}