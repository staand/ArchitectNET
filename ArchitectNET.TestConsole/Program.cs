using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading;
using System.Xml.Linq;

namespace ArchitectNET.TestConsole
{
    class Program
    {
        [A]
        class A : Attribute
        {
            
        }

        static void Main(string[] args)
        {
            try
            {
                var c = new StockGet.StockQuoteSoapClient(new BasicHttpBinding(),
                                                          new EndpointAddress(
                                                              "http://www.webservicex.net/stockquote.asmx"));
                try
                {
                    ICommunicationObject obj = c;
                    obj.BeginOpen(
                        TimeSpan.FromSeconds(5),
                        async r =>
                        {
                            obj.EndOpen(r);
                            var t = await c.GetQuoteAsync("MA");
                            var xml = XElement.Parse(t);
                            Console.WriteLine(xml);
                        },
                        null);
                    Thread.Sleep(30 * 1000);
                }
                finally
                {
                    c.Close();
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }
    }
}