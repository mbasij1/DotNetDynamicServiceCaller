using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Globalization;

namespace DynamicWCFProxyClient.Proxy.Models
{
	
	public class inputbody
	{
		// ATTRIBUTES
		[XmlAttribute("use")]
		public string use { get; set; }
		
		// ELEMENTS
		[XmlText]
		public string Value { get; set; }
		
		// CONSTRUCTOR
		public inputbody()
		{}
	}
}
