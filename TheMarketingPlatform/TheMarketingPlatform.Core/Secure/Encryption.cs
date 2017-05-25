using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;

namespace TheMarketingPlatform.Core.Secure
{
    /// <summary>
    /// Help class for encryption and decryption
    /// </summary>
    public sealed class Encryption
    {
        /// <summary>
        /// Encrypts data with aes
        /// </summary>
        /// <param name="data">the clear data</param>
        /// <param name="key">the key for aes</param>
        /// <param name="vector">the IV vector</param>
        /// <returns>returns encrypted data</returns>
        public static byte[] Encrypt(byte[] data, byte[] key, byte[] vector)
        {
            byte[] encrypt;

            using (var aes = new AesManaged())
            {
                aes.Key = key;
                aes.IV = vector;

                using (ICryptoTransform encryptor = aes.CreateEncryptor(key, vector))
                {
                    using (var stream = new MemoryStream())
                    {
                        using (var crypto = new CryptoStream(stream, encryptor, CryptoStreamMode.Write))
                        {
                            using (var writer = new BinaryWriter(crypto))
                            {
                                writer.Write(data);
                            }

                            encrypt = stream.ToArray();
                        }
                    }
                }
            }

            return encrypt;
        }

        /// <summary>
        /// Decrypts data with aes
        /// </summary>
        /// <param name="data">the encrypted data</param>
        /// <param name="key">a key for aes</param>
        /// <param name="vector">a IV vector for aes</param>
        /// <returns>the decrypted data</returns>
        public static byte[] Decrypt(byte[] data, byte[] key, byte[] vector)
        {
            byte[] decrypt = new byte[data.Length];

            using (var aes = new AesManaged())
            {
                aes.Key = key;
                aes.IV = vector;


                using (ICryptoTransform decryptor = aes.CreateDecryptor(key, vector))
                {
                    using (var stream = new MemoryStream(data))
                    {
                        using (var crypto = new CryptoStream(stream, decryptor, CryptoStreamMode.Read))
                        {
                            using (var memo = new MemoryStream())
                            {
                                crypto.CopyTo(memo);
                                decrypt = memo.ToArray();
                            }
                        }
                    }
                }
            }

            return decrypt;
        }

        /// <summary>
        /// Hash data with the sha 256 algorithm
        /// </summary>
        /// <param name="data">Data to hash</param>
        /// <returns>returns a 256 bit hash</returns>
        public static byte[] Hash256(byte[] data)
        {
            using (var sha = new SHA256Managed())
            {
                return sha.ComputeHash(data);
            }
        }

        /// <summary>
        /// Hash data with the sha 512 algorithm
        /// </summary>
        /// <param name="data">Data to hash</param>
        /// <returns>returns a 512 bit hash</returns>
        public static byte[] Hash512(byte[] data)
        {
            using (var sha = new SHA512Managed())
            {
                return sha.ComputeHash(data);
            }
        }

        /// <summary>
        /// Returns a random vector and key for aes
        /// </summary>
        /// <returns>Returns a tuble of vector and key</returns>
        public static (byte[] key, byte[] vector) GetKeyAndVector()
        {
            using (var aes = new AesManaged())
            {
                aes.GenerateIV();
                aes.GenerateKey();

                return (aes.Key, aes.IV);
            }
        }

        /// <summary>
        /// Generates a random key for aes
        /// </summary>
        /// <returns>a random key</returns>
        public static byte[] GetKey()
        {
            using (var aes = new AesManaged())
            {
                aes.GenerateKey();
                return aes.Key;
            }
        }

        /// <summary>
        /// Generates a random vector for aes
        /// </summary>
        /// <returns>a random IV vector</returns>
        public static byte[] GetVector()
        {
            using (var aes = new AesManaged())
            {
                aes.GenerateIV();
                return aes.IV;
            }
        }

        /// <summary>
        /// Get random numbers with specific size
        /// </summary>
        /// <param name="size">the size of the array</param>
        /// <returns>returns a array with random numbers</returns>
        public static byte[] GetRandom(int size)
        {
            var array = new byte[size];
            var rand = RandomNumberGenerator.Create();
            rand.GetBytes(array);
            return array;
        }
    }
}
