namespace SeriousLauncher {
    internal class LauncherData {
        // Remote location JSON data file
        public static readonly string URL = @"https://storage.yandexcloud.net/serious-trouble-resources/launcher.json";
        // Class instance fields
        public List<VersionData> versions;
        public List<string> hubs;
    }

    internal class VersionData {
        public string version;
        public string buildPath;
    }
}
