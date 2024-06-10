using IWshRuntimeLibrary;

namespace SeriousLauncher {
    internal class Shortcut {
        public static void CreateDesktopShortcut(string target) {
            string desktopFolder = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            WshShell shell = new();
            string settingsLink = Path.Combine(desktopFolder, "Serious Trouble.lnk");
            IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(settingsLink);
            shortcut.TargetPath = target;
            //shortcut.IconLocation = @"C:\Program Files\Serious Trouble\icon.ico";
            //shortcut.Arguments = "-arg";
            shortcut.Description = "Run Serious Trouble";
            shortcut.Save();
        }
    }
}
