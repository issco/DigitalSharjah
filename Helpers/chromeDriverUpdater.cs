using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.IO.Compression;
using System.Reflection;
using System.Threading.Tasks;

public class ChromeDriverUpdater
{
    public async Task UpdateChromeDriverAsync()
    {
        string latestVersion = await GetLatestChromeDriverVersion();
        string windowsDownloadUrl = $"https://edgedl.me.gvt1.com/edgedl/chrome/chrome-for-testing/{latestVersion}/win64/chromedriver-win64.zip";
        string macDownloadUrl = $"https://edgedl.me.gvt1.com/edgedl/chrome/chrome-for-testing/{latestVersion}/mac-x64/chromedriver-mac-x64.zip";

        string assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        string chromeDriverFolder = Path.Combine(assemblyPath, "Drivers");

        UpdateChromeDriver(windowsDownloadUrl, Path.Combine(chromeDriverFolder, "chromedriver.exe"), "chromedriver.exe");
        UpdateChromeDriver(macDownloadUrl, Path.Combine(chromeDriverFolder, "chromedriver"), "chromedriver");

        Console.WriteLine("ChromeDriver update completed.");
    }

    private async Task<string> GetLatestChromeDriverVersion()
    {
        using (HttpClient client = new HttpClient())
        {
            var response = await client.GetStringAsync("https://googlechromelabs.github.io/chrome-for-testing/last-known-good-versions.json");
            var jsonDocument = JsonDocument.Parse(response);
            var stableVersion = jsonDocument.RootElement.GetProperty("channels").GetProperty("Stable").GetProperty("version").GetString();
            return stableVersion;
        }
    }

    private void UpdateChromeDriver(string downloadUrl, string targetPath, string driverNameByOs)
    {
        using (HttpClient client = new HttpClient())
        {
            byte[] driverBytes = client.GetByteArrayAsync(downloadUrl).Result;

            // Create a temporary file to save the downloaded ZIP
            string tempZipPath = Path.Combine(Path.GetTempPath(), "chromedriver.zip");
            File.WriteAllBytes(tempZipPath, driverBytes);

            // Create the destination directory if it doesn't exist
            string driversDirectory = Path.GetDirectoryName(targetPath);
            string tempExtractFolder = Path.Combine(driversDirectory, "chromedriver_temp");
            Directory.CreateDirectory(tempExtractFolder);

            // Extract the contents of the ZIP with custom handling
            using (var archive = new ZipArchive(new MemoryStream(driverBytes), ZipArchiveMode.Read))
            {
                foreach (var entry in archive.Entries)
                {
                    // Exclude the LICENSE.chromedriver file
                    if (!entry.FullName.Contains("LICENSE.chromedriver"))
                    {
                        // Identify the correct filename and path within the ZIP archive
                        if (entry.FullName.Contains(driverNameByOs))
                        {
                            string entryPath = Path.Combine(tempExtractFolder, Path.GetFileName(entry.FullName));
                            entry.ExtractToFile(entryPath, true);

                            // Preserve executable attribute (for Windows)
                            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                            {
                                File.SetAttributes(entryPath, File.GetAttributes(entryPath) | FileAttributes.Normal);
                            }

                            // Set executable permission (for macOS)
                            if (Environment.OSVersion.Platform == PlatformID.Unix || Environment.OSVersion.Platform == PlatformID.MacOSX)
                            {
                                Process.Start("chmod", $"+x {entryPath}").WaitForExit();
                            }


                            break; // Stop iterating after finding the correct file
                        }
                    }
                }
            }

            // Copy the extracted chromedriver file to the target path
            string chromedriverPath = Path.Combine(tempExtractFolder, Path.GetFileName(targetPath));
            File.Copy(chromedriverPath, targetPath, true);

            // Clean up temporary files and folder
            File.Delete(tempZipPath);
            Directory.Delete(tempExtractFolder, true);
        }
    }




}
