using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Globalization;

namespace DynamicWCFProxyClient.Proxy.Models
{
	
	public class restriction
	{
		// ATTRIBUTES
		[XmlAttribute("base")]
		public string @base { get; set; }
		
		// ELEMENTS
		[XmlElement("pattern")]
		public pattern pattern { get; set; }
		
		[XmlElement("minInclusive")]
		public minInclusive minInclusive { get; set; }
		
		[XmlElement("maxInclusive")]
		public maxInclusive maxInclusive { get; set; }
		
		// CONSTRUCTOR
		public restriction()
		{}
	}
}
