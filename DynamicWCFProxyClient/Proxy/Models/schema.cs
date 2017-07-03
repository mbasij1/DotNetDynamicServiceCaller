using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Globalization;

namespace DynamicWCFProxyClient.Proxy.Models
{
	
	public class schema
	{
		// ATTRIBUTES
		[XmlAttribute("elementFormDefault")]
		public string elementFormDefault { get; set; }
		
		[XmlAttribute("targetNamespace")]
		public string targetNamespace { get; set; }
		
		[XmlAttribute("attributeFormDefault")]
		public string attributeFormDefault { get; set; }
		
		// ELEMENTS
		[XmlElement("import")]
		public import import { get; set; }
		
		[XmlElement("element")]
		public List<schemaelement> schemaelement { get; set; }
		
		[XmlElement("simpleType")]
		public List<simpleType> simpleType { get; set; }
		
		[XmlElement("attribute")]
		public List<attribute> attribute { get; set; }
		
		[XmlElement("complexType")]
		public List<schemacomplexType> schemacomplexType { get; set; }
		
		// CONSTRUCTOR
		public schema()
		{}
	}
}
