// Разработка сетевого приложения на C# (семинары)
// Урок 7. Библиотеки: Nuget и разработка собственных библиотек
// Доработайте чат, заменив UDP-сокеты на NetMQ. Для этого напишите новую библиотеку, 
// где в которой вы имплементируется IMessageSource и IMessageSourceClient с применением указанной библиотеки.

using NetMQ;
using NetMQ.Sockets;

public interface IMessageSource
{
    void SendMessage(string message);
    string ReceiveMessage();
}

public interface IMessageSourceClient
{
    void Connect();
    void SendMessage(string message);
    string ReceiveMessage();
    void Disconnect();
}

public class NetMQMessageSource : IMessageSource, IMessageSourceClient
{
    private readonly string _ipAddress;
    private readonly int _port;
    private PairSocket _socket;

    public NetMQMessageSource(string ipAddress, int port)
    {
        _ipAddress = ipAddress;
        _port = port;
    }

    public void SendMessage(string message)
    {
        _socket.SendFrame(message);
    }

    public string ReceiveMessage()
    {
        return _socket.ReceiveFrameString();
    }

    public void Connect()
    {
        _socket = new PairSocket();
        _socket.Connect($"tcp://{_ipAddress}:{_port}");
    }

    public void Disconnect()
    {
        _socket.Dispose();
    }
}

// В данном коде созданы интерфейсы IMessageSource и IMessageSourceClient, которые определяют методы для отправки и получения сообщений. 
// Затем создан класс NetMQMessageSource, который реализует данные интерфейсы и использует библиотеку NetMQ для работы с сокетами. 
// В конструкторе класса принимаются IP-адрес и порт для подключения к сокету. Методы Connect, SendMessage, ReceiveMessage и Disconnect 
// реализуют логику работы с сокетом.