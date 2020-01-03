namespace KimotilkaV3.Models
{
    public class RequestUrl
    {
        public string Url { get; set; }
        public AdvancedSettings AdvancedSettings { get; set; }
    }

    public class AdvancedSettings
    {
        public string Name { get; set; }
        public bool OnlyHash { get; set; }
        public double? StartDate { get; set; }
        public double? ExpireDate { get; set; }
    }
}