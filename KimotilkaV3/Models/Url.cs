using System;

namespace KimotilkaV2.Models
{
    public class Url
    {
        public string Hash { get; set; }
        public string Link { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}