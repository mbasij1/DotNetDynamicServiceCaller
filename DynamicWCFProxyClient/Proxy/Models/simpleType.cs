using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Globalization;

namespace DynamicWCFProxyClient.Proxy.Models
{
	
	public class simpleType
	{
		// ATTRIBUTES
		[XmlAttribute("name")]
		public string name { get; set; }
		
		// ELEMENTS
		[XmlElement("restriction")]
		public restriction restriction { get; set; }
		
		// CONSTRUCTOR
		public simpleType()
		{}
	}
}
