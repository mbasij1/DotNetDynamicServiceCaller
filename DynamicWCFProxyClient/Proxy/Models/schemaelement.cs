using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Globalization;

namespace DynamicWCFProxyClient.Proxy.Models
{
	
	public class schemaelement
	{
		// ATTRIBUTES
		[XmlAttribute("name")]
		public string name { get; set; }
		
		[XmlIgnore]
		public bool? nillable { get; set; }
		[XmlAttribute("nillable")]
		public string nillableString
		{
			get { return nillable == null ? "" : nillable.Value ? "true" : "false"; }
			set
			{
				if (String.IsNullOrWhiteSpace(value)) nillable = null;
				else nillable = value == "true";
			}
		}
		
		[XmlAttribute("type")]
		public string type { get; set; }
		
		// ELEMENTS
		[XmlElement("complexType")]
		public List<elementcomplexType> elementcomplexType { get; set; }
		
		// CONSTRUCTOR
		public schemaelement()
		{}
	}
}
