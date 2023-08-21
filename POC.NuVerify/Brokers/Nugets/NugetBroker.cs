using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using POC.NuVerify.Models.Nugets;
using RESTFulSense.Clients;

namespace POC.NuVerify.Brokers.Nugets
{
    internal class NugetBroker
    {
        public string DownloadNugetPackage(string packageName)
        {
            string currentLocation = Directory.GetCurrentDirectory();
            Guid tempLocation = Guid.NewGuid();
            string command = $"/c cd {currentLocation}& nuget.exe install {packageName} -DirectDownload -OutputDirectory {tempLocation}";

            var process = new Process();
            process.StartInfo.FileName = "cmd.exe";
            process.StartInfo.Arguments = command;
            //process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.UseShellExecute = true;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.Start();
            process.WaitForExit();

            string targetDirectory =
                $"{currentLocation}\\{tempLocation}";

            string file = 
                Directory.GetFiles(targetDirectory, "*.dll" , SearchOption.AllDirectories)
                    .FirstOrDefault(file => file.Contains($"{packageName}.dll"));

            return file;
        }

        public async ValueTask<NugetPackage> GetNugetPackageDetailsAsync(string packageName)
        {
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://azuresearch-usnc.nuget.org/");
            var restfulApiClient = new RESTFulApiFactoryClient(httpClient);

            return await restfulApiClient.GetContentAsync<NugetPackage>(
                relativeUrl: $"query?q={packageName}");
        }
    }
}
