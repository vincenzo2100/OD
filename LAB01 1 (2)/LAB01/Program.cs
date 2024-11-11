using LAB01;

while (true)
{
    Console.WriteLine("\n--- Cipher Program ---");
    Console.WriteLine("1. Encrypt/Decrypt using Caesar Cipher");
    Console.WriteLine("2. Encrypt/Decrypt using Vigenere Cipher");
    Console.WriteLine("3. Decrypt using Ceaser Cracker");
    Console.WriteLine("0. Exit");
    Console.Write("Choose an option: ");

    if (!int.TryParse(Console.ReadLine(), out int choice))
    {
        Console.WriteLine("Invalid input. Please enter a number.");
        continue;
    }

    switch (choice)
    {
        case 0:
            Console.WriteLine("Exiting program...");
            return;

        case 1:
            Console.Write("Enter the text: ");
            string caesarText = Console.ReadLine();

            Console.Write("Enter the Caesar cipher key (integer): ");
            if (!int.TryParse(Console.ReadLine(), out int keyCeaser))
            {
                Console.WriteLine("Invalid key. Please enter a valid integer.");
                continue;
            }

            string encryptedMessageCeaser = CeaserCipher.Encrypt(caesarText, keyCeaser);
            string decryptedMessageCeaser = CeaserCipher.Decrypt(encryptedMessageCeaser, keyCeaser);


            Console.WriteLine($"\nCaesar encrypted: {encryptedMessageCeaser}");
            Console.WriteLine($"Caesar decrypted: {decryptedMessageCeaser}");
            break;
        case 2:
            Console.Write("Enter the text: ");
            string vigenereText = Console.ReadLine();

            Console.Write("Enter the Vigenere cipher key: ");
            string vigenereCipherKey = Console.ReadLine();

            string encryptedMessageVigenere = VigenereCipher.Encrypt(vigenereText, vigenereCipherKey);
            string decryptedMessageVigenere = VigenereCipher.Decrypt(encryptedMessageVigenere, vigenereCipherKey);

            Console.WriteLine($"\nVigenere encrypted: {encryptedMessageVigenere}");
            Console.WriteLine($"Vigenere decrypted: {decryptedMessageVigenere}");
            break;
        case 3:
            Console.Write("Enter the encrypted Ceaser cipher text: ");
            string message = Console.ReadLine();
            Console.Write("Enter amount of combinations: ");
            if (!int.TryParse(Console.ReadLine(), out int topN))
            {
                Console.WriteLine("Invalid key. Please enter a valid integer.");
                continue;
            }
            var probableDecryptions = CaesarCracker.BreakCaesarCipher(message, topN);
            Console.WriteLine("\nProbable Caesar decryptions:");
            foreach (var result in probableDecryptions)
                Console.WriteLine(result);
            break;
        default:
            Console.WriteLine("Invalid option, please try again.");
            break;
    }
}
