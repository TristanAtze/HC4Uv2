using ChatApp;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace HC4U_2._0
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            string repoUrl = "https://github.com/TristanAtze/HC4Uv2.git";
            string repoDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "HC4Uv2");

            if (Directory.Exists(repoDir))
            {
                UpdateRepo(repoDir);
            }
            else
            {
                CloneRepo(repoUrl, repoDir);
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ChatForm());
        }

        private static void CloneRepo(string repoUrl, string repoDir)
        {
            ExecuteGitCommand($"clone {repoUrl} {repoDir}");
        }

        private static void UpdateRepo(string repoDir)
        {
            ExecuteGitCommand($"-C \"{repoDir}\" pull");
        }

        private static void ExecuteGitCommand(string arguments)
        {
            ProcessStartInfo processStartInfo = new ProcessStartInfo
            {
                FileName = "git",
                Arguments = arguments,
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using (Process process = Process.Start(processStartInfo))
            {
                using (StreamReader reader = process.StandardOutput)
                {
                    string output = reader.ReadToEnd();
                    Console.WriteLine(output); 
                }
            }
        }
    }
}
