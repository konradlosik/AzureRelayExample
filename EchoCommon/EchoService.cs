namespace Microsoft.ServiceBus.Samples
{
    using System;
    using System.ServiceModel;

    [ServiceBehavior(Name = "EchoService", Namespace = "https://samples.microsoft.com/ServiceModel/Relay/")]
    public class EchoService : IEchoContract
    {
        public EchoResponse Echo(EchoReqest request)
        {
            Console.WriteLine("Echo is: " + request != null ? request.Content : "null");
            return new EchoResponse() { Id = DateTime.Now.Minute*100 + DateTime.Now.Second ,
                Content = (request != null && !string.IsNullOrEmpty(request.Content) ? request.Content.ToLower() : "null") + "123"
        };
        }
    }
}
