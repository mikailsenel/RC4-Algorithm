# Rivest Cipher 4 Algorithm (RC4)


#### **Use**
```csharp
// Encryption
 byte[] encryptedText = Encryption(pass, plainText);
// Decryption
byte[] decryptedText = Decryption(pass,encryptedText);
// Print Screen
PrintScreen(encryptedText,decryptedText);

```
#### **Convert String to Byte**
```csharp
byte[] pass = Encoding.ASCII.GetBytes("test");
byte[] plainText = Encoding.ASCII.GetBytes("mikail");
```
