using Microsoft.Win32;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Diagnostics;
using System.IO.Compression;
using System.Text;
using HttpClientProgress;

namespace SeriousLauncher {
    public partial class SeriousLauncherWindow : Form {
        private readonly string applicationFileName = @"Serious Trouble.exe";
        private readonly string applicationDirectoryName = @"Serious Trouble";
        private string pathToApplication = string.Empty;
        private RegistryData? registryData;
        private LauncherData? launcherData;

        private void SeriousLauncherWindow_Shown(object sender, EventArgs e) {
            this.registryData = CheckRegistryData();
            if (CheckFileSystem()) {
                StatusStripLabel.Text = string.Format("Application installed by path '{0}'", this.pathToApplication);
                _ = CheckUpdateAsync();
                return;
            }
            StatusStripLabel.Text = string.Format("Application is not installed");
            if (this.pathToApplication == string.Empty) {
                this.pathToApplication = @$"C:\Program Files";
            }
            InstallButton.Enabled = true;
        }

        private RegistryData? CheckRegistryData() {
            RegistryKey? subKey = Registry.CurrentUser.OpenSubKey(RegistryData.PathToSubKey);
            if (subKey == null) {
                return null;
            }
            RegistryData instance = new() {
                location = GetValueFromRegistryKey(subKey, RegistryData.TemplateAppLocationKey).TrimEnd(Path.DirectorySeparatorChar),
                version = GetValueFromRegistryKey(subKey, RegistryData.TemplateAppVersionKey)
            };
            subKey.Close();
            return instance;
        }

        private bool CheckFileSystem() {
            List<string> paths = new() {
                @$"C:\Program Files\{this.applicationDirectoryName}",
                @$"D:\Games\{this.applicationDirectoryName}"
            };

            foreach (var path in paths) {
                if (Directory.Exists(path)) {
                    this.pathToApplication = path;
                    if (File.Exists(Path.Combine(this.pathToApplication, this.applicationFileName))) {
                        return true;
                    }
                }
            }

            if (null == this.registryData) {
                return false;
            }
            if (this.registryData.location == string.Empty) {
                // TODO: trow exception (Old registry entry has been found)
                return false;
            }
            if (this.registryData.location.Equals(this.pathToApplication)) {
                return false;
            }
            if (Directory.Exists(this.registryData.location) &&
                File.Exists(Path.Combine(this.registryData.location, this.applicationFileName))) {
                this.pathToApplication = this.registryData.location;
                return true;
            }
            return false;
        }

        private async Task CheckUpdateAsync() {
            string lastVersion = "";
            HttpClient httpClient = new();
            try {
                HttpResponseMessage responce = await httpClient.GetAsync(LauncherData.URL);
                string jsonResponce = await responce.Content.ReadAsStringAsync();
                this.launcherData = JsonConvert.DeserializeObject<LauncherData>(jsonResponce) ?? throw new NullReferenceException();
                lastVersion = launcherData.versions[0].version;
            } catch (Exception e) {
                ErrorLabel.Text = string.Format("Get launcher data exception: '{0}'", e.Message);
                RunButton.Enabled = true;
                lastVersion = string.Empty;
            } finally {
                httpClient.Dispose();
            }
            if (lastVersion.Equals(string.Empty)) {
                return;
            }
            if (lastVersion.Equals(this.registryData?.version)) {
                StatusStripLabel.Text = string.Format("Actual version: {0}", lastVersion);
                RunButton.Enabled = true;
                return;
            }
            StatusStripLabel.Text = string.Format("Required update: {0}", lastVersion);
            InstallButton.Enabled = true;
        }

        public SeriousLauncherWindow() {
            InitializeComponent();
        }

        private async void InstallButton_Click(object sender, EventArgs e) {
            FolderBrowserDialog.InitialDirectory = this.pathToApplication;
            FolderBrowserDialog.ShowDialog();
            if (FolderBrowserDialog.SelectedPath == string.Empty) {
                return;
            }
            InstallButton.Enabled = false;
            ErrorLabel.Text = string.Empty;
            CheckFinalInstallationPath(FolderBrowserDialog.SelectedPath);
            FolderBrowserDialog.SelectedPath = string.Empty;
            //string fileName = "Serious Trouble.zip";
            string fileName = "Debugging archive.zip";
            string localApplicationData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string fullPathToArchive = Path.GetFullPath(fileName, localApplicationData);
            await DownloadArchiveAsync(fullPathToArchive);
            if (File.Exists(fullPathToArchive) == false) {
                InstallButton.Enabled = true;
                return;
            }

            using ZipArchive archive = ZipFile.OpenRead(fullPathToArchive){
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
                    string fullPath = Path.GetFullPath(entry.FullName, this.pathToApplication);
                    File.Delete(fullPath);
                    entry.ExtractToFile(fullPath);
                    SetupProgressBar.PerformStep();
                }
                RunButton.Enabled = true;
                StatusStripLabel.Text =
                    string.Format("Application installed by path '{0}' (total entries: {1})",
                    this.pathToApplication, archive.Entries.Count.ToString());
            }

            //archive.Dispose();
            File.Delete(fullPathToArchive);
        }

        private async Task DownloadArchiveAsync(string path) {
            //string url = "https://storage.yandexcloud.net/serious-trouble-resources/Debugging%20archive.zip";

            HttpClient client = new() {
                Timeout = TimeSpan.FromSeconds(120)
            };
            try {
                if (this.launcherData == null || this.launcherData.versions.Count < 1) {
                    throw new ApplicationException("URL is not defined");
                }
                string url = this.launcherData.versions[0].buildPath;
                byte[] bytes = await client.GetByteArrayAsync(new Uri(url));
                await File.WriteAllBytesAsync(path, bytes);
            } catch (Exception e) {
                ErrorLabel.Text = string.Format("Download archive exception: '{0}'", e.Message);
            } finally {
                client.Dispose();
            }
        }

        //private async Task DownloadArchiveAsync(string path) {
        //    string url = "https://storage.yandexcloud.net/serious-trouble-resources/Debugging%20archive.zip";

        //    WebClient client = new();
        //    client.DownloadDataCompleted += DownloadFileCompletedEventHandler;
        //    try {
        //        //if(this.launcherData == null || this.launcherData.versions.Count < 1) {
        //        //    throw new ApplicationException("URL is not defined");
        //        //}
        //        //string url = this.launcherData.versions[0].buildPath;
        //        client.DownloadFileAsync(new Uri(url), path);
        //    } catch (Exception e) {
        //        ErrorLabel.Text = string.Format("Download archive exception: '{0}'", e.Message);
        //    } finally {
        //        client.Dispose();
        //    }
        //}

        private void DownloadFileCompletedEventHandler(object? sender, AsyncCompletedEventArgs e) {
            throw new NotImplementedException();
        }

        private void RunButton_Click(object sender, EventArgs e) {
            Process process = new();
            process.StartInfo.FileName = Path.Combine(this.pathToApplication, this.applicationFileName);
            //process.StartInfo.Arguments = "-n";
            process.Start();
            process.WaitForExit();
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
    }
}