using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Globalization;

namespace DynamicWCFProxyClient.Proxy.Models
{
	
	public class portType
	{
		// ATTRIBUTES
		[XmlAttribute("name")]
		public string name { get; set; }
		
		// ELEMENTS
		[XmlElement("operation")]
		public List<portTypeoperation> portTypeoperation { get; set; }
		
		// CONSTRUCTOR
		public portType()
		{}
	}
}
