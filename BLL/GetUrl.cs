using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.IO;

namespace FxProductMonitor.BLL
{
    public class EasyApi
    {
        public string Perform(string url, string cookie, string host, string requestUrl, string referer)
        {
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            IPAddress hostIp = Dns.GetHostByName(host).AddressList[0];
            socket.Connect(hostIp, 80);

            StringBuilder sb = new StringBuilder();
            sb.Append("GET ");
            sb.Append(requestUrl);
            sb.Append(" HTTP/1.0\r\nHost: ");
            sb.Append(host);
            sb.Append("\r\nConnection: keep-alive\r\n");
            sb.Append("Accept:text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8\r\n");
            sb.Append("User-Agent:Mozilla/5.0 (Windows NT 5.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/31.0.1650.63 Safari/537.36\r\n");
            sb.Append("Cookie:");
            sb.Append(cookie);
            sb.Append("\r\nReferer:");
            sb.Append(referer);
            sb.Append("\r\n\r\n");

            string requestHeader = sb.ToString();
            byte[] bRequestHeader = Encoding.UTF8.GetBytes(requestHeader);
            socket.Send(bRequestHeader, bRequestHeader.Length, 0);

            string recvBuf = "";
            byte[] recvBytes = new byte[1024];
            int bytesRead = 0;
            bytesRead = socket.Receive(recvBytes, recvBytes.Length, 0);
            MemoryStream ms = new MemoryStream();
            while (bytesRead > 0)
            {
                ms.Write(recvBytes, 0, bytesRead);
                bytesRead = socket.Receive(recvBytes, recvBytes.Length, 0);
            }
            recvBuf = Encoding.UTF8.GetString(ms.ToArray());
            socket.Close();
            return recvBuf;
        }
        [DllImport("ws2_32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern Int32 WSAStartup(Int16 wVersionRequested, out WSAData wsaData);

        [DllImport("ws2_32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern IntPtr socket(int af, int socket_type, int protocol);
        //IPPROTO_TCP             6
        //SOCK_STREAM     1
        //AF_INET         2 

        [DllImport("Ws2_32.dll")]
        public static extern int connect(IntPtr s, out sockaddr_in addr, int addrsize);
        [DllImport("Ws2_32.dll")]
        public static extern int recv(IntPtr s, out string buf, int len, int flags);
        [DllImport("Ws2_32.dll")]
        public static extern int send(IntPtr s, string buf, int len, int flags);
        [Obsolete]
        [DllImport("Ws2_32.dll", CharSet = CharSet.Ansi)]
        public static extern uint inet_addr(string cp);
        [DllImport("Ws2_32.dll")]
        public static extern ushort htons(ushort hostshort);
        [DllImport("ws2_32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern int closesocket(IntPtr s);
        [DllImport("ws2_32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern Int32 WSACleanup();

        public string CPerform(string url, string cookie, string host, string requestUrl, string referer)
        {

            StringBuilder sb = new StringBuilder();
            sb.Append("GET ");
            sb.Append(requestUrl);
            sb.Append(" HTTP/1.0\r\nHost: ");
            sb.Append(host);
            sb.Append("\r\nConnection: keep-alive\r\n");
            sb.Append("Accept:text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8\r\n");
            sb.Append("User-Agent:Mozilla/5.0 (Windows NT 5.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/31.0.1650.63 Safari/537.36\r\n");
            sb.Append("Cookie:");
            sb.Append(cookie);
            sb.Append("\r\nReferer:");
            sb.Append(referer);
            sb.Append("\r\n\r\n");

            UInt16 nVersion = ((UInt16)(((byte)(2)) | ((UInt16)((byte)(2))) << 8));
            WSAData wsadata;
            if (WSAStartup((short)2, out wsadata) != 0)
            {
                return null;
            }
            sockaddr_in addr = new sockaddr_in();
            addr.sin_addr.S_addr = inet_addr(Dns.GetHostByName(host).AddressList[0].Address.ToString());
            addr.sin_family = 2;
            addr.sin_port = htons(80);
            IntPtr sock = socket(2, 1, 6);


            connect(sock, out addr, Marshal.SizeOf(addr));
            int ret = send(sock, sb.ToString(), sb.ToString().Length, 0);
            string tempBuf = "";
            string recvBuf = "";

            while (recv(sock, out tempBuf, 1024, 0) > 0)
            {
                recvBuf += tempBuf;
            }


            closesocket(sock);
            WSACleanup();
            return tempBuf;


        }
    }

    public class WSAData
    {
        public UInt16 wVersion;
        public UInt16 wHighVersion;
        //#ifdef _WIN64
        //        UInt16          iMaxSockets;
        //        UInt16          iMaxUdpDg;
        //        string              lpVendorInfo;
        //        char                    szDescription[WSADESCRIPTION_LEN+1];
        //        char                    szSystemStatus[WSASYS_STATUS_LEN+1];
        //#else
        public string szDescription;
        public string szSystemStatus;
        public UInt16 iMaxSockets;
        public UInt16 iMaxUdpDg;
        public string lpVendorInfo;
        //#endif
    }

    public class sockaddr_in
    {
        public UInt16 sin_family;
        public UInt16 sin_port;
        public in_addr sin_addr;
        public string sin_zero;
    }
    public struct in_addr
    {
        public ulong S_addr;
    }
}
