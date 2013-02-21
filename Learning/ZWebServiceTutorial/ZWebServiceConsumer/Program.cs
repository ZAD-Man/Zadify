using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using ZWebServiceConsumer.ZWebServiceReference;

namespace ZWebServiceConsumer
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread.Sleep(1000);
            var webService = new Service1();
            Console.WriteLine(webService.WebMethodTheFirst("ZAD-Man", "Cypon"));
            Console.ReadLine();
        }
    }
}
