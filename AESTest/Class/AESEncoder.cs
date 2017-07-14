using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;

namespace AESTest.Class
{
    public class AESEncoder
    {
        static RijndaelManaged rijAlg = new RijndaelManaged();

        public string EncryptToBase64(string plainText,byte[] Key,byte[] IV)
        {
            if(IsInputValid(plainText,Key,IV))
            {
                byte[] encrypted;
                rijAlg.Key = Key;
                rijAlg.IV = IV;

                ICryptoTransform encryptor = rijAlg.CreateEncryptor(rijAlg.Key, rijAlg.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
                return Convert.ToBase64String(encrypted);
            }
            return string.Empty;
        }

        public string DecryptStringFromBase64String(string cipherText,byte[] Key,byte[] IV)
        {
            
            if(IsInputValid(cipherText,Key,IV))
            {
                var Base64byte = Convert.FromBase64String(cipherText);
                
                string plainText = string.Empty;

                rijAlg.Key = Key;
                rijAlg.IV = IV;

                ICryptoTransform decryptor = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV);

                using (MemoryStream msDecrypt = new MemoryStream(Base64byte))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            return srDecrypt.ReadToEnd();
                        }
                    }
                }

            }
            return string.Empty;
        }

        protected bool IsInputValid(string plainText,byte[] Key,byte[] IV)
        {
            if (string.IsNullOrEmpty(plainText) ||
                Key == null || Key.Length <= 0 ||
                IV == null || IV.Length <= 0)
                return false;
            return true;
        }
    }
}
