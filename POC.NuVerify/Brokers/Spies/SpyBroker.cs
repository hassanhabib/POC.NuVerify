using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POC.NuVerify.Brokers.Spies
{
    internal class SpyBroker
    {
        public string[] GenerateFilesFromDll(string dllPath)
        {
            string currentLocation = Directory.GetCurrentDirectory();
            Guid tempLocation = Guid.NewGuid();
            string command = $"/c ilspycmd --nested-directories -p -o {currentLocation}\\{tempLocation} {dllPath}";

            var process = new Process();
            process.StartInfo.FileName = "cmd";
            process.StartInfo.Arguments = command;
            process.StartInfo.RedirectStandardOutput = true;
            process.Start();
            process.WaitForExit();

            string targetDirectory = $"{currentLocation}\\{tempLocation}\\";

            return Directory.GetFiles(targetDirectory, "*.*", SearchOption.AllDirectories);
        }
    }
}
