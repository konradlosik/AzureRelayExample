namespace Microsoft.ServiceBus.Samples
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.ServiceModel;
    using System.Text;
    using System.Threading.Tasks;
    class Program
    {
        static void Main(string[] args)
        {
            string serviceNamespace, sasKey;
            ServiceBusEnvironment.SystemConnectivity.Mode = ConnectivityMode.AutoDetect;
            AccessHelper.SetAccessPolicies(out serviceNamespace, out sasKey);

            Uri serviceUri = ServiceBusEnvironment.CreateServiceUri("sb", serviceNamespace, "EchoService");
            //Utwórz obiekt referencji dla punktu końcowego przestrzeni nazw usługi.
            TransportClientEndpointBehavior sasCredential = new TransportClientEndpointBehavior();
            sasCredential.TokenProvider = TokenProvider.CreateSharedAccessSignatureTokenProvider("RootManageSharedAccessKey", sasKey);
            //Utwórz fabrykę kanałów, która ładuje konfigurację opisaną w pliku App.config
            ChannelFactory<IEchoChannel> channelFactory = new ChannelFactory<IEchoChannel>("RelayEndpoint", new EndpointAddress(serviceUri));
            //Zastosuj poświadczenia.
            channelFactory.Endpoint.Behaviors.Add(sasCredential);
            //Utwórz i otwórz kanał do usługi.
            IEchoChannel channel = channelFactory.CreateChannel();
            channel.Open();
            //Napisz podstawowy interfejs użytkownika i funkcje echa.
            Console.WriteLine("Enter text to echo (or [Enter] to exit):");
            string input = Console.ReadLine();
            while (input != String.Empty)
            {
                try
                {
                    var echo = channel.Echo(new EchoReqest() { Content = input });
                    Console.WriteLine("Server echoed: {0},id={1}", echo.Content,echo.Id);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error: " + e.Message);
                }
                input = Console.ReadLine();
            }

            channel.Close();
            channelFactory.Close();
        }
    }
}
