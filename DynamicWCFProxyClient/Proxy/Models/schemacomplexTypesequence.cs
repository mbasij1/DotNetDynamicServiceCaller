using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Globalization;

namespace DynamicWCFProxyClient.Proxy.Models
{
	
	public class schemacomplexTypesequence
	{
		
		// ELEMENTS
		[XmlElement("element")]
		public List<Element> schemacomplexTypesequenceelement { get; set; }
		
		// CONSTRUCTOR
		public schemacomplexTypesequence()
		{}
	}
}
