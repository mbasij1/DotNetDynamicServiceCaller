using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Globalization;

namespace DynamicWCFProxyClient.Proxy.Models
{
	
	public class definitionsbinding
	{
		// ATTRIBUTES
		[XmlAttribute("name")]
		public string name { get; set; }
		
		[XmlAttribute("type")]
		public string type { get; set; }
		
		// ELEMENTS
		[XmlElement("binding")]
		public bindingbinding bindingbinding { get; set; }
		
		[XmlElement("operation")]
		public List<bindingoperation> bindingoperation { get; set; }
		
		// CONSTRUCTOR
		public definitionsbinding()
		{}
	}
}
