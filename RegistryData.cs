namespace SeriousLauncher {
    internal class RegistryData {
        // Branches and application key names in registry (may change)
        public static readonly string PathToSubKey = @"SOFTWARE\deeffoora-games\Serious Trouble\";
        public static readonly string TemplateAppLocationKey = @"Application location";
        public static readonly string TemplateAppVersionKey = @"Version";
        // Class instance fields
        public string location = string.Empty;
        public string version = string.Empty;
    }
}
