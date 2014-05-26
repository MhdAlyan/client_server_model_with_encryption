using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace Files_transfreer_client
{
    static class AES_Encryption
    {
        public static byte[] EncryptString(SymmetricAlgorithm symAlg, string inString)
        {

            byte[] inBlock = UnicodeEncoding.Unicode.GetBytes(inString);

            ICryptoTransform xfrm = symAlg.CreateEncryptor();

            byte[] outBlock = xfrm.TransformFinalBlock(inBlock, 0, inBlock.Length);

            return outBlock;
        }

        public static byte[] EncryptBytes(SymmetricAlgorithm symAlg, byte[] inBlock)
        {

            ICryptoTransform xfrm = symAlg.CreateEncryptor();

            byte[] outBlock = xfrm.TransformFinalBlock(inBlock, 0, inBlock.Length);

            return outBlock;
        }


        public static string DecryptBytes(SymmetricAlgorithm symAlg, byte[] inBytes)
        {
            ICryptoTransform xfrm = symAlg.CreateDecryptor();


            byte[] outBlock = xfrm.TransformFinalBlock(inBytes, 0, inBytes.Length);

            return UnicodeEncoding.Unicode.GetString(outBlock);
        }

        public static byte [] DecryptFileBytes(SymmetricAlgorithm symAlg, byte[] inBytes)
        {
            ICryptoTransform xfrm = symAlg.CreateDecryptor();


            byte[] outBlock = xfrm.TransformFinalBlock(inBytes, 0, inBytes.Length);

            return outBlock;
        }


        public static void ChangeMode(string s ,AesCryptoServiceProvider aesc)
        {
            if (s == "CBC")
            {
                aesc.Mode = CipherMode.CBC;
            }
            else if (s == "CFB")
            {
                aesc.Mode = CipherMode.CFB;
            }
            else if (s == "CTS")
            {
                aesc.Mode = CipherMode.CTS;
            }
            else if (s == "ECB")
            {
                aesc.Mode = CipherMode.ECB;
                
            }
            else if (s == "OFB")
            {
                aesc.Mode = CipherMode.OFB;
                
            }
            else
            {
                aesc.Mode = CipherMode.CBC;
                
            }
        }



    }
}
