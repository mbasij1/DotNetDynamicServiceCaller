using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Globalization;

namespace DynamicWCFProxyClient.Proxy.Models
{
	
	public class portTypeoperation
	{
		// ATTRIBUTES
		[XmlAttribute("name")]
		public string name { get; set; }
		
		// ELEMENTS
		[XmlElement("input")]
		public portTypeoperationinput portTypeoperationinput { get; set; }
		
		[XmlElement("output")]
		public portTypeoperationoutput portTypeoperationoutput { get; set; }
		
		// CONSTRUCTOR
		public portTypeoperation()
		{}
	}
}
