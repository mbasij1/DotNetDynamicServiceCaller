using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicWCFProxyClient.Proxy.Structure
{
    public class ServiceStructure
    {
        public Dictionary<string, NameSpace> NameSpaces { get; set; }

        public ServiceStructure()
        {
            NameSpaces = new Dictionary<string, NameSpace>();
        }
    }
}
