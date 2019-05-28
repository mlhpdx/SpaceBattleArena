using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SpaceBattleArena
{
    public class MwnpListener
    {
        static string name = "Listener Thead";
        private NetworkStream inStream;
        private Socket sock;
        private Client client;
        private Thread thread;
        private volatile bool running;

        private bool LOGGING = true;
        private StreamWriter logStream;

        public MwnpListener(Client client, Socket sock)
        {
            this.client = client;
            this.sock = sock;
            inStream = new NetworkStream(sock);
            running = true;

            logStream = new StreamWriter("listener.log");
            //logStream = System.out;
        }

        public Task start()
        {
            return Task.Run(run);
        }

        private void run()
        {
            while (running)
            {
                try
                {
                    int msgSize = getMessageLength();
                    if (msgSize == -1)
                        return;

                    MwnpMessage msg = getMessage(msgSize);

                    if (msg == null)
                        return;

                    client.parseMessage(msg);

                    if (LOGGING)
                        printMessage(msg);

                    Thread.Sleep(1);
                }
                catch (IOException ex)
                {
                    Console.Error.WriteLine("Server read error...");
                    Console.Error.WriteLine(ex.ToString());
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(e.ToString());
                }
            }
        }

        public void end()
        {
            client.logMessage("Listener ending...");
            if (sock?.Connected == true)
            {
                sock?.Close();
                sock = null;
            }

            logStream?.Close();
            logStream = null;

            running = false;
        }

        private int getMessageLength()
        {
            StringBuilder builder = new StringBuilder();
            int currByte = -1;
            try
            {
                while (sock.Connected)
                {
                    currByte = inStream.ReadByte();

                    if (currByte != -1 && currByte != '[')
                        builder.Append((char)currByte);
                    else
                        break;
                }
            }
            catch (IOException ex)
            {
                // disconnected while waiting for message... carry on
            }

            if (!sock.Connected || currByte == -1)
                return -1;
            return (int.Parse(builder.ToString()));
        }

        private MwnpMessage getMessage(int length)
        {
            StringBuilder builder = new StringBuilder("[");
            for (int i = 0; i < length - 1; i++)
            {
                int currByte = inStream.ReadByte();
                if (currByte == -1)
                    return null;

                builder.Append((char)currByte);
            }
            string message = builder.ToString();

            return MwnpMessage.parseMessage(message);
        }

        private void printMessage(MwnpMessage message)
        {
            (logStream ?? Console.Out).Write("Message received from {0} intended for {1} - \r\n", message.getSenderId(), message.getReceiverId());
            (logStream ?? Console.Out).WriteLine(message.ToString());
        }
    }
}