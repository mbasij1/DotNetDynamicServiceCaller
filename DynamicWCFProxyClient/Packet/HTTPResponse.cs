using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicWCFProxyClient.Packet
{
    public class HTTPResponse
    {
        public HTTPResponse()
        {
            Headers = new Dictionary<string, string>();
        }

        public int ResponseCode { get; set; }

        public string ResponseDetail { get; set; }

        public Dictionary<string, string> Headers { get; set; }

        public string Body { get; set; }
    }
}
