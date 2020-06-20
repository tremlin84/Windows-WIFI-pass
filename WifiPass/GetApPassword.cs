using System;
using System.Diagnostics;
using System.Linq;

namespace WifiPass
{
    class GetApPassword
    {
        public GetApPassword(string input)
        {
            string command = "netsh wlan show profile" + input + " key=clear";
            try
            {
                ProcessStartInfo procStartInfo = new ProcessStartInfo("cmd", "/c " + command)
                {
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                Process proc = new Process
                {
                    StartInfo = procStartInfo
                };
                proc.Start();

                string results;
                while ((results = proc.StandardOutput.ReadLine()) != null)
                {
                    if (results.Contains("Key Content"))
                    {
                        string[] ap = results.Split(':');                        
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"Access Point:{input} Password:{ap[1]}");
                    }                    
                }                

                proc.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}
