using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB01
{
    public class Vigenere
    {
        public static String GenerateKey(String str,String key)
        {
            int x = str.Length;
            for (int i = 0; ; i++)
            {
                if (x == i)
                    i = 0;
                if (key.Length == str.Length)
                    break;
                key += (key[i]);
            }
            return key;
        }

        public static String CipherText(String str,String key)
        {
            String cipher_text = "";

            for (int i = 0; i < str.Length; i++)
            {
               
                int x = (str[i] + key[i]) % 26;

                x += 'A';

                cipher_text += (char)(x);
            }
            return cipher_text;
        }

        public static String FindOriginalText(String cipher_text,String key)
        {
            String orig_text = "";

            for (int i = 0; i < cipher_text.Length &&
                                    i < key.Length; i++)
            {
               
                int x = (cipher_text[i] -
                            key[i] + 26) % 26;

               
                x += 'A';
                orig_text += (char)(x);
            }
            return orig_text;
        }
    }
}
