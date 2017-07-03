using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicWCFProxyClient.Proxy.Structure
{
    public class SMethodArgumant
    {
        public string Name { get; set; }

        public SType Type { get; set; }

        public string NameSpace { get; set; }

        public void SetValue(Request request, object Value)
        {
            var temp = request.Argumants.SingleOrDefault(a => a.Name == Name);
            if (temp == null)
                request.Argumants.Add(new Variable() { Name = Name, Value = Value, NameSpace = NameSpace });
            else
                temp.Value = Value;
        }
    }
}
