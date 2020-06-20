using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace WifiPass
{
    class GetAccessPoint
    {
        public List<string> AccessPoint = new List<string>();
        public GetAccessPoint(string command = "netsh wlan show profile")
        {
            if (command.EndsWith("profile"))
            {

            }
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

                string standard_output;
                while ((standard_output = proc.StandardOutput.ReadLine()) != null)
                {
                    if (standard_output.Contains("User Profile"))
                    {
                        string[] ap = standard_output.Split(':');
                        AccessPoint.Add(ap[1]);                        
                    }
                }
                proc.Dispose();
            }
            catch (Exception)
            {
                Console.WriteLine("Error finding access points");

            }            
        }
    }
}
