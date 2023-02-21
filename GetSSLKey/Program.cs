using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;dotnet 
using System.Threading;
using System.Threading.Tasks;

namespace GetSSLKey
{
    internal class Program
    {
        static void Main(string[] args)
        {


            byte[] key = GetSSL();
            string hexValue ="";
            Console.WriteLine("\n\nSSL in Bytes:");
            for (int i = 0; i < key.Length; i++)
            {
                Console.Write(key[i]);
                hexValue += key[i].ToString("X2");

            }
            Console.WriteLine("\n\nSSL in Hex2: \n" + hexValue + "\n\n");


            Console.ReadKey();
        }


        static byte[] GetSSL()
        {

            try
            {
                Uri u = new Uri(GetURL());
                ServicePoint sp = ServicePointManager.FindServicePoint(u);

                string groupName = Guid.NewGuid().ToString();
                HttpWebRequest req = HttpWebRequest.Create(u) as HttpWebRequest;
                req.ConnectionGroupName = groupName;

                using (WebResponse resp = req.GetResponse())
                {

                }
                sp.CloseConnectionGroup(groupName);
                byte[] key = sp.Certificate.GetPublicKey();

                return key;
            }
            catch (Exception)
            {
                Console.WriteLine("Something went wrong! Terminating program...");
                Console.ReadKey();
                throw;
            }

        }

        static string GetURL()
        {
        start:
            Console.Clear();
            string URL = "";
            try
            {
                Console.Write("Enter URL: ");
                URL = Console.ReadLine();
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid URL");
                goto start;
            }
            return URL;
        }



    }
}
