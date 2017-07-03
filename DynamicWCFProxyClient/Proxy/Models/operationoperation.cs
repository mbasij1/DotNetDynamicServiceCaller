using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Globalization;

namespace DynamicWCFProxyClient.Proxy.Models
{
	
	public class operationoperation
	{
		// ATTRIBUTES
		[XmlAttribute("soapAction")]
		public string soapAction { get; set; }
		
		[XmlAttribute("style")]
		public string style { get; set; }
		
		// ELEMENTS
		[XmlText]
		public string Value { get; set; }
		
		// CONSTRUCTOR
		public operationoperation()
		{}
	}
}
