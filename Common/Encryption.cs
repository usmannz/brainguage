using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SampleProject.Common
{
public class Encryption
    {
        private static System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
        private static byte[] key = { 7, 3, 4, 5, 6, 7, 8, 9, 10, 11, 23, 16, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25 };
        private static byte[] iv = { 95, 210, 210, 33, 111, 123, 123, 234 };

        /// <summary>
        /// Convert Hexadecimal String To Byte Array
        /// </summary>
        /// <param name="HexString">Hexadecimal String</param>
        /// <returns>Byte Array</returns>
        private static byte[] HexStringToByteArray(string HexString)
        {
            if (HexString == null || HexString.Length == 0) return null;
            //if the string length is odd, then last digit will get a zero prepended to it.
            if (HexString.Length % 2 == 1) HexString = HexString.Insert(HexString.Length, "0");
            byte[] bytes = new byte[HexString.Length / 2];
            for (int i = 0; i <= HexString.Length - 1; i += 2)
            {
                bytes[i / 2] = Convert.ToByte(HexString.Substring(i, 2), 16);
            }
            return bytes;
        }
        /// <summary>
        /// Encrypt the plain text
        /// </summary>
        /// <param name="plainText">Plain Text</param>
        /// <returns></returns>
        public static string Encrypt(string plainText)
        {
            if (string.IsNullOrEmpty(plainText))
                return string.Empty;

            // Declare a UTF8Encoding object so we may use the GetByte
            // method to transform the plainText into a Byte array.
            UTF8Encoding utf8encoder = new UTF8Encoding();
            byte[] inputInBytes = utf8encoder.GetBytes(plainText);

            // Create a new TripleDES service provider
            TripleDESCryptoServiceProvider tdesProvider = new TripleDESCryptoServiceProvider();

            // The ICryptTransform interface uses the TripleDES
            // crypt provider along with encryption key and init vector
            // information
            ICryptoTransform cryptoTransform = tdesProvider.CreateEncryptor(key, iv);

            // All cryptographic functions need a stream to output the
            // encrypted information. Here we declare a memory stream
            // for this purpose.
            MemoryStream encryptedStream = new MemoryStream();
            CryptoStream cryptStream = new CryptoStream(encryptedStream, cryptoTransform, CryptoStreamMode.Write);

            // Write the encrypted information to the stream. Flush the information
            // when done to ensure everything is out of the buffer.
            cryptStream.Write(inputInBytes, 0, inputInBytes.Length);
            cryptStream.FlushFinalBlock();
            encryptedStream.Position = 0;

            // Read the stream back into a Byte array and return it to the calling
            // method.
            byte[] result = new byte[encryptedStream.Length];
            encryptedStream.Read(result, 0, (int)encryptedStream.Length);
            cryptStream.Close();
            return BitConverter.ToString(result).Replace("-", "");
        }

        /// <summary>
        /// Decrypt the encrypted data
        /// </summary>
        /// <param name="pEncryptedHexString">Encrypted Hex String</param>
        /// <returns></returns>
        public static string Decrypt(string pEncryptedHexString)
        {
            if (string.IsNullOrEmpty(pEncryptedHexString))
                return string.Empty;

            byte[] inputInBytes = HexStringToByteArray(pEncryptedHexString);

            TripleDESCryptoServiceProvider tdesProvider = new TripleDESCryptoServiceProvider();

            // As before we must provide the encryption/decryption key along with
            // the init vector.
            ICryptoTransform cryptoTransform = tdesProvider.CreateDecryptor(key, iv);

            // Provide a memory stream to decrypt information into
            MemoryStream decryptedStream = new MemoryStream();
            CryptoStream cryptStream = new CryptoStream(decryptedStream, cryptoTransform, CryptoStreamMode.Write);
            cryptStream.Write(inputInBytes, 0, inputInBytes.Length);
            cryptStream.FlushFinalBlock();
            decryptedStream.Position = 0;

            // Read the memory stream and convert it back into a string
            byte[] result = new byte[decryptedStream.Length];
            decryptedStream.Read(result, 0, (int)decryptedStream.Length);
            cryptStream.Close();
            UTF8Encoding myutf = new UTF8Encoding();
            return myutf.GetString(result);
        }

        public static string Hash(string plainText)
        {
            return Encrypt(plainText);
        }
    }

}
