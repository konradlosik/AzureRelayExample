namespace Microsoft.ServiceBus.Samples
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.ServiceModel;
    [ServiceContract(Name = "IEchoContract", Namespace = "https://samples.microsoft.com/ServiceModel/Relay/")]
    public interface IEchoContract
    {
        [OperationContract]
        //EchoResponse Echo(EchoReqest text);
        EchoResponse Echo(EchoReqest text);//
    }
}
