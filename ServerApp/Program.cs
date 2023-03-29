// See https://aka.ms/new-console-template for more information
using System.Net;
using System.Net.Sockets;
using System.Text;

ChatServer server = new ChatServer();
server.Start();
class ChatServer
{
    const short port = 4040;
    const string JOIN_CMD = "$<join>";
    UdpClient server = new UdpClient(port);
    HashSet<IPEndPoint> members = new HashSet<IPEndPoint>();
    IPEndPoint clientEndPoint = null;
    void AddMember(IPEndPoint member)
    {
        members.Add(member);
        Console.WriteLine("Member was added!");

    }
    private void SendToAll(byte[] data)
    {
        foreach (IPEndPoint member in members)
        {
            server.SendAsync(data, data.Length, member);
        }
    }
    public void Start()
    {
        while (true)
        {
            byte[] data = server.Receive(ref clientEndPoint);
            string message = Encoding.Unicode.GetString(data);
            Console.WriteLine($"{message} at {DateTime.Now.ToShortTimeString()}" + $" from {clientEndPoint}");
            if (message == JOIN_CMD)
            {
                AddMember(clientEndPoint);
            }
            else
            {
                SendToAll(data);
            }
        }

    }
}
