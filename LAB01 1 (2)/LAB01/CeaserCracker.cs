using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LAB01
{
    class CaesarCracker
    {
        private static readonly Dictionary<char, double> EnglishLetterFrequencies = new Dictionary<char, double>()
    {
        {'a', 8.12}, {'b', 1.49}, {'c', 2.71}, {'d', 4.32}, {'e', 12.02},
        {'f', 2.30}, {'g', 2.03}, {'h', 5.92}, {'i', 7.31}, {'j', 0.10},
        {'k', 0.69}, {'l', 3.98}, {'m', 2.61}, {'n', 6.95}, {'o', 7.68},
        {'p', 1.82}, {'q', 0.11}, {'r', 6.02}, {'s', 6.28}, {'t', 9.10},
        {'u', 2.88}, {'v', 1.11}, {'w', 2.09}, {'x', 0.17}, {'y', 2.11},
        {'z', 0.07}
    };

        public static Dictionary<int, string> BreakCaesarCipher(string cipherText, int topN)
        {
            Dictionary<int, double> shiftScores = new Dictionary<int, double>();

            for (int shift = 0; shift < 26; shift++)
            {
                string decryptedText = DecryptCaesar(cipherText, shift);
                double score = ScoreText(decryptedText);
                shiftScores.Add(shift, score);
            }

            var sortedShifts = shiftScores.OrderBy(x => x.Value).Take(topN);

            Dictionary<int, string> probableDecryptions = new Dictionary<int, string>();

            foreach (var shift in sortedShifts)
            {
                probableDecryptions.Add(shift.Key, DecryptCaesar(cipherText, shift.Key));
            }

            return probableDecryptions;
        }

        public static string DecryptCaesar(string cipherText, int shift)
        {
            char[] decrypted = new char[cipherText.Length];

            for (int i = 0; i < cipherText.Length; i++)
            {
                char c = cipherText[i];

                if (char.IsLetter(c))
                {
                    char offset = char.IsUpper(c) ? 'A' : 'a';
                    decrypted[i] = (char)((((c - offset) - shift + 26) % 26) + offset);
                }
                else
                {
                    decrypted[i] = c;
                }
            }

            return new string(decrypted);
        }

        public static double ScoreText(string text)
        {
            Dictionary<char, int> letterCount = new Dictionary<char, int>();

            foreach (char c in text)
            {
                if (char.IsLetter(c))
                {
                    char lowerC = char.ToLower(c);
                    if (letterCount.ContainsKey(lowerC))
                    {
                        letterCount[lowerC]++;
                    }
                    else
                    {
                        letterCount[lowerC] = 1;
                    }
                }
            }

            int totalLetters = letterCount.Values.Sum();

            double score = 0.0;
            foreach (var letter in EnglishLetterFrequencies)
            {
                double expectedFrequency = letter.Value / 100.0;
                double actualFrequency = letterCount.ContainsKey(letter.Key) ? (double)letterCount[letter.Key] / totalLetters : 0;
                score += Math.Abs(expectedFrequency - actualFrequency);
            }

            return score;
        }
    }
}