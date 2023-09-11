using System;
namespace Common
{
    public static class CriptoExtension
    {
        static string passPhrase1 = "!/(48$·#@";        // can be any string
        static string saltValue1 = "2t?¿@/&$!";        // can be any string
        static string hashAlgorithm1 = "SHA1";             // can be "MD5"
        static int passwordIterations1 = 3;                  // can be any number
        static string initVector1 = "9·#!/|@)}_-{º53d"; // must be 16 bytes

        static int keySize = 256;                // can be 192 or 128


        //public static string Encrypt(this string value)
        //{
        //    return Security.Cripto.Encrypt(value, passPhrase1, saltValue1, hashAlgorithm1, passwordIterations1, initVector1, keySize);
        //}
        //public static string Decrypt(this string value)
        //{
        //    return Security.Cripto.Decrypt(value, passPhrase1, saltValue1, hashAlgorithm1, passwordIterations1, initVector1, keySize);
        //}
    }
}
