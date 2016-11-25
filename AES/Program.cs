using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AES
{
    class Program
    {
        public static void EncryptSomeText()
        {
            string original = "My secret data!";
            using (SymmetricAlgorithm symmetricAlgorithm = new AesManaged())

            {
                byte[] encrypted = Encrypt(symmetricAlgorithm, original);
                string roundtrip = Decrypt(symmetricAlgorithm, encrypted);
                // Displays: My secret data!
                Console.WriteLine("Original: {0}", original);
                foreach (var b in encrypted)
                    Console.WriteLine(b.ToString());

                Console.WriteLine("Round Trip: {0}", roundtrip);
            }
        }
        static byte[] Encrypt(SymmetricAlgorithm aesAlg, string plainText)
        {
            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
            using (MemoryStream msEncrypt = new MemoryStream())
            {
                using (CryptoStream csEncrypt =
                    new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                    {
                        swEncrypt.Write(plainText);
                    }

                    return msEncrypt.ToArray();
                }
            }
        }

        static string Decrypt(SymmetricAlgorithm aesAlg, byte[] cipherText)
        {
            ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
            using (MemoryStream msDecrypt = new MemoryStream(cipherText))
            {
                using (CryptoStream csDecrypt =
                new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                    {
                        return srDecrypt.ReadToEnd();
                    }
                }
            }
        }

        static void ConsoleWriteAbsatz(String Ausgabe = null, Char zeichen = '-')
        {
            Console.WriteLine((" " + Ausgabe.Trim() + " ").PadLeft(30, zeichen).PadRight(79, zeichen));
        }

        static void Main(string[] args)
        {
            byte[] bb = new byte[32];
            byte[] cc = new byte[32];
            for (int i = 0; i < bb.Length; i++)
            {
                bb[i] = 10;
                cc[i] = 180;
            }
            var aesM = new AesManaged();
            aesM.Key = bb;
            //aaa.GenerateKey();


            Byte[] encryptedStringbb = Encrypt(aesM, "HALLO Torsten Pohling");

            aesM.Key = cc;
            Byte[] encryptedStringcc = Encrypt(aesM, "Torsten Pohling Holla");

            ConsoleWriteAbsatz(" erste encrypted ");
            foreach (var item in encryptedStringbb)
            {
                Console.Write(item.ToString() + " ");
            }
            Console.WriteLine();

            ConsoleWriteAbsatz("  zweite encrypted");
            foreach (var item in encryptedStringcc)
            {
                Console.Write(item.ToString() + " ");
            }
            Console.WriteLine();

            ConsoleWriteAbsatz("Rückverwandlung ");
            aesM.Key = bb;
            String decryptedStringbb = Decrypt(aesM, encryptedStringbb);

            aesM.Key = cc;
            String decryptedStringcc = Decrypt(aesM, encryptedStringcc);

            Console.WriteLine(decryptedStringbb);
            Console.WriteLine(decryptedStringcc);

            Console.ReadLine();

        }
    }
}
