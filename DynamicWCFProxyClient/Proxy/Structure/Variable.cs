using System;
using System.Collections.Generic;
using System.Text;

namespace DynamicWCFProxyClient.Proxy.Structure
{
    public class Variable
    {
        public string Name { get; set; }

        public string NameSpace { get; set; }

        public object Value { get; set; }

        private static Dictionary<string, char> UsedLetters = new Dictionary<string, char>() {
            {"\"http://schemas.xmlsoap.org/soap/envelope/\"", 's' },
            {"\"http://www.w3.org/2001/XMLSchema-instance\"", 'i'} };

        private static Random rand = new Random();

        public override string ToString()
        {
            return ToString("");
        }

        public string ToString(string prefix)
        {
            string Prefix = "";
            if (!string.IsNullOrEmpty(prefix))
                Prefix = prefix + ":";

            if (Value == null)
                return $"<{Prefix}{Name} i:nil=\"true\"/>";
            else
            {
                StringBuilder sb = new StringBuilder();
                if (Value is List<Variable>)
                {
                    foreach (var variable in Value as List<Variable>)
                        sb.Append(variable.ToString(prefix));
                    return sb.ToString();
                }

                string namespacestring = "";
                string Addon = "";
                if (NameSpace != null)
                {
                    if (UsedLetters.ContainsKey(NameSpace))
                        namespacestring = UsedLetters[NameSpace].ToString();
                    else
                    {
                        char key = 'i';
                        do
                        {
                            key = (char)Convert.ToByte(rand.Next(97, 122));
                        } while (UsedLetters.ContainsValue(key));
                        UsedLetters[NameSpace] = key;
                        namespacestring = key.ToString();
                        Addon = $"xmlns:{key}=\"{NameSpace}\"";
                    }
                }
                if(Addon != "")
                    sb.Append($"<{Prefix}{Name}  {Addon}>");
                else
                    sb.Append($"<{Prefix}{Name}>");
                if (Value is Variable)
                    sb.Append(((Variable)Value).ToString((string.IsNullOrEmpty(namespacestring)) ? Prefix : namespacestring));
                else if (Value is DateTime)
                    sb.Append(((DateTime)Value).ToString("yyyy-MM-ddTHH:mm:ss"));
                else if (Value is bool)
                    sb.Append(((bool)Value) ? "true" : "false");
                else
                    sb.Append(Value.ToString());

                if(!string.IsNullOrEmpty(Addon))
                    UsedLetters.Remove(NameSpace);
                sb.Append($"</{Prefix}{Name}>");
                return sb.ToString();
            }
        }
    }
}