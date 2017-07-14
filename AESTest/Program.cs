using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using AESTest.Class;

namespace AESTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string origin = "Test.This is a test";
            AESEncoder encoder = new AESEncoder();

            using (RijndaelManaged rijAlg = new RijndaelManaged())
            {
                rijAlg.GenerateKey();
                rijAlg.GenerateIV();
                string encrypted = encoder.EncryptToBase64(origin, rijAlg.Key, rijAlg.IV);
                string decrypted = encoder.DecryptStringFromBase64String(encrypted, rijAlg.Key, rijAlg.IV);
                Console.WriteLine("Original : {0}", origin);
                Console.WriteLine("Encrypted : {0}", encrypted);
                Console.WriteLine("Decrypted : {0}", decrypted);
            }
        }
    }
}
