using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Globalization;

namespace DynamicWCFProxyClient.Proxy.Models
{
	
	public class portTypeoperationoutput
	{
		// ATTRIBUTES
		[XmlAttribute("Action")]
		public string Action { get; set; }
		
		[XmlAttribute("message")]
		public string message { get; set; }
		
		// ELEMENTS
		[XmlText]
		public string Value { get; set; }
		
		// CONSTRUCTOR
		public portTypeoperationoutput()
		{}
	}
}
