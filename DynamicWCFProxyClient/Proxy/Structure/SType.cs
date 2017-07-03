using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicWCFProxyClient.Proxy.Structure
{
    public class SType
    {
        public string Name { get; set; }

        public bool Nullable { get; set; }

        public List<STypeSequence> sequence { get; set; }
        public string NameSpace { get; internal set; }

        public SType()
        {
            sequence = new List<STypeSequence>();
        }

        public Variable CreateComplexInstance()
        {
            if (sequence.Count == 0)
                throw new Exception("This Is Not Complex");

            Variable instance = new Variable();

            List<Variable> temp =  new List<Variable>();
            foreach (var item in sequence)
            {
                temp.Add(new Variable() { Name = item.Name, Value = null, NameSpace = item.NameSpace });
            }
            instance.Value = temp;

            return instance;
        }

        public STypeSequence GetPropery(string PropertyName)
        {
            return sequence.SingleOrDefault(a => a.Name == PropertyName);
        }
    }
}
