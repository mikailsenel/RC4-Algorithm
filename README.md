Rivest Cipher 4 Algorithm (RC4)
=================

<!--ts-->
   * [Construction](#construction)
   * [Use](#use)
      * [KSA Call](#use)
      * [PRGA Call](#use)
      * [Encryption Call](#use)
      * [Decryption Call](#use)
      * [Print Console Call](#use)
   * [Initialize](#initialize)
   * [KSA (Key-scheduling algorithm)](#ksa-key-scheduling-algorithm)
   * [PRGA (Pseudo-random generation algorithm)](#prga-pseudo-random-generation-algorithm)
   * [Swap](#swap)
   * [Encryption](#encryption)
   * [Decryption](#decryption)
   * [Print Console](#print-console)
   * [Screenshot](#screenshot)
<!--te-->

Construction
============
```csharp
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
```
Use
============
```csharp
            Program program = new Program (Encoding.ASCII.GetBytes(t), Encoding.ASCII.GetBytes(k));
            program.Initialize();
            program.KSA();      //Key - scheduling algorithm
            program.PRGA();     //Pseudo - random generation algorithm
			
            byte[] cipherText = program.Encryption();           //Encryption Call
	    program.PrintConsole(cipherText);                   //Print Console
	    
	    byte[] solvedText = program.Decryption();           //Decryption Call
	    program.PrintConsole(solvedText);                   //Print Console
```
Initialize
============
```csharp
        public void Initialize()
        {
            //Bu metodda keyden 256 adet türetiyoruz.
            for (int i = 0; i < keyBox.Length; i++)
            {
                keyBox[i] = key[i % key.Length];
                sBox[i] = i;
            }
        }
```
KSA (Key-scheduling algorithm)
============
```csharp
        public void KSA()
        {
            int j = 0;
            for (int i = 0; i < keyBox.Length; i++)
            {
                j = (j + sBox[i] + keyBox[i]) % 256;
                sBox = Swap(i, j, sBox);
            }
        }
```
PRGA (Pseudo-random generation algorithm)
============
```csharp
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
```
Swap 
============
```csharp
         public int[] Swap(int a, int b, int[] tempBox)
        {

            int temp = tempBox[a];
            tempBox[a] = tempBox[b];
            tempBox[b] = temp;
            return tempBox;
        }
```
Encryption
============
```csharp
         public byte[] Encryption()
        {
            byte[] cipherText = new byte[text.Length];
            for (int i = 0; i < text.Length; i++)
            {
                cipherText[i] = (byte)(random[i] ^ text[i]);
            }

            return cipherText;
        }
```
Decryption
============
```csharp
         public byte[] Decryption()
        {
            byte[] solvedText = new byte[text.Length];
            for (int i = 0; i < text.Length; i++)
            {
                solvedText[i] = (byte)(random[i] ^ text[i]);
            }

            return solvedText;
        }
```
Print Console
============
```csharp
        public void PrintConsole(byte[] printText)
        {
            Console.Write("Result: \nBase64: ");
            Console.WriteLine(Convert.ToBase64String(printText));
            Console.Write("String: "+Encoding.ASCII.GetString(printText));
            Console.ReadLine();
        }
        }
```
Screenshot
============
![](https://i.ibb.co/CWwp572/screenshot.png)
