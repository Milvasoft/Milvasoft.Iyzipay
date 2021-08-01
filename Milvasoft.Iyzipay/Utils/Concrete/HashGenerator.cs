﻿using System;
using System.Security.Cryptography;
using System.Text;

namespace Milvasoft.Iyzipay.Utils.Concrete
{
    public sealed class HashGenerator
    {
        private HashGenerator()
        {
        }

        public static string GenerateHash(string apiKey, string secretKey, string randomString, BaseRequest request)
        {
#if NETSTANDARD
            SHA1 algorithm = SHA1.Create();
#else
            HashAlgorithm algorithm = new SHA1Managed();
#endif
            string hashStr = apiKey + randomString + secretKey + request.ToPKIRequestString();
            byte[] computeHash = algorithm.ComputeHash(Encoding.UTF8.GetBytes(hashStr));
            return Convert.ToBase64String(computeHash);
        }
    }
}
