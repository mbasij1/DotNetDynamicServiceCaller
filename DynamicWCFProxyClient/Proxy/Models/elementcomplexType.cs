using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Globalization;

namespace DynamicWCFProxyClient.Proxy.Models
{
	
	public class elementcomplexType
	{
		
		// ELEMENTS
		[XmlElement("sequence")]
		public elementcomplexTypesequence elementcomplexTypesequence { get; set; }
		
		// CONSTRUCTOR
		public elementcomplexType()
		{}
	}
}
