using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace DynamicWCFProxyClient.Proxy
{
    public class NamespaceIgnorantXmlTextReader : XmlTextReader
    {
        public NamespaceIgnorantXmlTextReader(Stream reader) : base(reader) { }

        public override string NamespaceURI
        {
            get { return ""; }
        }
    }
}
