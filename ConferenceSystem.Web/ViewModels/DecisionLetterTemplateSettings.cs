namespace ConferenceSystem.Web.ViewModels
{
    public class DecisionLetterTemplateSettings
    {
        public string? CurrentTemplateImagePath { get; set; }
        public string? CurrentSignatureImagePath { get; set; }
        public string? ChairmanName { get; set; }
        public string? ChairmanTitle { get; set; }
        public int PageWidthPx { get; set; } = 794;
        public int PageHeightPx { get; set; } = 1123;
        public List<DecisionLetterFieldSetting> Fields { get; set; } = new();
    }

    public class DecisionLetterFieldSetting
    {
        public string Key { get; set; } = string.Empty;
        public string Label { get; set; } = string.Empty;
        public float X { get; set; }
        public float Y { get; set; }
        public float Width { get; set; } = 160;
        public float Height { get; set; } = 28;
        public int FontSize { get; set; } = 11;
        public bool Bold { get; set; }
        public bool Visible { get; set; } = true;
    }
}
