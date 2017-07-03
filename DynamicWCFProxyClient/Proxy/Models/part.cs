using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Globalization;

namespace DynamicWCFProxyClient.Proxy.Models
{
	
	public class part
	{
		// ATTRIBUTES
		[XmlAttribute("name")]
		public string name { get; set; }
		
		[XmlAttribute("element")]
		public string element { get; set; }
		
		// ELEMENTS
		[XmlText]
		public string Value { get; set; }
		
		// CONSTRUCTOR
		public part()
		{}
	}
}
