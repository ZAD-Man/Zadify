using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using ServiceStack.ServiceHost;

namespace Zadify
{
    /// <summary>
    /// Define your ServiceStack web service request (i.e. the Request DTO).
    /// </summary>    
    public class Hello
    {
        public string Name { get; set; }
    }

    /// <summary>
    /// Define your ServiceStack web service response (i.e. Response DTO).
    /// </summary>
    public class HelloResponse
    {
        public string Result { get; set; }
    }

    /// <summary>
    /// Create your ServiceStack web service implementation.
    /// </summary>
    public class HelloService : IService
    {
        public object Any(Hello request)
        {
            //Looks strange when the name is null so we replace with a generic name.
            var name = request.Name ?? "John Doe";
            return new HelloResponse { Result = "Hello, " + name };
        }
    }
}