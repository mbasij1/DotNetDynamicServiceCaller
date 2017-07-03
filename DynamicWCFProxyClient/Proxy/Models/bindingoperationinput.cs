using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Globalization;

namespace DynamicWCFProxyClient.Proxy.Models
{
	
	public class bindingoperationinput
	{
		
		// ELEMENTS
		[XmlElement("body")]
		public inputbody inputbody { get; set; }
		
		// CONSTRUCTOR
		public bindingoperationinput()
		{}
	}
}
