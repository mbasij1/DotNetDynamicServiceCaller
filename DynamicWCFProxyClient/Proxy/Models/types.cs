using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Globalization;

namespace DynamicWCFProxyClient.Proxy.Models
{
	
	public class types
	{
		
		// ELEMENTS
		[XmlElement("schema")]
		public List<schema> schema { get; set; }
		
		// CONSTRUCTOR
		public types()
		{}
	}
}
