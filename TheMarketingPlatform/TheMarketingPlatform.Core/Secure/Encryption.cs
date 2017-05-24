using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;

namespace TheMarketingPlatform.Core.Secure
{
    public sealed class Encryption
    {
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

        public static byte[] Hash256(byte[] data)
        {
            using (var sha = new SHA256Managed())
            {
                return sha.ComputeHash(data);
            }
        }

        public static byte[] Hash512(byte[] data)
        {
            using (var sha = new SHA512Managed())
            {
                return sha.ComputeHash(data);
            }
        }

        public static (byte[] key, byte[] vector) GetKeyAndVector()
        {
            using (var aes = new AesManaged())
            {
                aes.GenerateIV();
                aes.GenerateKey();

                return (aes.Key, aes.IV);
            }
        }

        public static byte[] GetKey()
        {
            using (var aes = new AesManaged())
            {
                aes.GenerateKey();
                return aes.Key;
            }
        }

        public static byte[] GetVector()
        {
            using (var aes = new AesManaged())
            {
                aes.GenerateIV();
                return aes.IV;
            }
        }

        public static byte[] GetRandom(int size)
        {
            var array = new byte[size];
            var rand = RandomNumberGenerator.Create();
            rand.GetBytes(array);
            return array;
        }
    }
}
