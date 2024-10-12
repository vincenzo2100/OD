using LAB01;

Console.WriteLine(Ceaser.Encipher("czesc", 23));
String str = "TAJNYKOD".ToUpper();
String keyword = "HAGHS".ToUpper();

String key = Vigenere.GenerateKey(str, keyword);
String cipher_text = Vigenere.CipherText(str, key);

Console.WriteLine(cipher_text);
Console.WriteLine(Vigenere.FindOriginalText(cipher_text,key));
