using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Soap;
using System.IO;
using System.Runtime.Serialization;
using DynamicWCFProxyClient.Packet;
using System.Xml.Linq;
using System.Xml.Serialization;
using DynamicWCFProxyClient.Proxy.Models;
using System.Xml;
using DynamicWCFProxyClient.Proxy.Structure;

namespace DynamicWCFProxyClient.Proxy
{
    public class ProxyClient
    {
        private Uri EndpointUri { get; set; }

        private PacketManager PacketManager = new PacketManager();

        private definitions DefinationSerivce;

        private List<SMethod> Methods { get; set; }

        private List<SType> Types { get; set; }

        public ProxyClient(Uri endpointUri)
        {
            if (endpointUri == null)
                throw new Exception("endpointUri Can't be null, You Should Define Endpoint");

            EndpointUri = endpointUri;

            ReadStructure();
        }

        private void ReadStructure()
        {
            var res = PacketManager.WebSend(EndpointUri, MethodType.GET, "");
            if (res.ResponseCode != 200)
                throw new Exception($" Endpoint Service Return Response Code {res.ResponseCode}");

            if (string.IsNullOrEmpty(res.Body))
                throw new Exception("EndPoint Don't Response");

            DefinationSerivce = LoadSingleWSDL();
            Types = GetTypes();
            Methods = GetOperations();
            //var discovery = GetDiscovery();
            //foreach (var reference in discovery.contractRef)
            //    LoadReference(reference.@ref);
            
            //foreach (var item in DefinationSerivce.types.schema.import)
            //{
            //    LoadSchema(item.XsiSchemaLocation);
            //}
        }

        private definitions LoadSingleWSDL()
        {
            var res = PacketManager.WebSend(new Uri(EndpointUri, "?singleWsdl"), MethodType.GET, "");
            if (res.ResponseCode != 200)
                throw new Exception($" Endpoint Service Return Response Code {res.ResponseCode}");

            XmlSerializer serializer = new XmlSerializer(typeof(definitions));
            return (definitions)serializer.Deserialize(new NamespaceIgnorantXmlTextReader(new MemoryStream(Encoding.UTF8.GetBytes(res.Body))));
        }

        #region Multi File WSDL Read 
        //private void LoadSchema(string xsiSchemaLocation)
        //{
        //    var res = PacketManager.WebSend(new Uri(xsiSchemaLocation), "");
        //    XmlSerializer serializer = new XmlSerializer(typeof(definitions));
        //    DefinationSerivce = (definitions)serializer.Deserialize(new NamespaceIgnorantXmlTextReader(new MemoryStream(Encoding.UTF8.GetBytes(res))));
        //}

        //private discovery GetDiscovery()
        //{
        //    var res = PacketManager.WebSend(new Uri(EndpointUri, "?disco"), MethodType.GET, "");
        //    if (res.ResponseCode != 200)
        //        throw new Exception($" Endpoint Service Return Response Code {res.ResponseCode}");
        //    XmlSerializer serializer = new XmlSerializer(typeof(discovery));
        //    return (discovery)serializer.Deserialize(new MemoryStream(Encoding.UTF8.GetBytes(res.Body)));
        //}

        //private void LoadReference(string RefAddress)
        //{
        //    var res = PacketManager.WebSend(new Uri(RefAddress), "");
        //    XmlSerializer serializer = new XmlSerializer(typeof(definitions));
        //    DefinationSerivce = (definitions)serializer.Deserialize(new NamespaceIgnorantXmlTextReader(new MemoryStream(Encoding.UTF8.GetBytes(res))));
        //}
        #endregion

        public Dictionary<string, object> Call(Request request)
        {
            var res = PacketManager.SoapWebSend(EndpointUri, request.ToString(), request.Action);
            if (res.ResponseCode != 200 && res.ResponseCode != 500)
                throw new Exception($"Endpoint Service Return Response Code {res.ResponseCode}");
            return DeserializeObject(GetMethod(request.ActionName).Type, res.Body);
        }

        public List<SMethod> GetMethods()
        {
            return Methods;
        }

        private List<SMethod> GetOperations()
        {
            var portTypeOperations = DefinationSerivce.portType.portTypeoperation;
            List<SMethod> methods = new List<SMethod>();
            foreach (var operation in portTypeOperations)
                methods.Add(GetOperation(operation.name));
            
            return methods;
        }

        public SMethod GetMethod(string ActionName)
        {
            var method = Methods.SingleOrDefault(a => a.ActionName == ActionName);
            if (method == null)
                throw new Exception("Method Not Exist In This Service");

            return method;
        }

        private SMethod GetOperation(string ActionName)
        {
            var Operation = DefinationSerivce.portType.portTypeoperation.SingleOrDefault(a => a.name == ActionName);
            if (Operation == null)
                throw new Exception("Method Not Exist In This Service");

            SMethod method = new SMethod();
            method.Action = Operation.portTypeoperationinput.Action;
            method.ActionName = ActionName;
            //method.Type = 

            #region Set argumants method
            {
                var Typename = Operation.portTypeoperationinput.message.Split(':').Last();
                var messeage = DefinationSerivce.message.Single(a => a.name == Typename);
                Typename = messeage.part.element.Split(':').Last();
                var schema = DefinationSerivce.types.schema.Single(a => a.schemaelement.Any(b => b.name == Typename));
                var parameters = schema.schemaelement.Single(b => b.name == Typename);
                foreach (var param in parameters.elementcomplexType.First().elementcomplexTypesequence.elementcomplexTypesequenceelement)
                {
                    var p = GetType(param.type);
                    method.Argumants.Add(new SMethodArgumant() { Name = param.name, Type = p, NameSpace = p.NameSpace });
                }
            }
            #endregion
            #region Set output method
            {
                var Typename = Operation.portTypeoperationoutput.message.Split(':').Last();
                var messeage = DefinationSerivce.message.Single(a => a.name == Typename);
                Typename = messeage.part.element.Split(':').Last();

                var schema = DefinationSerivce.types.schema.SingleOrDefault(a => a.schemaelement.Any(b => b.name == Typename && string.IsNullOrEmpty(b.type)));
                var parameters = schema.schemaelement.Single(b => b.name == Typename).elementcomplexType.First().elementcomplexTypesequence.elementcomplexTypesequenceelement.FirstOrDefault();

                if (parameters != null)
                    method.Type = new SType() { Name = Typename, sequence = new List<STypeSequence> { new STypeSequence() { Name = parameters.name, TypeName = parameters.type, NameSpace = schema.targetNamespace } } };
                else
                    method.Type = new SType() { Name = Typename, sequence = new List<STypeSequence> { } };
                
            }
            #endregion
            
            return method;
        }

        private List<SType> GetTypes()
        {
            List<SType> types = new List<SType>();
            foreach (var schema in DefinationSerivce.types.schema)
            {
                foreach (var complexType in schema.schemacomplexType)
                {
                    if(!types.Any(a=> a.Name == complexType.name))
                        types.Add(GetTypeFromDefination(complexType.name));
                }

                foreach (var simpleType in schema.simpleType)
                {
                    if (!types.Any(a => a.Name == simpleType.name))
                        types.Add(GetTypeFromDefination(simpleType.name));
                }

                foreach (var element in schema.schemaelement)
                {
                    if (!types.Any(a => a.Name == element.name))
                        types.Add(GetTypeFromDefination(element.name));
                }
            }
            return types;
        }

        private SType GetTypeFromDefination(string Type)
        {
            SType sType = new SType();
            var type = Type.Split(':').Last();
            var schema = DefinationSerivce.types.schema.SingleOrDefault(a => a.schemacomplexType.Any(b => b.name == type));
            if (schema != null)
            {
                sType.Name = type;
                sType.sequence = new List<STypeSequence>();
                sType.NameSpace = schema.targetNamespace;
                foreach (var newparam in schema.schemacomplexType.Single(b => b.name == type).schemacomplexTypesequence.schemacomplexTypesequenceelement)
                {
                    sType.sequence.Add(new STypeSequence() { Name = newparam.name, TypeName = newparam.type.Split(':').Last(), NameSpace = schema.targetNamespace });
                }
            }
            else
            {
                sType.Name = type;
            }
            return sType;
        }

        public SType GetType(string Type)
        {
            return Types.SingleOrDefault(a => a.Name == Type.Split(':').Last());
        }

        #region Deserializing To Dictionary<string, object>
        private Dictionary<string, object> DeserializeObject(SType type, string res)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(res);
                Dictionary<string, object> obj = null;

                foreach (XmlNode node in doc.DocumentElement.ChildNodes)
                {
                    if (node.LocalName == "Body")
                    {
                        if (node.FirstChild.LocalName == "Fault")
                            obj = (Dictionary<string, object>)ConvertFaultToDictionary(node);
                        else
                            obj = (Dictionary<string, object>)ConvertToDictionary(type, node.FirstChild);
                    }
                }

                return obj;
            }
            catch (Exception ex)
            {
                throw new Exception($" Endpoint Service Response Is Not Valid, Response = {res}", ex);
            }
        }

        private object ConvertFaultToDictionary(XmlNode Root)
        {
            if(!Root.HasChildNodes)
            {
                if (!string.IsNullOrEmpty(Root.InnerText))
                    return Root.InnerText;

                if (Root.Attributes.Count > 0)
                    foreach (XmlAttribute att in Root.Attributes)
                        if (!att.Name.Contains("xmlns"))
                            return (att.Value == "true") ? true : false; ;

                return null;
            }

            if (Root.ChildNodes.Count == 1 && Root.FirstChild.LocalName.Contains("#"))
                return ConvertFaultToDictionary(Root.FirstChild);

            Dictionary<string, object> obj = new Dictionary<string, object>();
            foreach (XmlNode node in Root.ChildNodes)
                obj[node.LocalName] = ConvertFaultToDictionary(node);
            return obj;
        }

        private object ConvertToDictionary(SType type, XmlNode Root)
        {
            if (Root.HasChildNodes)
            {
                if(Root.ChildNodes.Count > 1 && Root.ChildNodes[0].LocalName == Root.ChildNodes[1].LocalName)
                {
                    List<object> list = new List<object>();

                    foreach (XmlElement node in Root.ChildNodes)
                        list.Add(ConvertToDictionary(GetType(type.GetPropery(node.LocalName).TypeName), node));

                    return list;
                }

                if(Root.ChildNodes.Count == 1 && Root.FirstChild.LocalName.Contains("#"))
                    return ConvertToDictionary(type, Root.FirstChild);

                if(Root.ChildNodes.Count == 1)
                {
                    Dictionary<string, object> obj = new Dictionary<string, object>();
                    obj[Root.ChildNodes[0].LocalName] = ConvertToDictionary(GetType(type.GetPropery(Root.ChildNodes[0].LocalName).TypeName), Root.ChildNodes[0]);
                    return obj;
                }
                {
                    Dictionary<string, object> obj = new Dictionary<string, object>();
                    foreach (XmlNode node in Root.ChildNodes)
                        obj[node.LocalName] = ConvertToDictionary(GetType(type.GetPropery(node.LocalName).TypeName), node);
                    return obj;
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(Root.InnerText))
                    switch (type.Name.ToLower())
                    {
                        case "guid":
                            return Guid.Parse(Root.InnerText);

                        case "datetime":
                            return DateTime.Parse(Root.InnerText);

                        case "long":
                            return Convert.ToInt64(Root.InnerText);

                        case "decimal":
                            return Convert.ToDecimal(Root.InnerText);

                        case "int":
                            return Convert.ToInt32(Root.InnerText);

                        case "string":
                            return Root.InnerText;

                        case "bool":
                            return Root.InnerText == "true" ? true : false;

                        default:
                            return Root.InnerText;
                    }
                

                if (Root.Attributes.Count > 0)
                    foreach (XmlAttribute att in Root.Attributes)
                        if (!att.Name.Contains("xmlns"))
                            return (att.Value == "true")?true:false;

                return null; // throw new Exception("Error In Parsing Result");
            }
            
        }
        #endregion
    }
}
