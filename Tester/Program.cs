using System;
using System.Diagnostics;

namespace Tester
{
    class Program
    {
        static void Main(string[] args)
        {
            string cmd = "python main.py 123456 1 {\\\"Ano\\\":[\\\"2020\\\"],\\\"Mes\\\":[\\\"1\\\",\\\"2\\\"]}";
          

            string startDir = @"C:\Gradius\TrainerEngine";
            Console.WriteLine($"/c {cmd}");
            var process = new Process()
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    Arguments = $"/c {cmd}",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    WorkingDirectory = startDir
                }
            };
            process.Start();
            string result = process.StandardOutput.ReadToEnd();
            process.WaitForExit();
            Console.WriteLine(result);

            Console.WriteLine("Hello World!");
            Console.ReadKey();
           
        }
    }
}
