using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Globalization;

namespace DynamicWCFProxyClient.Proxy.Models
{
	
	public class schemacomplexType
	{
		// ATTRIBUTES
		[XmlAttribute("name")]
		public string name { get; set; }
		
		// ELEMENTS
		[XmlElement("sequence")]
		public schemacomplexTypesequence schemacomplexTypesequence { get; set; }
		
		// CONSTRUCTOR
		public schemacomplexType()
		{}
	}
}
