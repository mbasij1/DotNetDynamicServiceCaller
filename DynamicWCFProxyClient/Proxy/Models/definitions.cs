using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Globalization;

namespace DynamicWCFProxyClient.Proxy.Models
{
	
	public class definitions
	{
		// ATTRIBUTES
		[XmlAttribute("name")]
		public string name { get; set; }
		
		[XmlAttribute("targetNamespace")]
		public string targetNamespace { get; set; }
		
		// ELEMENTS
		[XmlElement("types")]
		public types types { get; set; }
		
		[XmlElement("message")]
		public List<message> message { get; set; }
		
		[XmlElement("portType")]
		public portType portType { get; set; }
		
		[XmlElement("binding")]
		public definitionsbinding definitionsbinding { get; set; }
		
		[XmlElement("service")]
		public service service { get; set; }
		
		// CONSTRUCTOR
		public definitions()
		{}
	}
}
