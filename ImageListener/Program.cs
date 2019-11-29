namespace Microsoft.ServiceBus.Samples
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.ServiceModel;
    using System.ServiceModel.Channels;
    using System.ServiceModel.Web;
    using System.IO;
    using System.Drawing;
    using System.Drawing.Imaging;
    using Microsoft.ServiceBus;
    using Microsoft.ServiceBus.Web;

    class Program
    {
        static void Main(string[] args)
        {
            string serviceNamespace = "doctorsmobilevisit";//"Relay";
            Uri address = ServiceBusEnvironment.CreateServiceUri("https", serviceNamespace, "Image");
            WebServiceHost host = new WebServiceHost(typeof(ImageService), address);
            host.Open();
            //zezwoliłem w regulach zapory na ruch w obie strony dla: 9352,9352,80,443

            Console.WriteLine("Copy the following address into a browser to see the image: ");
            Console.WriteLine(address + "GetImage");
            Console.WriteLine();
            Console.WriteLine("Press [Enter] to exit");
            Console.ReadLine();
            host.Close();
        }
    }

    [ServiceContract(Name = "ImageContract", Namespace = "https://samples.microsoft.com/ServiceModel/Relay/RESTTutorial1")]
    public interface IImageContract
    {
        [OperationContract, WebGet]
        Stream GetImage();
    }
    public interface IImageChannel : IImageContract, IClientChannel { }

    [ServiceBehavior(Name = "ImageService", Namespace = "https://samples.microsoft.com/ServiceModel/Relay/")]
    public class ImageService : IImageContract
    {
        const string imageFileName = "image.jpg";
        Image bitmap;
        public ImageService()
        {
            this.bitmap = Image.FromFile(imageFileName);
        }
        public Stream GetImage()
        {
            MemoryStream stream = new MemoryStream();
            this.bitmap.Save(stream, ImageFormat.Jpeg);

            stream.Position = 0;
            WebOperationContext.Current.OutgoingResponse.ContentType = "image/jpeg";

            return stream;
        }
    }
}
