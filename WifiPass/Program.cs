using System;
using System.Diagnostics;
using System.Linq;

namespace WifiPass
{
    class Program
    {
        static void Main()
        {
            Console.Title = "";
            FindAP("netsh wlan show profile");            
            Console.Read();
        }
        
        private static void FindAP(string input)
        {
            try
            {                
                ProcessStartInfo procStartInfo = new ProcessStartInfo("cmd", "/c " + input)
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
                                
                AccessPointGetter(proc.StandardOutput.ReadToEnd());
                proc.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);                
            }
        }

        private static void AccessPointGetter(string input)
        {
            var ListOfWords = input.Split();

            for (int i = 0; i < ListOfWords.Length; i++)
            {
                if (ListOfWords[i] == ":")
                {
                    i++;
                    AccessPoints(ListOfWords[i]);
                }
            }
        }

        private static void AccessPoints(string input)
        {
            string command = "netsh wlan show profile " + input + " key=clear";
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

                string a = proc.StandardOutput.ReadToEnd();
                
                string lastWord = a.Split(' ').Last();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Access Point: {input} Password: {lastWord}");

                proc.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }    
}
