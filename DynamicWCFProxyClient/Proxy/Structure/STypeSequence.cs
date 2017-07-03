using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicWCFProxyClient.Proxy.Structure
{
    public class STypeSequence
    {
        public string Name { get; set; }

        public string TypeName { get; set; }

        public string NameSpace { get; set; }

        public void SetValue(Variable instance, object Value)
        {
            if (!(instance.Value is List<Variable>))
                throw new Exception("This is Not Complex Type");

            var temp = (instance.Value as List<Variable>).SingleOrDefault(a => a.Name == Name);
            if (temp == null)
                throw new Exception("The Complex Type Is Different");
            temp.Value = Value;
        }
    }
}
