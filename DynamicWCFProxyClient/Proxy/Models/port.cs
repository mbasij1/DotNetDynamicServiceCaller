using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Globalization;

namespace DynamicWCFProxyClient.Proxy.Models
{
	
	public class port
	{
		// ATTRIBUTES
		[XmlAttribute("name")]
		public string name { get; set; }
		
		[XmlAttribute("binding")]
		public string binding { get; set; }
		
		// ELEMENTS
		[XmlElement("address")]
		public address address { get; set; }
		
		// CONSTRUCTOR
		public port()
		{}
	}
}
