using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicWCFProxyClient.Proxy.Structure
{
    public class Request
    {
        public string Action { get; set; }

        public string ActionName { get; set; }

        public List<Variable> Argumants { get; set; }

        public Request()
        {
            Argumants = new List<Variable>();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<s:Envelope xmlns:s=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:i=\"http://www.w3.org/2001/XMLSchema-instance\"><s:Body>");
            sb.Append($"<{ActionName} xmlns=\"http://tempuri.org/\">");
            foreach (var argumant in Argumants)
                sb.Append(argumant.ToString());

            sb.Append($"</{ActionName}>");
            sb.Append("</s:Body></s:Envelope>");
            return sb.ToString();
        }
    }
}
