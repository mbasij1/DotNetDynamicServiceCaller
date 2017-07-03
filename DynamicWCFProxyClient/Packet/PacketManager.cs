using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DynamicWCFProxyClient.Packet
{
    public class PacketManager
    {
        public HTTPResponse SoapWebSend(Uri EndPoint, string Data, string Action)
        {
            var headers = new Dictionary<string, string>();
            headers["SOAPAction"] = "\"" +  Action + "\"";
            headers["Content-Type"] = "text/xml; charset=utf-8";
            headers["Expect"] = "100-continue";
            headers["Accept-Encoding"] = "gzip";
            return WebSend(EndPoint, MethodType.POST, Data, headers);
        }

        public HTTPResponse WebSend(Uri EndPoint, MethodType methodType, string Data, Dictionary<string, string> Headers = null)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"{methodType.ToString()} {EndPoint.AbsoluteUri} HTTP/1.1");          
            sb.AppendLine("User-Agent: DynamicWCFProxy");
            if(Headers != null)
            {
                foreach (var header in Headers)
                {
                    sb.AppendLine($"{header.Key}: {header.Value}");
                }
            }
            
            sb.AppendLine("Host: " + EndPoint.Host);
            sb.AppendLine("Content-Length:" + Encoding.UTF8.GetByteCount(Data));
            sb.Append("\n");
            if (!string.IsNullOrEmpty(Data))
            {
                sb.Append(Data);
            }
            var byteresult = Call(EndPoint, sb.ToString());
            List<byte> Header = new List<byte>();
            List<byte> Body = new List<byte>();
            bool IsBody = false;
            byte @break = 0;
            foreach (var item in byteresult)
            {
                if (item == '\n')
                    @break++;
                else if (item != 13)
                    @break = 0;

                if(@break == 2)
                {
                    IsBody = true;
                    continue;
                }

                if (IsBody)
                    Body.Add(item);
                else
                    Header.Add(item);
            }

            var callresult = new StringReader(Encoding.ASCII.GetString(Header.ToArray()));
            HTTPResponse response = new HTTPResponse();
            StringBuilder result = new StringBuilder();
            
            do
            {
                string line = callresult.ReadLine();
                // TODO: Check Headers!
                if (string.IsNullOrEmpty(line))
                    break;

                if (line.Contains("HTTP"))
                {
                    var sections = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    response.ResponseCode = Convert.ToInt32(sections[1].Trim());
                    response.ResponseDetail = sections[2].Trim();
                }
                else
                {
                    var sections = line.Split(new char[] { ':' }, 2);
                    response.Headers[sections[0].Trim()] = sections[1].Trim();
                }
            } while (true);

            if (response.Headers.Keys.Any(a => a == "Vary") && response.Headers["Vary"] == "Accept-Encoding")
                response.Body = Encoding.UTF8.GetString(Decompress(Body.ToArray()));
            else
                response.Body = Encoding.UTF8.GetString(Body.ToArray());
            return response;
        }

        public byte[] Call(Uri EndPoint, string Data)
        {
            byte[] bytes = new byte[1024];

            // Connect to a remote device.  
            try
            {
                // Establish the remote endpoint for the socket.  
                IPHostEntry ipHostInfo = Dns.GetHostEntry(EndPoint.Host);
                IPAddress ipAddress = ipHostInfo.AddressList[0];
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, EndPoint.Port);

                // Create a TCP/IP  socket.  
                Socket sender = new Socket(remoteEP.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

                // Connect the socket to the remote endpoint. Catch any errors.
                sender.Connect(remoteEP);

                // Encode the data string into a byte array.  
                byte[] msg = Encoding.UTF8.GetBytes(Data);

                // Send the data through the socket.  
                int bytesSent = sender.Send(msg);
                sender.Shutdown(SocketShutdown.Send);
                //StringBuilder sb = new StringBuilder();
                List<byte> result = new List<byte>();
                // Receive the response from the remote device.  
                while (true)
                {
                    bytes = new byte[1024];
                    int bytesRec = sender.Receive(bytes);
                    for (int i = 0; i < bytesRec; i++)
                        result.Add(bytes[i]);
                    
                    //sb.Append(Encoding.UTF8.GetString(bytes,0, bytesRec));
                    if (bytesRec == 0 && sender.Available == 0)
                        break;
                }

                // Release the socket.  
                sender.Shutdown(SocketShutdown.Both);
                sender.Close();
                return result.ToArray();
            }
            catch (Exception e)
            {
                throw new Exception("Can't Call The EndPoint, See Internal Error", e);
            }
        }

        private byte[] Decompress(byte[] gzip)
        {
            // Create a GZIP stream with decompression mode.
            // ... Then create a buffer and write into while reading from the GZIP stream.
            using (GZipStream stream = new GZipStream(new MemoryStream(gzip),
                CompressionMode.Decompress))
            {
                const int size = 4096;
                byte[] buffer = new byte[size];
                using (MemoryStream memory = new MemoryStream())
                {
                    int count = 0;
                    do
                    {
                        count = stream.Read(buffer, 0, size);
                        if (count > 0)
                        {
                            memory.Write(buffer, 0, count);
                        }
                    }
                    while (count > 0);
                    return memory.ToArray();
                }
            }
        }
    }

    public enum MethodType
    {
        GET = 0,
        POST = 1
    }
}
