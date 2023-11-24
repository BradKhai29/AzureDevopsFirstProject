namespace SampleUnitTestProject_NetCore.Helpers.DateTimeFormat
{
    public sealed record BuiltInFormat(string Format)
    {
        public static readonly BuiltInFormat ddMMyyyy = new BuiltInFormat("dd/MM/yyyy");
        public static readonly BuiltInFormat ddMMyyyy_HHmmss = new BuiltInFormat("dd/MM/yyyy HH:mm:ss");
        public static readonly BuiltInFormat MMddyyyy = new BuiltInFormat("MM/dd/yyyy");
        public static readonly BuiltInFormat ddMMyy = new BuiltInFormat("dd/MM/yy");
        public static readonly BuiltInFormat Default = ddMMyyyy;
        public static readonly BuiltInFormat DefaultWithTime = ddMMyyyy_HHmmss;
        public static readonly BuiltInFormat ScreenshotFormat = new BuiltInFormat("yyyyMMdd_HHmmss");
        public static readonly BuiltInFormat ReportFormat = ScreenshotFormat;
    }
}