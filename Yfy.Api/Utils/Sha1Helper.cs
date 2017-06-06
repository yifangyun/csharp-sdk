namespace Yfy.Api
{
    using System;
    using System.IO;
    using System.Security.Cryptography;

    internal class Sha1Helper
    {
        public static string ComputeSha1(string filePath)
        {
            var fs = File.OpenRead(filePath);
            var sha1 = SHA1.Create().ComputeHash(fs);
            fs.Close();
            return BitConverter.ToString(sha1).Replace("-", "").ToLower();
        }
    }
}
