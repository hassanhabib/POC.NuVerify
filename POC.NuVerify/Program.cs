using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using POC.NuVerify.Brokers.Githubs;
using POC.NuVerify.Brokers.Nugets;
using POC.NuVerify.Brokers.OpenAIs;
using POC.NuVerify.Brokers.Spies;
using POC.NuVerify.Models.Nugets;

namespace POC.NuVerify
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var spyBroker = new SpyBroker();
            var nugetBroker = new NugetBroker();
            var githubBroker = new GithubBroker();
            var openAiBroker = new OpenAIBroker();

            string fileLocation =
                nugetBroker.DownloadNugetPackage("Standard.AI.OpenAI");

            string[] allFiles = spyBroker.GenerateFilesFromDll(fileLocation);

            NugetPackage nugetPackageDetails =
                await nugetBroker.GetNugetPackageDetailsAsync("Standard.AI.OpenAI");

            string gitProjectDirectory = 
                githubBroker.CloneGitProject(nugetPackageDetails.data[0].projectUrl);

            var allFilesResults = new List<(string File, string Result)>();

            foreach(string file in allFiles)
            {
                string fileName = file.Substring(file.LastIndexOf('\\') + 1);
                string nugetFileContent = File.ReadAllText(file);

                string maybeGitFile = Directory.GetFiles(gitProjectDirectory, fileName, SearchOption.AllDirectories)
                    .FirstOrDefault();

                if (maybeGitFile is not null)
                {
                    string gitFileContent = File.ReadAllText(maybeGitFile);

                    try
                    {
                        string result = await openAiBroker.ScanFilesAsync(
                            nugetFileContent,
                            gitFileContent);

                        allFilesResults.Add((maybeGitFile, result));
                    }
                    catch
                    {
                        allFilesResults.Add((maybeGitFile, "AI ERROR"));
                    }
                }
                else
                {
                    allFilesResults.Add((fileName, "NO MATCH"));
                }
            }

            Console.WriteLine("Hello, World!");
        }
    }
}