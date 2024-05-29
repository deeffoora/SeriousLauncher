using Microsoft.Win32;
using System.IO.Compression;
using System.Text;

namespace SeriousLauncher {
    public partial class SeriousLauncherWindow : Form {
        private readonly string pathToSeriousTroubleSubKey = @"SOFTWARE\deeffoora-games\Serious Trouble\";
        private readonly string templateAppLocationKey = @"Application location";

        public SeriousLauncherWindow() {
            InitializeComponent();
        }

        private void SetupButton_Click(object sender, EventArgs e) {
            SetupButton.Enabled = false;
            string fileName = "SeriousTrouble.zip";
            string fullPathToArchive = Path.GetFullPath(fileName, @"C:\Users\deeffoora\Downloads");


            RegistryKey? subKey = Registry.CurrentUser.OpenSubKey(this.pathToSeriousTroubleSubKey);
            if (subKey == null) {
                StatusStripLabel.Text = "Serious Trouble registry key is missing";
                return;
            }
            string name = GetKeyByTemplateName(subKey, this.templateAppLocationKey);
            if (name == string.Empty) {
                StatusStripLabel.Text = string.Format("No key match by template '{0}'", this.templateAppLocationKey);
                return;
            }
            byte[]? sequence = (byte[]?)subKey.GetValue(name);
            if (sequence == null) {
                StatusStripLabel.Text = string.Format("Failed to extract data from '{0}' key", name);
                return;
            }
            // The last byte 00 in the sequence causes an exception
            string trim = Encoding.UTF8.GetString(sequence, 0, sequence.Length - 1);
            string fullPathToApplication = StatusStripLabel.Text = Path.GetFullPath(trim);
            subKey.Close();
            string root = Path.GetFullPath(Path.Combine(fullPathToApplication, @"..\"));
            if (Directory.Exists(root) == false) {
                StatusStripLabel.Text = string.Format("Root application directory is missing '{0}'", root);
                return;
            }

            using ZipArchive archive = ZipFile.OpenRead(fullPathToArchive);
            SetupProgressBar.Maximum = archive.Entries.Count;
            foreach (ZipArchiveEntry entry in archive.Entries) {
                string? entryPath = Path.GetDirectoryName(entry.FullName);
                if (string.IsNullOrEmpty(entryPath)) {
                    entryPath = @".\";
                }
                entryPath = Path.GetFullPath(entryPath, fullPathToApplication);
                if (Directory.Exists(entryPath) == false) {
                    Directory.CreateDirectory(entryPath);
                }
                entry.ExtractToFile(Path.GetFullPath(entry.FullName, fullPathToApplication));
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