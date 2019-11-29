
namespace Microsoft.ServiceBus.Samples
{
    using System.Runtime.Serialization;
    [DataContract]
    public class EchoResponse
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Content { get; set; }
    }
}
