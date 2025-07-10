using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DVLDConstant
{
    public static class clsEncryption
    {
        /// <summary>
        ///Provides methods for asymmetric encryption and decryption using Aes - use same key to encrypt and decrypt
        /// </summary>
        public static class clsSymmetricEncryption
        {
            /// <summary>
            /// Encrypts a plain text string using AES 128-bit encryption with a fixed IV (not secure for production).
            /// </summary>
            /// <param name="plainText">The plain text to encrypt.</param>
            /// <param name="key">The encryption key (must be 16 bytes - char for AES-128).</param>
            /// <returns>Base64-encoded encrypted string.</returns>
            public static string Encrypt(string plainText, string key)
            {
                using (Aes aesAlg = Aes.Create())
                {
                    // Set the key and IV for AES encryption
                    aesAlg.Key = Encoding.UTF8.GetBytes(key);
                    aesAlg.IV = new byte[aesAlg.BlockSize / 8];


                    // Create an encryptor
                    ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);


                    // Encrypt the data
                    using (var msEncrypt = new System.IO.MemoryStream())
                    {
                        using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                        using (var swEncrypt = new System.IO.StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plainText);
                        }


                        // Return the encrypted data as a Base64-encoded string
                        return Convert.ToBase64String(msEncrypt.ToArray());
                    }
                }
            }

            /// <summary>
            /// Decrypts a Base64-encoded AES 128-bit encrypted string using a fixed IV (must match Encrypt method).
            /// </summary>
            /// <param name="cipherText">The Base64-encoded encrypted text.</param>
            /// <param name="key">The same encryption key used in encryption.</param>
            /// <returns>The decrypted original plain text.</returns>
            public static string Decrypt(string cipherText, string key)
            {
                using (Aes aesAlg = Aes.Create())
                {
                    // Set the key and IV for AES decryption
                    aesAlg.Key = Encoding.UTF8.GetBytes(key);
                    aesAlg.IV = new byte[aesAlg.BlockSize / 8];


                    // Create a decryptor
                    ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);


                    // Decrypt the data
                    using (var msDecrypt = new System.IO.MemoryStream(Convert.FromBase64String(cipherText)))
                    using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    using (var srDecrypt = new System.IO.StreamReader(csDecrypt))
                    {
                        // Read the decrypted data from the StreamReader
                        return srDecrypt.ReadToEnd();
                    }
                }
            }
        }

    }
}
