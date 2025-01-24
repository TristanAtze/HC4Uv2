
namespace ChatApp
{
    partial class ChatForm : Form
    {
        /// <summary>
        /// Erforderliche Designer-Variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn die verwalteten Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designer-Unterstützung – 
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        [STAThread]
        private void InitializeComponent()
        {
            txtMessages = new TextBox();
            txtInput = new TextBox();
            btnHostServer = new Button();
            btnJoinChat = new Button();
            SuspendLayout();
            // 
            // txtMessages
            // 
            txtMessages.Dock = DockStyle.Top;
            txtMessages.Location = new Point(0, 0);
            txtMessages.Multiline = true;
            txtMessages.Name = "txtMessages";
            txtMessages.ReadOnly = true;
            txtMessages.ScrollBars = ScrollBars.Vertical;
            txtMessages.Size = new Size(600, 266);
            txtMessages.TabIndex = 0;
            // 
            // txtInput
            // 
            txtInput.Dock = DockStyle.Bottom;
            txtInput.Location = new Point(0, 377);
            txtInput.Name = "txtInput";
            txtInput.Size = new Size(600, 23);
            txtInput.TabIndex = 1;
            txtInput.KeyDown += TxtInput_KeyDown;
            // 
            // btnHostServer
            // 
            btnHostServer.Dock = DockStyle.Left;
            btnHostServer.Location = new Point(0, 266);
            btnHostServer.Name = "btnHostServer";
            btnHostServer.Size = new Size(150, 111);
            btnHostServer.TabIndex = 2;
            btnHostServer.Text = "Server Hosten";
            btnHostServer.UseVisualStyleBackColor = true;
            btnHostServer.Click += BtnHostServer_Click;
            // 
            // btnJoinChat
            // 
            btnJoinChat.Dock = DockStyle.Right;
            btnJoinChat.Location = new Point(450, 266);
            btnJoinChat.Name = "btnJoinChat";
            btnJoinChat.Size = new Size(150, 111);
            btnJoinChat.TabIndex = 3;
            btnJoinChat.Text = "Chat Beitreten";
            btnJoinChat.UseVisualStyleBackColor = true;
            btnJoinChat.Click += BtnJoinChat_Click;
            // 
            // ChatForm
            // 
            ClientSize = new Size(600, 400);
            Controls.Add(btnJoinChat);
            Controls.Add(btnHostServer);
            Controls.Add(txtInput);
            Controls.Add(txtMessages);
            Name = "ChatForm";
            Text = "Chat Anwendung";
            ResumeLayout(false);
            PerformLayout();
        }
        #endregion

        private System.Windows.Forms.TextBox txtMessages;
        private System.Windows.Forms.TextBox txtInput;
        private System.Windows.Forms.Button btnHostServer;
        private System.Windows.Forms.Button btnJoinChat;
    }
}
