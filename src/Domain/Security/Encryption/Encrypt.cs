    using System;
    using System.IO;
    using System.Security.Cryptography;
    using System.Text;

    namespace Domain.Security.Encryption
    {
        public static class Encrypt
        {
            private const string EncryptionKey = "testEncryptionKey";

            public static string EncryptPassword(string clearText)
            {
                if (string.IsNullOrEmpty(clearText)) throw new ArgumentException("Input text cannot be null or empty.");

                byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);

                using (Aes encryptor = Aes.Create())
                {
                    var pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[]
                    { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });

                    encryptor.Key = pdb.GetBytes(32);
                    encryptor.IV = pdb.GetBytes(16);

                    using (var ms = new MemoryStream())
                    {
                        using (var cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(clearBytes, 0, clearBytes.Length);
                            cs.Close();
                        }

                        return Convert.ToBase64String(ms.ToArray());
                    }
                }
            }

            public static string DecryptPassword(string cipherText)
            {
                if (string.IsNullOrEmpty(cipherText)) throw new ArgumentException("Cipher text cannot be null or empty.");

                byte[] cipherBytes = Convert.FromBase64String(cipherText);

                using (Aes encryptor = Aes.Create())
                {
                    var pdb = new Rfc2898DeriveBytes(EncryptionKey,new byte []
                    { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });

                    encryptor.Key = pdb.GetBytes(32);
                    encryptor.IV = pdb.GetBytes(16);

                    using (var ms = new MemoryStream())
                    {
                        using (var cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(cipherBytes, 0, cipherBytes.Length);
                            cs.Close();
                        }

                        return Encoding.Unicode.GetString(ms.ToArray());
                    }
                }
            }

            public static string EncryptPasswordBase64(string text)
            {
                if (string.IsNullOrEmpty(text)) throw new ArgumentException("Input text cannot be null or empty.");

                var plainTextBytes = Encoding.UTF8.GetBytes(text);
                return Convert.ToBase64String(plainTextBytes);
            }

            public static string DecryptPasswordBase64(string base64EncodedData)
            {
                if (string.IsNullOrEmpty(base64EncodedData)) throw new ArgumentException("Base64 data cannot be null or empty.");

                var base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
                return Encoding.UTF8.GetString(base64EncodedBytes);
            }
        }
    }
