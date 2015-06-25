using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace UDP_Client
{
    class Program
    {
        static void Main(string[] args)
        {
            string data = "";
            byte[] sendBytes = new Byte[1024];
            byte[] rcvPacket = new Byte[1024];
            UdpClient client = new UdpClient();
            IPAddress address = IPAddress.Parse(IPAddress.Broadcast.ToString());
            client.Connect(address, 8008);
            IPEndPoint remoteIPEndPoint = new IPEndPoint(IPAddress.Any, 0);

            Console.WriteLine("Client is Started");
            Console.WriteLine("Type your message");

            while (data != "q")
            {
                data = Console.ReadLine();
                sendBytes = Encoding.ASCII.GetBytes(DateTime.Now.ToString() + " " + data);
                client.Send(sendBytes, sendBytes.GetLength(0));
                rcvPacket = client.Receive(ref remoteIPEndPoint);

                string rcvData = Encoding.ASCII.GetString(rcvPacket);
                Console.WriteLine("Handling client at " + remoteIPEndPoint + " - ");

                Console.WriteLine("Message Received: " + rcvPacket.ToString());
            }
            Console.WriteLine("Close Port Command Sent");  //user feedback
            Console.ReadLine();
            client.Close();  //close connection
        }
    }
}
