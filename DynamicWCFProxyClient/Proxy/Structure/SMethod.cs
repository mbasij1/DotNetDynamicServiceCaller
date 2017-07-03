using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicWCFProxyClient.Proxy.Structure
{
    public class SMethod
    {
        public string Action { get; set; }

        public string ActionName { get; set; }

        internal List<SMethodArgumant> Argumants { get; set; }

        /// <summary>
        /// Type Of OutPut Method
        /// </summary>
        public SType Type { get; set; }

        public SMethod()
        {
            Argumants = new List<SMethodArgumant>();
        }

        /// <summary>
        /// Create Requst For Method
        /// </summary>
        /// <returns></returns>
        public Request CreateRequest()
        {
            Request request = new Request();
            request.Action = Action;
            request.ActionName = ActionName;
            return request;
        }

        /// <summary>
        /// Get All Argumants Of Method
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SMethodArgumant> GetArgumants()
        {
            return Argumants;
        }

        /// <summary>
        /// Get Special Argumant
        /// </summary>
        /// <param name="ArgumantName"></param>
        /// <returns>Argumant</returns>
        public SMethodArgumant GetArgumant(string ArgumantName)
        {
            return Argumants.SingleOrDefault(a => a.Name == ArgumantName);
        }
    }
}
