using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Globalization;

namespace DynamicWCFProxyClient.Proxy.Models
{
	
	public class Element
	{
		// ATTRIBUTES
		[XmlAttribute("minOccurs")]
		public int minOccurs  { get; set; }
		
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
		
		[XmlAttribute("maxOccurs")]
		public string maxOccurs { get; set; }
		
		// ELEMENTS
		[XmlText]
		public string Value { get; set; }
		
		// CONSTRUCTOR
		public Element()
		{}
	}
}
