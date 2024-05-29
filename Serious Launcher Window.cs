using Microsoft.Win32;
using System.IO.Compression;
using System.Text;

namespace SeriousLauncher {
    public partial class SeriousLauncherWindow : Form {
        private readonly string pathToSeriousTroubleSubKey = @"SOFTWARE\deeffoora-games\Serious Trouble\";
        private readonly string templateAppLocationKey = @"Application location";
        private readonly string applicationFileName = @"Serious Trouble.exe";
        private string pathToApplication = string.Empty;

        private void SeriousLauncherWindow_Shown(object sender, EventArgs e) {
            bool isInstalled = CheckLocalInstalledApplication();
            if (isInstalled) {
                StatusStripLabel.Text = string.Format("Application installed by path '{0}'", this.pathToApplication);
            } else {
                if (this.pathToApplication == string.Empty) {
                    this.pathToApplication = @"C:\Users\deeffoora\Downloads\Temp";
                }
                SetupButton.Enabled = true;
            }
        }

        private bool CheckLocalInstalledApplication() {
            RegistryKey? subKey = Registry.CurrentUser.OpenSubKey(this.pathToSeriousTroubleSubKey);
            if (subKey == null) {
                StatusStripLabel.Text = "Serious Trouble registry key is missing";
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
            if (Directory.Exists(supposedPath) == false) {
                StatusStripLabel.Text = string.Format("The path does not exist '{0}'", supposedPath);
                return false;
            }
            // The suggested installation path that is found in the registry
            this.pathToApplication = supposedPath;
            string filePath = Path.GetFullPath(Path.Combine(supposedPath, this.applicationFileName));
            if (File.Exists(filePath) == false) {
                StatusStripLabel.Text = string.Format("Application is not installed");
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


            //string root = Path.GetFullPath(Path.Combine(fullPathToApplication, @"..\"));
            //if (Directory.Exists(root) == false) {
            //    StatusStripLabel.Text = string.Format("Root application directory is missing '{0}'", root);
            //    return;
            //}

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