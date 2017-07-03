using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Globalization;

namespace DynamicWCFProxyClient.Proxy.Models
{
	
	public class bindingoperation
	{
		// ATTRIBUTES
		[XmlAttribute("name")]
		public string name { get; set; }
		
		// ELEMENTS
		[XmlElement("operation")]
		public operationoperation operationoperation { get; set; }
		
		[XmlElement("input")]
		public bindingoperationinput bindingoperationinput { get; set; }
		
		[XmlElement("output")]
		public bindingoperationoutput bindingoperationoutput { get; set; }
		
		// CONSTRUCTOR
		public bindingoperation()
		{}
	}
}
