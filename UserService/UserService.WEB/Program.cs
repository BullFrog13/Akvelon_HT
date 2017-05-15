using System;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace UserService.WEB
{
    public class Program
    {
        private static ServiceHost _host;

        public static void Main()
        {
            var httpUrl = new Uri("http://localhost:5000/UserService");

            //Create ServiceHost
            _host = new ServiceHost(typeof(HttpServices.UserService), httpUrl);

            //Enable metadata exchange
            var smb = new ServiceMetadataBehavior
            {
                HttpGetEnabled = true,
                MetadataExporter = { PolicyVersion = PolicyVersion.Policy15 }
            };

            _host.Description.Behaviors.Add(smb);

            _host.Open();
            Console.WriteLine("Service is hosted at " + httpUrl);
            Console.WriteLine("Host is running... Press  key to stop");
            Console.ReadLine();

            _host.Close();
            Environment.Exit(0);
        }
    }
}