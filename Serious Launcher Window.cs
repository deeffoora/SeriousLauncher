using Microsoft.Win32;
using Newtonsoft.Json;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Xml.Linq;

namespace SeriousLauncher {
    public partial class SeriousLauncherWindow : Form {
        private readonly string applicationFileName = @"Serious Trouble.exe";
        private readonly string applicationDirectoryName = @"Serious Trouble";
        private string pathToApplication = string.Empty;
        private RegistryData? registryData;

        private void SeriousLauncherWindow_Shown(object sender, EventArgs e) {
            this.registryData = CheckRegistryData();
            if (CheckFileSystem()) {
                StatusStripLabel.Text = string.Format("Application installed by path '{0}'", this.pathToApplication);
                RunButton.Enabled = true;
                _ = CheckUpdateAsync();
                return;
            }
            StatusStripLabel.Text = string.Format("Application is not installed");
            if (this.pathToApplication == string.Empty) {
                this.pathToApplication = @$"C:\Program Files\";
            }
            InstallButton.Enabled = true;
        }

        private RegistryData? CheckRegistryData() {
            RegistryKey? subKey = Registry.CurrentUser.OpenSubKey(RegistryData.PathToSubKey);
            if (subKey == null) {
                return null;
            }
            RegistryData instance = new() {
                location = GetValueFromRegistryKey(subKey, RegistryData.TemplateAppLocationKey),
                version = GetValueFromRegistryKey(subKey, RegistryData.TemplateAppVersionKey)
            };
            subKey.Close();
            return instance;
        }

        private bool CheckFileSystem() {
            if (null == this.registryData) {
                List<string> paths = new() {
                    @$"C:\Program Files\{this.applicationDirectoryName}",
                    @$"D:\Games\{this.applicationDirectoryName}"
                };

                foreach (var path in paths) {
                    if (Directory.Exists(path)) {
                        this.pathToApplication = path;
                        string fullPath = Path.Combine(this.pathToApplication, this.applicationFileName);
                        if (File.Exists(fullPath)) {
                            return true;
                        }
                    }
                }
                return false;

            } else {
                if (this.registryData.location == string.Empty) {
                    // TODO: trow exception (Old registry entry has been found)
                    return false;
                } else {
                    if (Directory.Exists(this.registryData.location)) {
                        this.pathToApplication = this.registryData.location;
                    }
                    string fullPath = Path.Combine(this.pathToApplication, this.applicationFileName);
                    if (File.Exists(fullPath)) {
                        return true;
                    }
                    return false;
                }

            }
        }

        private async Task CheckUpdateAsync() {
            string lastVersion = "";
            HttpClient httpClient = new();
            try {
                HttpResponseMessage responce = await httpClient.GetAsync(LauncherData.URL);
                string jsonResponce = await responce.Content.ReadAsStringAsync();
                //jsonResponce = jsonResponce.Replace("{", "");
                LauncherData? launcherData =
                    JsonConvert.DeserializeObject<LauncherData>(jsonResponce) ?? throw new NullReferenceException();
                lastVersion = launcherData.versions[0].version;
            } catch (Exception e) {
                ErrorLabel.Text = string.Format("Get launcher data exception: '{0}'", e.Message);
            } finally {
                httpClient.Dispose();
            }
            if (lastVersion.Equals(this.registryData?.version)) {
                StatusStripLabel.Text = string.Format("Actual version '{0}'", lastVersion);
                return;
            }
            StatusStripLabel.Text = string.Format("Update is required to '{0}'", lastVersion);
        }

        public SeriousLauncherWindow() {
            InitializeComponent();
        }

        private void InstallButton_Click(object sender, EventArgs e) {
            InstallButton.Enabled = false;
            FolderBrowserDialog.InitialDirectory = this.pathToApplication;
            FolderBrowserDialog.ShowDialog();
            if (FolderBrowserDialog.SelectedPath == string.Empty) {
                InstallButton.Enabled = true;
                return;
            }
            CheckFinalInstallationPath(FolderBrowserDialog.SelectedPath);
            StatusStripLabel.Text = this.pathToApplication;
            // Temporary path to archive
            string fileName = "SeriousTrouble.zip";
            string userPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            string fullPathToArchive = Path.GetFullPath(fileName, $@"{userPath}\Downloads");

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
            RunButton.Enabled = true;
            StatusStripLabel.Text =
                string.Format("Application installed by path '{0}' (total entries: {1})",
                this.pathToApplication, archive.Entries.Count.ToString());
        }

        private void CheckFinalInstallationPath(string path) {
            string dirName = Path.GetFileName(path.TrimEnd(Path.DirectorySeparatorChar));
            if (dirName == this.applicationDirectoryName) {
                return;
            }
            this.pathToApplication = Path.Combine(path, this.applicationDirectoryName);
            if (Directory.Exists(this.pathToApplication) == false) {
                Directory.CreateDirectory(this.pathToApplication);
            }
        }

        private string GetValueFromRegistryKey(RegistryKey subKey, string templ) {
            string name = GetKeyByTemplateName(subKey, templ);
            if (name == string.Empty) {
                return string.Empty;
            }
            byte[]? sequence = (byte[]?)subKey.GetValue(name);
            if (sequence == null) {
                return string.Empty;
            }
            // The last byte [00] in the sequence causes an exception
            return Encoding.UTF8.GetString(sequence, 0, sequence.Length - 1);
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

        private void RunButton_Click(object sender, EventArgs e) {
            Process process = new();
            // Configure the process using the StartInfo properties.
            process.StartInfo.FileName = Path.Combine(this.pathToApplication, this.applicationFileName);
            //process.StartInfo.Arguments = "-n";
            //process.StartInfo.WindowStyle = ProcessWindowStyle.Maximized;
            process.Start();
            process.WaitForExit();
        }
    }
}