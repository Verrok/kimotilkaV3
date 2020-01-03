using System;

namespace KimotilkaV3.Models
{
    public class Url
    {
        public int FollowCount { get; set; }
        public bool IsActive { get; set; }
        public string Hash { get; set; }
        public string Link { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}