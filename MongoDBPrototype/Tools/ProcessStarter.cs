using System.Diagnostics;

namespace MongoDBPrototype
{
    public class ProcessStarter
    {
        private static Process _process;

        public static void StartProcess(string fileName, string arguments = "")
        {
            _process = new Process
            {
                StartInfo =
                {
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    FileName = fileName,
                    Arguments = arguments
                }
            };
            _process.Start();
        }

        public static void StartProcessAndWaitForExit(string fileName, string arguments = "")
        {
            StartProcess(fileName, arguments);
            _process.WaitForExit();
        }
    }
}