using System;
using System.Windows.Forms;
using System.Threading;
using System.Net;
using System.Net.Sockets;

namespace StalkerOnlineQuesterEditor.IOClasses
{
    public class TCPListener
    {
        Int32 port = 666;
        TcpListener server = null;
        TcpClient client = null;
        IPAddress localAddr = IPAddress.Parse("127.0.0.1");
        MainForm parent;

        public TCPListener(MainForm parent)
        {
            this.parent = parent;
        }

        public void start()
        {
            Thread newThread = new Thread(startListen);
            newThread.Start();
        }

        void startListen()
        {
            server = new TcpListener(localAddr, port);
            server.Start();

            Byte[] bytes = new Byte[256];
            String data = null;

            // Enter the listening loop.
            while (true)
            {
                Console.Write("Waiting for a connection... ");
                try
                {
                    client = server.AcceptTcpClient();
                }
                catch
                {
                    break;
                }
                Console.WriteLine("Connected!");

                data = null;

                NetworkStream stream = client.GetStream();

                int i;

                try
                {
                    while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                    {

                        data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                        Console.WriteLine("Received: {0}", data);
                        if (data.Contains("openNPC:"))
                        {
                            this.parent.openNPC(data.Replace("openNPC:", "").Trim());
                        }
                        data = data.ToUpper();

                        byte[] msg = System.Text.Encoding.ASCII.GetBytes(data);

                        stream.Write(msg, 0, msg.Length);
                        Console.WriteLine("Sent: {0}", data);
                    }
                }
                catch
                {
                   
                }
                server.Stop();
                client.Close();
                onConnectedStop();
                break;
            }
        }

        public void stop()
        {
            server.Stop();
            if (client != null)
                client.Close();
        }

        void onConnectedStop()
        {
            start();
        }
    }
}
