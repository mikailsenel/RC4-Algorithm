using System;
using System.Security;
using System.Text;

namespace RC4_Algorithm
{
    class Program
    {
        private byte[] key, text;
        private byte[] keyBox = new byte[256];
        private int[] sBox = new int[256];
        private int[] random;

        public Program(byte[] text, byte[] key)
        {
            this.text = text;
            this.key = key;
            this.random = new int[text.Length];
        }

        static void Main(string[] args)
        {
            Console.Write("Enter text: ");
            string t = Console.ReadLine();
            Console.Write("Enter key: ");
            string k = Console.ReadLine();
            Program program = new Program
                (Encoding.ASCII.GetBytes(t), Encoding.ASCII.GetBytes(k));
            program.Initialize();
            program.KSA();      //Key - scheduling algorithm
            program.PRGA();     //Pseudo - random generation algorithm

            Console.Write("\n1) Encryption \n2) Decryption \nSelection: ");
            string secim = Console.ReadLine();
            switch (secim)
            {
                case "1":
                    byte[] cipherText = program.Encryption();           //Encryption Call
                    Console.WriteLine("\nText is encrypted.\n\n");
                    program.PrintScreen(cipherText);
                    break;
                case "2":
                    byte[] solvedText = program.Decryption();           //Decryption Call
                    Console.WriteLine("\nText is Decrypted.\n\n");
                    program.PrintScreen(solvedText);
                    break;
            }
        }

        public void Initialize()
        {
            //Bu metodda keyden 256 adet türetiyoruz.
            for (int i = 0; i < keyBox.Length; i++)
            {
                keyBox[i] = key[i % key.Length];
                sBox[i] = i;
            }
        }

        /// <summary>
        ///             Key-scheduling algorithm 
        /// </summary>
        public void KSA()
        {
            int j = 0;
            for (int i = 0; i < keyBox.Length; i++)
            {
                j = (j + sBox[i] + keyBox[i]) % 256;
                sBox = Swap(i, j, sBox);
            }
        }

        /// <summary>
        ///             Pseudo-random generation algorithm 
        /// </summary>
        public void PRGA() 
        {
            int j = 0, k = 0;
            for (int i = 0; i < text.Length; i++)
            {
                j = (j + 1) % 256;
                k = (k + sBox[j]) % 256;
                sBox = Swap(k, j, sBox);
                random[i] = sBox[(sBox[j] + sBox[k]) % 256];
            }
        }

        public int[] Swap(int a, int b, int[] tempBox)
        {

            int temp = tempBox[a];
            tempBox[a] = tempBox[b];
            tempBox[b] = temp;
            return tempBox;
        }

        public byte[] Encryption()
        {
            byte[] cipherText = new byte[text.Length];
            for (int i = 0; i < text.Length; i++)
            {
                cipherText[i] = (byte)(random[i] ^ text[i]);
            }

            return cipherText;
        }

        public byte[] Decryption()
        {
            byte[] solvedText = new byte[text.Length];
            for (int i = 0; i < text.Length; i++)
            {
                solvedText[i] = (byte)(random[i] ^ text[i]);
            }

            return solvedText;
        }

        public void PrintScreen(byte[] printText)
        {
            Console.Write("Result: \nBase64: ");
            Console.WriteLine(Convert.ToBase64String(printText));
            Console.Write("String: "+Encoding.ASCII.GetString(printText));
            Console.ReadLine();
        }
    }
}
