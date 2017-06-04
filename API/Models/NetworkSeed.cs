using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Web;

namespace API.Models
{
    [Serializable]
    public class NetworkSeed
    {
        public NetworkSeed()
        {

        }

        public string Name { get; set; }
        public string Ticker { get; set; }

        public HttpPostedFileBase File { get; set; }
    }
}