using System.Net;
using System.Net.Sockets;
using System.Text;


namespace ChatApp
{
    //TODO Join Name
    //TODO Kick
    //TODO Auslagern
    public partial class ChatForm : Form
    {
        private List<TcpClient> clients = new List<TcpClient>();
        private TcpListener? server;
        private NetworkStream stream;
        private TcpClient client;

        public ChatForm()
        {
            InitializeComponent();

            // Überprüfen, ob alle Steuerelemente sichtbar sind
            txtMessages.Visible = true;
            txtInput.Visible = true;
            btnHostServer.Visible = true;
            btnJoinChat.Visible = true;
        }

        private void BtnHostServer_Click(object sender, EventArgs e)
        {
            Thread serverThread = new Thread(StartServer);
            serverThread.IsBackground = true;
            serverThread.Start();
        }

        private void BtnJoinChat_Click(object sender, EventArgs e)
        {
            Thread clientThread = new Thread(StartClient);
            clientThread.IsBackground = true;
            clientThread.Start();
        }

        private void TxtInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendMessage(txtInput.Text);
                txtInput.Clear();
            }
        }

        private void StartServer()
        {
            const int port = 5000;
            try
            {
                server = new TcpListener(IPAddress.Any, port);
                server.Start();

                UpdateMessages("Server wurde gestartet.");
                string localIP = GetLocalIPAddress(this);
                UpdateMessages("Warte auf Verbindungen...");
                

                while (true)
                {
                    TcpClient client = server.AcceptTcpClient();
                    var clientIP = client.Client.RemoteEndPoint;
                    client.Client.Shutdown(new SocketShutdown());
                    clients.Add(client);
                    UpdateMessages($"Neuer Client verbunden. ({clientIP})");
                    BroadcastMessage("Ein neuer Benutzer hat den Chat betreten", null);

                    Thread receiveThread = new Thread(() => ReceiveMessages(client));
                    receiveThread.IsBackground = true;
                    receiveThread.Start();
                }
            }
            catch (Exception ex)
            {
                UpdateMessages($"Fehler im Server: {ex.Message}");
            }
        }

        [STAThread]
        static string GetLocalIPAddress(ChatForm chat)
        {
            try
            {
                foreach (var ip in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
                {
                    if (ip.AddressFamily == AddressFamily.InterNetwork)
                    {
                        string message = $"Deine IP ist: " + ip.ToString();

                        chat.UpdateMessages(message);
                        return ip.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                string message = "Das hat leider nicht funktioniert. Gebe 'ipconfig' in der Console ein um deine Ip herauszufinden. ERROR:" + ex;
                chat.UpdateMessages(message);
            }
            return "How did u get here?";
        }

        private void StartClient()
        {
            const int port = 5000;
            string serverIP = Prompt.ShowDialog("Geben Sie die Server-IP-Adresse ein:", "Chat Beitreten").Trim();

            try
            {
                client = new TcpClient(serverIP, port);
                UpdateMessages("Mit dem Server verbunden.");

                stream = client.GetStream();
                Thread receiveThread = new Thread(() => ReceiveMessages(client));
                receiveThread.IsBackground = true;
                receiveThread.Start();
            }
            catch (Exception ex)
            {
                UpdateMessages($"Fehler beim Client: {ex.Message}");
            }
        }

        private void ReceiveMessages(TcpClient client)
        {
            NetworkStream clientStream = client.GetStream();
            byte[] buffer = new byte[1024];
            while (true)
            {
                try
                {
                    int bytesRead = clientStream.Read(buffer, 0, buffer.Length);
                    if (bytesRead == 0) break;

                    string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    BroadcastMessage(message, client);
                    UpdateMessages(message);

                    if (message.Trim().ToUpper() == "EXIT") break;
                }
                catch (Exception ex)
                {
                    UpdateMessages($"Empfangsfehler: {ex.Message}");
                    break;
                }
            }
            clients.Remove(client);
            client.Close();
        }

        private void BroadcastMessage(string message, TcpClient sender)
        {
            byte[] data = Encoding.UTF8.GetBytes(message);
            foreach (var client in clients)
            {
                if (client != sender)
                {
                    NetworkStream clientStream = client.GetStream();
                    clientStream.Write(data, 0, data.Length);
                }
            }
        }

        private void SendMessage(string message)
        {
            try
            {
                if (!string.IsNullOrEmpty(message))
                {
                    byte[] data = Encoding.UTF8.GetBytes($"[{Environment.UserName.Split(".")[0]}]: {message}");

                    if (server != null)
                    {
                        BroadcastMessage($"[{Environment.UserName.Split(".")[0]}]: {message}", null);
                    }
                    else if (stream != null)
                    {
                        stream.Write(data, 0, data.Length);
                    }

                    UpdateMessages($"[Du]: {message}");
                }
            }
            catch (Exception ex)
            {
                UpdateMessages($"Sendefehler: {ex.Message}");
            }
        }

        private void UpdateMessages(string message)
        {
            if (txtMessages.InvokeRequired)
            {
                txtMessages.Invoke(new Action(() => txtMessages.AppendText(message + Environment.NewLine)));
            }
            else
            {
                txtMessages.AppendText(message + Environment.NewLine);
            }
        }
    }

    public static class Prompt
    {
        public static string ShowDialog(string text, string caption)
        {
            Form prompt = new Form()
            {
                Width = 400,
                Height = 150,
                Text = caption
            };
            Label textLabel = new Label() { Left = 10, Top = 20, Text = text, Width = 350 };
            TextBox textBox = new TextBox() { Left = 10, Top = 50, Width = 350 };
            Button confirmation = new Button() { Text = "OK", Left = 280, Width = 80, Top = 80 };
            confirmation.Click += (sender, e) => { prompt.Close(); };
            prompt.Controls.Add(textLabel);
            prompt.Controls.Add(textBox);
            prompt.Controls.Add(confirmation);
            prompt.AcceptButton = confirmation;
            prompt.ShowDialog();
            return textBox.Text;
        }
    }
}