using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Globalization;

namespace DynamicWCFProxyClient.Proxy.Models
{
	
	public class bindingoperationoutput
	{
		
		// ELEMENTS
		[XmlElement("body")]
		public outputbody outputbody { get; set; }
		
		// CONSTRUCTOR
		public bindingoperationoutput()
		{}
	}
}
