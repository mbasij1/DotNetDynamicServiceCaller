using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Globalization;

namespace DynamicWCFProxyClient.Proxy.Models
{
	
	public class message
	{
		// ATTRIBUTES
		[XmlAttribute("name")]
		public string name { get; set; }
		
		// ELEMENTS
		[XmlElement("part")]
		public part part { get; set; }
		
		// CONSTRUCTOR
		public message()
		{}
	}
}
