using System;
using System.Text;

namespace RC4_Algorithm
{
    class Program
    {
        static void Main(string[] args)
        {
            //Convert String to Byte (ASCII)
            byte[] pass = Encoding.ASCII.GetBytes("test");
            byte[] plainText = Encoding.ASCII.GetBytes("mikail");

            // Encryption
            byte[] encryptedText = Encryption(pass, plainText);
            // Decryption
            byte[] decryptedText = Decryption(pass,encryptedText);
            // Print Console
            PrintConsole(encryptedText,decryptedText);

        }

        public static byte[] Encryption(byte[] pass, byte[] plainText)
        {
            int t, i, j, k, temp;
            int[] key, repo;
            byte[] cipherText;

            key = new int[256];
            repo = new int[256];
            cipherText = new byte[plainText.Length];

            for (i = 0; i < 256; i++)
            {
                key[i] = pass[i % pass.Length];
                repo[i] = i;
            }
            for (j = i = 0; i < 256; i++)
            {
                j = (j + repo[i] + key[i]) % 256;

                Swap(repo,i,j);
            }
            for (t = j = i = 0; i < plainText.Length; i++)
            {
                t = (t + 1) % 256;
                j = (j + repo[t]) % 256;
                
                Swap(repo,t,j);
                

                k = repo[((repo[t] + repo[j]) % 256)];
                cipherText[i] = (byte)(plainText[i] ^ k);
            }
            return cipherText;
        }

        public static byte[] Decryption(byte[] pass, byte[] plainText)
        {
            return Encryption(pass, plainText);
        }

        public static void Swap(int[] array, int number1, int number2)
        {
            int temp = array[number1];
            array[number1] = array[number2];
            array[number2] = temp;
        }

        public static void PrintConsole(byte[] encryptedText,byte[] decryptedText)
        {
            Console.WriteLine("------------------Encryption------------------");
            Console.WriteLine("--------Byte-------");
            foreach (byte item in encryptedText)
            {
                Console.Write(item + " - ");
            }
            Console.WriteLine();
            Console.WriteLine("--------String-----");
            foreach (byte item in encryptedText)
            {
                Console.Write((Convert.ToChar(item)).ToString() + " - ");
            }
            Console.WriteLine();
            Console.WriteLine("------------------Decrypt------------------");
            Console.WriteLine("--------Byte-------");
            foreach (byte item in decryptedText)
            {
                Console.Write(item + " - ");
            }
            Console.WriteLine();
            Console.WriteLine("--------String-----");
            foreach (byte item in decryptedText)
            {
                Console.Write((Convert.ToChar(item)).ToString() + " - ");
            }
            Console.ReadLine();

            
        }
    }
}
