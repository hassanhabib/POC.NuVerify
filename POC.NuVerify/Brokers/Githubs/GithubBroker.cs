using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POC.NuVerify.Brokers.Githubs
{
    internal class GithubBroker
    {
        public string CloneGitProject(string projectUrl)
        {
            string currentLocation = Directory.GetCurrentDirectory();
            Guid tempLocation = Guid.NewGuid();
            string command = $"/c git clone {projectUrl} {currentLocation}\\{tempLocation}";

            var process = new Process();
            process.StartInfo.FileName = "cmd";
            process.StartInfo.Arguments = command;
            process.StartInfo.RedirectStandardOutput = true;
            process.Start();
            process.WaitForExit();

            string targetDirectory = $"{currentLocation}\\{tempLocation}\\";

            return targetDirectory;
        }
    }
}
