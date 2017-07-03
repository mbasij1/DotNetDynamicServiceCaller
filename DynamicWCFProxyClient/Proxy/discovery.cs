using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DynamicWCFProxyClient.Proxy
{

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.xmlsoap.org/disco/")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://schemas.xmlsoap.org/disco/", IsNullable = false)]
    public partial class discovery
    {

        private contractRef[] contractRefField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("contractRef", Namespace = "http://schemas.xmlsoap.org/disco/scl/")]
        public contractRef[] contractRef
        {
            get
            {
                return this.contractRefField;
            }
            set
            {
                this.contractRefField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.xmlsoap.org/disco/scl/")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://schemas.xmlsoap.org/disco/scl/", IsNullable = false)]
    public partial class contractRef
    {

        private string refField;

        private string docRefField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string @ref
        {
            get
            {
                return this.refField;
            }
            set
            {
                this.refField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string docRef
        {
            get
            {
                return this.docRefField;
            }
            set
            {
                this.docRefField = value;
            }
        }
    }
}
