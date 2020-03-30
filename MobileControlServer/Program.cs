using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

/* This software uses the program at http://www.nirsoft.net/utils/nircmd.html in order to
 * create the control functionllity of the server computer through the command prompt.
 * 
 * 
 * 
 * 
 * 
 * 
 */

namespace MobileControlServer
{
    class Program
    {
        private static readonly IPAddress ipAddr = IPAddress.Parse("192.168.0.17");
        private static readonly IPEndPoint localEndPoint = new IPEndPoint(ipAddr, 2345);
        private static readonly Socket listener = new Socket(ipAddr.AddressFamily,
                     SocketType.Stream, ProtocolType.Tcp);
        private static Socket clientSocket;
        static void Main(string[] args)
        {
            StartServer();
        }

        static void StartServer()
        {
            listener.Bind(localEndPoint);
            listener.Listen(10);
            Console.WriteLine("This is the Mobile Control Server!\n");
            byte[] bytes = new byte[256];
            string msg = null;
            while (true)
            {
                clientSocket = listener.Accept();
                Console.WriteLine(DateTime.Now.ToString("h:mm:ss tt") + ": Client connected from: " + clientSocket.RemoteEndPoint); // Lists the IP and port of the connecting client
                while (true)
                {
                    while (true)
                    {
                        if (!clientSocket.Connected)
                        {
                            break; // If client is not connected exit out of the first while loop
                        }
                        try
                        {
                            int numByte = clientSocket.Receive(bytes);// Wait for the client to send a message
                            msg = Encoding.ASCII.GetString(bytes, 0, numByte);
                        }
                        catch (System.Net.Sockets.SocketException)
                        {
                        }// Catch the client forcibly disconecting 
                        break;
                    }
                    if (clientSocket.Connected)
                    {
                        if (msg == "") // Catch the client forcibly disconecting
                        {
                            clientSocket.Disconnect(true);
                            continue;
                        }
                        Console.WriteLine(DateTime.Now.ToString("h:mm:ss tt") + ": Received: " + msg); // Prints the message with a timestamp
                        Start(msg);
                    }
                    else
                    {
                        Console.WriteLine(DateTime.Now.ToString("h:mm:ss tt") + ": Client disconnected from: " + clientSocket.RemoteEndPoint);
                        break; // If client is not connected exit out of the final while loop
                    }
                }
            }
        }
        private async void Wait(int time)
        {
            await Task.Delay(time * 1000);
        }
        public static void Start(string readData)
        {
            switch (readData)
            {
                case "VolumeUp":
                    VolumeUp();
                    break;
                case "VolumeDown":
                    VolumeDown();
                    break;
                case "Sleep":
                    Sleep();
                    break;
                case "Off":
                    Off();
                    break;
                case "Disconnect":
                    clientSocket.Disconnect(true);
                    break;
                default:
                    SleepTime(readData);
                    break;

            }
        }
        private static void VolumeUp()
        {
            System.Diagnostics.Process.Start("CMD.exe", "/C nircmd.exe changesysvolume 4000");
        }
        private static void VolumeDown()
        {
            System.Diagnostics.Process.Start("CMD.exe", "/C nircmd.exe changesysvolume -4000");
        }
        private static void Sleep()
        {
            System.Diagnostics.Process.Start("CMD.exe", "/C shutdown.exe /h /f");
        }
        private static void Off()
        {
            System.Diagnostics.Process.Start("CMD.exe", "/C nircmd.exe monitor off");
        }
        private static void SleepTime(string readData)
        {
            int time = Int32.Parse(readData);
            time *= 60000; // Converts the time into minutes
            Thread.Sleep(time);
            System.Diagnostics.Process.Start("CMD.exe", "/C shutdown.exe /h /f");
        }

    }
}