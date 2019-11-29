namespace Microsoft.ServiceBus.Samples
{
    using System;
    public class AccessHelper
    {
        public static void SetAccessPolicies(out string serviceNamespace, out string sasKey)
        {
            Console.Write("Your Service Namespace: ");
            serviceNamespace = Console.ReadLine();
            Console.Write("Your SAS key: ");
            sasKey = Console.ReadLine();
        }
    }
}
