using System;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Windows.Forms;

namespace GameCaro
{
    class NetworkSocket
    {
        #region Server
        public Socket Server;
        public event EventHandler Connected;

        public bool CreateServer()
        {
            try
            {
                IPEndPoint IPbind = new IPEndPoint(IPAddress.Any, 8080);
                Server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                Server.Bind(IPbind);
                Server.Listen(10);
                new Thread(() =>
                {
                    client = Server.Accept();
                    Connected.Invoke(this, new EventArgs());
                })
                { IsBackground = true }.Start();
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region Client
        Socket client;

        public bool ConnectServerr(string ip)
        {
            try
            {
                IPEndPoint iPserver = new IPEndPoint(IPAddress.Parse(ip), 8080);
                client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                client.Connect(iPserver);
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region Both
        private int buffer = 1024;

        public Socket Client { get => client; set => client = value; }

        public byte[] Serialize(object obj)
        {
            MemoryStream ms = new MemoryStream();
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(ms, obj);
            return ms.ToArray();
        }

        public object Deserialize(byte[] data)
        {
            MemoryStream ms = new MemoryStream(data);
            BinaryFormatter bf = new BinaryFormatter();
            return bf.Deserialize(ms);
        }
        public bool Send(object obj)
        {
            byte[] data = Serialize(obj);
            return SendData(client, data);
        }

        public object Receive()
        {
            byte[] reData = new byte[buffer];
            bool ReceiveSuccess = ReceiveData(client, reData);
            return Deserialize(reData);
        }
        private bool SendData(Socket target, byte[] data)
        {
            if (target != null)
            {
                try
                {
                    return target.Send(data) == 1 ? true : false;
                }
                catch
                {
                    return false;
                }
            }
            else
            {
                MessageBox.Show("Chưa có kết nối ");
                return false;
            }
        }

        private bool ReceiveData(Socket target, byte[] data)
        {
            try
            {
                if (target != null)
                    return target.Receive(data) == 1 ? true : false;
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }
        public string GetIPv4(NetworkInterfaceType _type)
        {
            string ouput = "";
            foreach (var item in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (item.NetworkInterfaceType == _type && item.OperationalStatus == OperationalStatus.Up)
                {
                    foreach (var ip in item.GetIPProperties().UnicastAddresses)
                    {
                        if (ip.Address.AddressFamily == AddressFamily.InterNetwork)
                        {
                            ouput = ip.Address.ToString();
                        }
                    }
                }
            }

            return ouput;
        }
        #endregion

    }
}
