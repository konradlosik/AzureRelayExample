namespace Microsoft.ServiceBus.Samples
{
    using System.ServiceModel;
    using System.Runtime.Serialization;
    [DataContract]
    public class EchoReqest
    {
        [DataMember]
        public string Content { get; set; }
    }
}
