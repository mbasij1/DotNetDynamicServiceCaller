using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Globalization;

namespace DynamicWCFProxyClient.Proxy.Models
{
	
	public class elementcomplexTypesequence
	{
		
		// ELEMENTS
		[XmlElement("element")]
		public List<Element> elementcomplexTypesequenceelement { get; set; }
		
		// CONSTRUCTOR
		public elementcomplexTypesequence()
		{}
	}
}
