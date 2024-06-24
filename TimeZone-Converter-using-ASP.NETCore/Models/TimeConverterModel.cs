namespace TimeZone_Converter_using_ASP.NETCore.Models
{
    public class TimeConverterModel
    {
        public string? SourceTimeZone { get; set; }
        public string? TargetTimeZone { get; set; }
        public DateTime SourceDateTime { get; set; }
        public DateTime TargetDateTime { get; set; }
    }
}
