using Microsoft.Win32;
using System.IO.Compression;
using System.Text;

namespace SeriousLauncher {
    public partial class SeriousLauncherWindow : Form {
        private readonly string pathToSeriousTroubleSubKey = @"SOFTWARE\deeffoora-games\Serious Trouble\";
        private readonly string templateAppLocationKey = @"Application location";
        private readonly string applicationFileName = @"Serious Trouble.exe";
        private readonly string applicationDirectoryName = @"Serious Trouble";
        private string pathToApplication = string.Empty;

        private void SeriousLauncherWindow_Shown(object sender, EventArgs e) {
            //bool isInstalled = CheckRegistry();
            bool isInstalled = false;
            //if (isInstalled) {
            //    StatusStripLabel.Text = string.Format("Application installed by path '{0}'", this.pathToApplication);
            //    return;
            //}
            if (this.pathToApplication == string.Empty) {
                isInstalled = CheckFileSystem();
            }
            if (isInstalled) {
                StatusStripLabel.Text = string.Format("Application installed by path '{0}'", this.pathToApplication);
                return;
            }
            if (this.pathToApplication == string.Empty) {
                this.pathToApplication = @$"C:\ProgrammFiles\{this.applicationDirectoryName}";
            }
            SetupButton.Enabled = true;
        }

        private bool CheckFileSystem() {
            List<string> paths = new() {
                @$"C:\Program Files\{this.applicationDirectoryName}",
                @$"D:\Games\{this.applicationDirectoryName}"
            };

            foreach (var path in paths) {
                if (Directory.Exists(path)) {
                    this.pathToApplication = path;
                    string filePath = Path.GetFullPath(Path.Combine(path, this.applicationFileName));
                    if (File.Exists(filePath)) {
                        return true;
                    }
                }
            }
            StatusStripLabel.Text = string.Format("No installation found in file system");
            return false;
        }

        private bool CheckRegistry() {
            RegistryKey? subKey = Registry.CurrentUser.OpenSubKey(this.pathToSeriousTroubleSubKey,
                System.Security.AccessControl.RegistryRights.ReadKey);
            if (subKey == null) {
                StatusStripLabel.Text = "Application registry key is missing";
                return false;
            }
            string name = GetKeyByTemplateName(subKey, this.templateAppLocationKey);
            if (name == string.Empty) {
                StatusStripLabel.Text = string.Format("No key match by template '{0}'", this.templateAppLocationKey);
                return false;
            }
            byte[]? sequence = (byte[]?)subKey.GetValue(name);
            subKey.Close();
            if (sequence == null) {
                StatusStripLabel.Text = string.Format("Failed to extract data from '{0}' key", name);
                return false;
            }
            // The last byte [00] in the sequence causes an exception
            string trimSequence = Encoding.UTF8.GetString(sequence, 0, sequence.Length - 1);
            string supposedPath = Path.GetFullPath(trimSequence);
            string root = Path.GetFullPath(Path.Combine(supposedPath, @"..\"));
            if (Directory.Exists(root) == false) {
                StatusStripLabel.Text = string.Format("The path does not exist '{0}'", supposedPath);
                return false;
            }
            // The suggested installation path that is found in the registry
            this.pathToApplication = supposedPath;
            if (Directory.Exists(supposedPath) == false) {
                StatusStripLabel.Text = string.Format("The path does not exist '{0}'", supposedPath);
                return false;
            }
            string filePath = Path.GetFullPath(Path.Combine(supposedPath, this.applicationFileName));
            if (File.Exists(filePath) == false) {
                StatusStripLabel.Text = string.Format("Installation files are missing by '{0}'", supposedPath);
                return false;
            }
            return true;
        }

        public SeriousLauncherWindow() {
            InitializeComponent();
        }

        private void SetupButton_Click(object sender, EventArgs e) {
            SetupButton.Enabled = false;
            string fileName = "SeriousTrouble.zip";
            string fullPathToArchive = Path.GetFullPath(fileName, @"C:\Users\deeffoora\Downloads");



            using ZipArchive archive = ZipFile.OpenRead(fullPathToArchive);
            SetupProgressBar.Maximum = archive.Entries.Count;
            foreach (ZipArchiveEntry entry in archive.Entries) {
                string? entryPath = Path.GetDirectoryName(entry.FullName);
                if (string.IsNullOrEmpty(entryPath)) {
                    entryPath = @".\";
                }
                entryPath = Path.GetFullPath(entryPath, this.pathToApplication);
                if (Directory.Exists(entryPath) == false) {
                    Directory.CreateDirectory(entryPath);
                }
                entry.ExtractToFile(Path.GetFullPath(entry.FullName, this.pathToApplication));
                SetupProgressBar.PerformStep();
            }

            StatusStripLabel.Text = archive.Entries.Count.ToString();
        }

        private string GetKeyByTemplateName(RegistryKey subKey, string templ) {
            string res = string.Empty;
            if (templ == string.Empty) {
                return res;
            }
            string[] temp = subKey.GetValueNames();
            foreach (string key in temp) {
                if (key.IndexOf(templ) == 0) {
                    res = key; break;
                }
            }
            return res;
        }
    }
}