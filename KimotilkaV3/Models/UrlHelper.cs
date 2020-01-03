using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace KimotilkaV3.Models
{
    public static class UrlHelper
    {

        private static readonly string Base62Set;
        private static readonly List<char> CharSet;
        private static readonly int HashLength;
        private static readonly Regex Regex;

        static UrlHelper()
        {
            Base62Set = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            HashLength = AppSettings.HashLength;
            CharSet = Base62Set.ToList();
            Regex = new Regex(@"^(http|https)://");
        }
        
        private static string GetHashedUrl(string url)
        {
            string hashed = "";
            List<byte> bytes = Encoding.ASCII.GetBytes(url).ToList();
            bytes.Shuffle();
            for (int i = 0; i < HashLength; i++)
            {
                hashed += CharSet[Convert.ToByte(bytes[i]) % 62]; 
            }
            return hashed;
        }


        private static bool CheckUrl(string url)
        {
            Uri uriResult;
            bool result = Uri.TryCreate(url, UriKind.Absolute, out uriResult) 
                          && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
            return result;
        }
    }
}