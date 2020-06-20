using System;
using System.Diagnostics;
using System.Linq;

namespace WifiPass
{
    class Program
    {
        static void Main()
        {
            Console.Title = "Wifi Password Finder";
            GetAccessPoint x = new GetAccessPoint();
            x.AccessPoint.ForEach(i => new GetApPassword(i));
                       
            Console.Read();
        }       
    }
}
