using System;
using System.Net;

namespace GetSSLKey
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var sslKey = GetSSL();
            var hexValue = BitConverter.ToString(sslKey).Replace("-", "");

            Console.WriteLine("\n\nSSL in Bytes:");
            Console.WriteLine(string.Join(" ", sslKey));

            Console.WriteLine("\n\nSSL in Hex2: \n" + hexValue + "\n\n");
            Console.ReadKey();
        }

        static byte[] GetSSL()
        {
            try
            {
                var uri = new Uri(GetURL());
                var sp = ServicePointManager.FindServicePoint(uri);

                var groupName = Guid.NewGuid().ToString();
                var request = (HttpWebRequest)WebRequest.Create(uri);
                request.ConnectionGroupName = groupName;

                using (var response = request.GetResponse()) { }

                sp.CloseConnectionGroup(groupName);

                return sp.Certificate.GetPublicKey();
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
            while (true)
            {
                Console.Clear();
                Console.Write("Enter URL: ");

                var url = Console.ReadLine();
                if (Uri.TryCreate(url, UriKind.Absolute, out Uri uri) && (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps))
                {
                    return url;
                }
                Console.WriteLine("Invalid URL");
            }
        }
    }
}
