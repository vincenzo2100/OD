using System;

class SDES
{
    static int[] P10 = { 3, 5, 2, 7, 4, 10, 1, 9, 8, 6 };
    static int[] P8 = { 6, 3, 7, 4, 8, 5, 10, 9 };
    static int[] IP = { 2, 6, 3, 1, 4, 8, 5, 7 };
    static int[] IPInverse = { 4, 1, 3, 5, 7, 2, 8, 6 };
    static int[] EP = { 4, 1, 2, 3, 2, 3, 4, 1 };
    static int[] P4 = { 2, 4, 3, 1 };

    static int[,] S0 = {
        { 1, 0, 3, 2 },
        { 3, 2, 1, 0 },
        { 0, 2, 1, 3 },
        { 3, 1, 3, 2 }
    };

    static int[,] S1 = {
        { 0, 1, 2, 3 },
        { 2, 0, 1, 3 },
        { 3, 0, 1, 0 },
        { 2, 1, 0, 3 }
    };

    static int[] Key1 = new int[8];
    static int[] Key2 = new int[8];

    public static void Main(string[] args)
    {
        int[] key = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        int[] plaintext = { 1, 1, 1, 1, 1, 1, 1, 1 };

        GenerateKeys(key);

        Console.WriteLine("Klucz 1: " + string.Join("", Key1));
        Console.WriteLine("Klucz 2: " + string.Join("", Key2));

        int[] ciphertext = Encrypt(plaintext);
        Console.WriteLine("Zaszyfrowany text: " + string.Join("", ciphertext));

        int[] decrypted = Decrypt(ciphertext);
        Console.WriteLine("Odszyfrowany tekst: " + string.Join("", decrypted));
    }

    static void GenerateKeys(int[] key)
    {
        int[] permutedKey = Permute(key, P10);
        int[] left = LeftShift(permutedKey[..5], 1);
        int[] right = LeftShift(permutedKey[5..], 1);

        Key1 = Permute(Combine(left, right), P8);

        left = LeftShift(left, 2);
        right = LeftShift(right, 2);

        Key2 = Permute(Combine(left, right), P8);
    }

    static int[] Encrypt(int[] plaintext)
    {
        int[] permuted = Permute(plaintext, IP);
        int[] fk1Result = Fk(permuted, Key1);
        int[] swapped = Swap(fk1Result);
        int[] fk2Result = Fk(swapped, Key2);
        return Permute(fk2Result, IPInverse);
    }

    static int[] Decrypt(int[] ciphertext)
    {
        int[] permuted = Permute(ciphertext, IP);
        int[] fk2Result = Fk(permuted, Key2);
        int[] swapped = Swap(fk2Result);
        int[] fk1Result = Fk(swapped, Key1);
        return Permute(fk1Result, IPInverse);
    }

    static int[] Fk(int[] input, int[] subkey)
    {
        int[] left = input[..4];
        int[] right = input[4..];

        int[] expanded = Permute(right, EP);
        int[] xorResult = XOR(expanded, subkey);

        int[] sboxResult = SBox(xorResult[..4], S0).Concat(SBox(xorResult[4..], S1)).ToArray();
        int[] permuted = Permute(sboxResult, P4);

        return Combine(XOR(left, permuted), right);
    }

    static int[] SBox(int[] input, int[,] sbox)
    {
        int row = Convert.ToInt32($"{input[0]}{input[3]}", 2);
        int col = Convert.ToInt32($"{input[1]}{input[2]}", 2);
        int value = sbox[row, col];
        return new int[] { value / 2, value % 2 };
    }

    static int[] XOR(int[] a, int[] b)
    {
        return a.Zip(b, (x, y) => x ^ y).ToArray();
    }

    static int[] LeftShift(int[] input, int shifts)
    {
        int[] result = new int[input.Length];
        for (int i = 0; i < input.Length; i++)
            result[i] = input[(i + shifts) % input.Length];
        return result;
    }

    static int[] Permute(int[] input, int[] table)
    {
        return table.Select(i => input[i - 1]).ToArray();
    }

    static int[] Combine(int[] left, int[] right)
    {
        return left.Concat(right).ToArray();
    }

    static int[] Swap(int[] input)
    {
        return Combine(input[4..], input[..4]);
    }
}
