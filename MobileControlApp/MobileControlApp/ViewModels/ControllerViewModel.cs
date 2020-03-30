using System;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;


namespace MobileControlApp.ViewModels
{
    public class ControllerViewModel : BaseViewModel
    {
        /* Code for adding a web socket using the included program.
        private static readonly IPAddress ipAddr = IPAddress.Parse("192.168.0.17");
        private static readonly IPEndPoint localEndPoint = new IPEndPoint(ipAddr, 2345);
        private static readonly Socket sender = new Socket(ipAddr.AddressFamily,
                   SocketType.Stream, ProtocolType.Tcp);
                   */
        public ControllerViewModel()
        {
            Title = "About";
        }
    }
}