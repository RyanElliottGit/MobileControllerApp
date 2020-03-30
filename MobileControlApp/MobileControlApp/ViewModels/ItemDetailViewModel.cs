using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using MobileControlApp.Models;

namespace MobileControlApp.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        static Item item;
        public Item Item { get; set; }
        //private static IPAddress ip;
        //private static IPEndPoint localEndPoint;
        //private static Socket sender;
        public ItemDetailViewModel(Item itemPeram)
        {
            item = itemPeram;
            Title = item?.Text;
            Item = item;
            /*  Code for adding a web socket to the other program.
            ip = IPAddress.Parse(item?.IpAdd);
            localEndPoint = new IPEndPoint(ip, 2345);
            sender = new Socket(ip.AddressFamily,
                   SocketType.Stream, ProtocolType.Tcp);
                   */

        }
    }
}
