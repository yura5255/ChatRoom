// See https://aka.ms/new-console-template for more information
using System.Net;
using System.Net.Sockets;
using System.Text;

ChatServer server = new ChatServer();
server.Start();
class ChatServer
{
    const short port = 4040;
    const string address = "127.0.0.1";
    TcpListener listener = null;
    public ChatServer()
    {
        listener = new TcpListener(IPAddress.Parse(address), port);
    }
    public void Start()
    {
        listener.Start();
        Console.WriteLine("Waiting for connection...");
        TcpClient client = listener.AcceptTcpClient();
        Console.WriteLine("Connected!");
        NetworkStream ns = client.GetStream();
        StreamReader sr = new StreamReader(ns);
        StreamWriter sw = new StreamWriter(ns);
        while (true)
        {
            string message = sr.ReadLine();
            Console.WriteLine($"{message} at {DateTime.Now.ToShortTimeString()}" + $" from {client.Client.LocalEndPoint}");
            sw.WriteLine("Thanks!");
            sw.Flush();
        }

    }
}
