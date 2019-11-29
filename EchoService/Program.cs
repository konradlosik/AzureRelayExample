namespace Microsoft.ServiceBus.Samples//Relay
{
    using System;
    using System.ServiceModel;
    using Microsoft.ServiceBus;
    using System.ServiceModel.Description;
    using Microsoft.ServiceBus.Description;
    class Program
    {
        static void Main(string[] args)
        {
            string serviceNamespace, sasKey;
            AccessHelper.SetAccessPolicies(out serviceNamespace, out sasKey);

            //zadeklaruj, że będziesz używać klucza SAS jako typu referencji
            TransportClientEndpointBehavior sasCredential = new TransportClientEndpointBehavior();
            sasCredential.TokenProvider = TokenProvider.CreateSharedAccessSignatureTokenProvider("RootManageSharedAccessKey", sasKey);
            //Utwórz adres bazowy dla usługi
            Uri address = ServiceBusEnvironment.CreateServiceUri("sb", serviceNamespace, "EchoService");
            ServiceBusEnvironment.SystemConnectivity.Mode = ConnectivityMode.AutoDetect;
            ServiceHost host = new ServiceHost(typeof(EchoService), address);
            //skonfiguruj punkt końcowy, aby umożliwić dostęp publiczny
            IEndpointBehavior serviceRegistrySettings = new ServiceRegistrySettings(DiscoveryType.Public);
            //Zastosuj poświadczenia usługi do punktów końcowych usługi zdefiniowanych w pliku App.config
            foreach (ServiceEndpoint endpoint in host.Description.Endpoints)
            {
                endpoint.Behaviors.Add(serviceRegistrySettings);
                endpoint.Behaviors.Add(sasCredential);
            }
            host.Open();
            Console.WriteLine("Service address: " + address);
            Console.WriteLine("Press [Enter] to exit");
            Console.ReadLine();
            host.Close();
        }
    }
}
