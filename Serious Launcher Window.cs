using Microsoft.Win32;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Diagnostics;
using System.IO.Compression;
using System.Text;
using HttpClientProgress;
using System;

namespace SeriousLauncher {
    public partial class SeriousLauncherWindow : Form {
        private readonly string applicationFileName = @"Serious Trouble.exe";
        private readonly string applicationDirectoryName = @"Serious Trouble";
        private string pathToApplication = string.Empty;
        private RegistryData? registryData;
        private LauncherData? launcherData;

        private async void SeriousLauncherWindow_Shown(object sender, EventArgs e) {
            await InitLauncher();
        }

        private async Task InitLauncher() {
            RunButton.Enabled = false;
            UpdateButton.Enabled = false;
            UpdateButton.Text = "UPDATE";
            StatusLabel.Text = string.Format("Get launcher data...");
            GetRegistryData();
            await GetLauncherData();
            StatusLabel.Text = string.Empty;
            if (CheckFileSystem()) {
                StatusLabel.Text = string.Format("Application installed by path '{0}'", this.pathToApplication);
                CheckUpdate();
                return;
            }
            StatusLabel.Text = string.Format("Application is not installed");
            if (this.pathToApplication == string.Empty) {
                this.pathToApplication = @$"C:\Program Files";
            }
            UpdateButton.Text = "INSTALL";
            UpdateButton.Enabled = true;
        }

        private void GetRegistryData() {
            RegistryKey? subKey = Registry.CurrentUser.OpenSubKey(RegistryData.PathToSubKey);
            if (subKey == null) {
                return;
            }
            this.registryData = new() {
                location = GetValueFromRegistryKey(subKey, RegistryData.TemplateAppLocationKey).TrimEnd(Path.DirectorySeparatorChar),
                version = GetValueFromRegistryKey(subKey, RegistryData.TemplateAppVersionKey)
            };
            subKey.Close();
        }

        private async Task GetLauncherData() {
            HttpClient client = new();
            try {
                HttpResponseMessage responce = await client.GetAsync(LauncherData.URL);
                string jsonResponce = await responce.Content.ReadAsStringAsync();
                this.launcherData =
                    JsonConvert.DeserializeObject<LauncherData>(jsonResponce) ?? throw new NullReferenceException();
            } catch (Exception e) {
                ErrorLabel.Text = string.Format("Get launcher data exception: '{0}'", e.Message);
            } finally {
                client.Dispose();
            }
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
                // TODO: Throw exception (old registry entry has been found)
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

        private void CheckUpdate() {
            if (this.launcherData == null) {
                StatusLabel.Text = string.Format("Impossible to check the version");
                RunButton.Enabled = true;
                return;
            }
            // TODO: To iterate through values
            string last = launcherData.versions[0].version;
            if (last.Equals(string.Empty)) {
                return;
            }
            if (last.Equals(this.registryData?.version)) {
                StatusLabel.Text = string.Format("Actual version: {0}", last);
                RunButton.Enabled = true;
                return;
            }
            StatusLabel.Text = string.Format("Required update: {0}", last);
            UpdateButton.Enabled = true;
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
            UpdateButton.Enabled = false;
            ErrorLabel.Text = string.Empty;
            CheckFinalInstallationPath(FolderBrowserDialog.SelectedPath);
            FolderBrowserDialog.SelectedPath = string.Empty;
            string fileName = "Serious Trouble.zip";
            //string fileName = "Debugging archive.zip";
            string pathToArchive = Path.GetFullPath(fileName, Path.GetTempPath());
            InitProgressBar(100);
            await DownloadArchiveAsync(pathToArchive);
            if (File.Exists(pathToArchive) == false) {
                UpdateButton.Enabled = true;
                return;
            }

            using (ZipArchive archive = ZipFile.OpenRead(pathToArchive)) {
                InitProgressBar(archive.Entries.Count);
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
                    this.SetupProgressBar.PerformStep();
                }
                RunButton.Enabled = true;
                StatusLabel.Text =
                    string.Format("Application installed by path '{0}' (total entries: {1})",
                    this.pathToApplication, archive.Entries.Count.ToString());
            }
            File.Delete(pathToArchive);
        }

        private async Task DownloadArchiveAsync(string path) {
            HttpClient client = new() {
                Timeout = TimeSpan.FromSeconds(120)
            };
            Progress<float> progress = new();
            progress.ProgressChanged += ProgressChangedEventHandler;

            try {
                if (this.launcherData == null || this.launcherData.versions.Count < 1) {
                    throw new ApplicationException("URL is not defined");
                }
                // TODO: To iterate through values
                //string url = "https://storage.yandexcloud.net/serious-trouble-resources/Debugging%20archive.zip";
                string url = this.launcherData.versions[0].buildPath;

                using FileStream file = new(path, FileMode.Create, FileAccess.Write, FileShare.None);
                await client.DownloadDataAsync(url, file, progress);
            } catch (Exception e) {
                ErrorLabel.Text = string.Format("Download archive exception: '{0}'", e.Message);
            } finally {
                client.Dispose();
                progress.ProgressChanged -= ProgressChangedEventHandler;
            }
        }

        private void ProgressChangedEventHandler(object? sender, float progress) {
            this.SetupProgressBar.Value = (int)progress;
            //this.StatusLabel.Text = progress.ToString();
        }

        //private void DownloadFileCompletedEventHandler(object? sender, AsyncCompletedEventArgs e) {
        //    throw new NotImplementedException();
        //}

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

        private void InitProgressBar(int count) {
            this.SetupProgressBar.Minimum = 0;
            this.SetupProgressBar.Maximum = count;
            this.SetupProgressBar.Value = 0;
        }
    }
}