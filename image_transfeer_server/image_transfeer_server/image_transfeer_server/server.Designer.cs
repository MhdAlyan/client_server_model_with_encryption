namespace Files_transfeer_server
{
    partial class sever
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SaveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.OpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Label_Client_Show = new System.Windows.Forms.Label();
            this.checkBox_Send2All = new System.Windows.Forms.CheckBox();
            this.checkedListBox_Clients = new System.Windows.Forms.CheckedListBox();
            this.button_Send_Text = new System.Windows.Forms.Button();
            this.TextBox_Input = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.TextBox_Send = new System.Windows.Forms.TextBox();
            this.Button_Send = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.TextBox_Receive_Text_Data = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.TextBox_Receive = new System.Windows.Forms.TextBox();
            this.Button_Save = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.Button_Stop_listening = new System.Windows.Forms.Button();
            this.Button_Start_Listening = new System.Windows.Forms.Button();
            this.label_PortNumber = new System.Windows.Forms.Label();
            this.label_ServerIp = new System.Windows.Forms.Label();
            this.TextBox_PortNumber = new System.Windows.Forms.TextBox();
            this.TextBox_ServerIp = new System.Windows.Forms.TextBox();
            this.dataGridView_Clients = new System.Windows.Forms.DataGridView();
            this.Column_Clients = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column_State = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column_Client_IP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Size_of_Sent_File = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button_Show_Table = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.button_add_room = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox_room_name = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Clients)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // OpenFileDialog
            // 
            this.OpenFileDialog.FileName = "openFileDialog1";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Label_Client_Show);
            this.groupBox1.Controls.Add(this.checkBox_Send2All);
            this.groupBox1.Controls.Add(this.checkedListBox_Clients);
            this.groupBox1.Controls.Add(this.button_Send_Text);
            this.groupBox1.Controls.Add(this.TextBox_Input);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.TextBox_Send);
            this.groupBox1.Controls.Add(this.Button_Send);
            this.groupBox1.Location = new System.Drawing.Point(12, 148);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(321, 276);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Sending ";
            // 
            // Label_Client_Show
            // 
            this.Label_Client_Show.AutoSize = true;
            this.Label_Client_Show.Font = new System.Drawing.Font("Andalus", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label_Client_Show.ForeColor = System.Drawing.Color.Red;
            this.Label_Client_Show.Location = new System.Drawing.Point(2, 109);
            this.Label_Client_Show.Name = "Label_Client_Show";
            this.Label_Client_Show.Size = new System.Drawing.Size(311, 21);
            this.Label_Client_Show.TabIndex = 19;
            this.Label_Client_Show.Text = "Selcet The Client You Want To Sent To It OR Save File from it";
            // 
            // checkBox_Send2All
            // 
            this.checkBox_Send2All.AutoSize = true;
            this.checkBox_Send2All.Location = new System.Drawing.Point(6, 136);
            this.checkBox_Send2All.Name = "checkBox_Send2All";
            this.checkBox_Send2All.Size = new System.Drawing.Size(117, 17);
            this.checkBox_Send2All.TabIndex = 18;
            this.checkBox_Send2All.Text = " Send To All Clients";
            this.checkBox_Send2All.UseVisualStyleBackColor = true;
            this.checkBox_Send2All.CheckedChanged += new System.EventHandler(this.checkBox_Send2All_CheckedChanged);
            // 
            // checkedListBox_Clients
            // 
            this.checkedListBox_Clients.ColumnWidth = 150;
            this.checkedListBox_Clients.FormattingEnabled = true;
            this.checkedListBox_Clients.Location = new System.Drawing.Point(128, 132);
            this.checkedListBox_Clients.Name = "checkedListBox_Clients";
            this.checkedListBox_Clients.ScrollAlwaysVisible = true;
            this.checkedListBox_Clients.Size = new System.Drawing.Size(173, 139);
            this.checkedListBox_Clients.TabIndex = 13;
            // 
            // button_Send_Text
            // 
            this.button_Send_Text.Location = new System.Drawing.Point(6, 77);
            this.button_Send_Text.Name = "button_Send_Text";
            this.button_Send_Text.Size = new System.Drawing.Size(107, 23);
            this.button_Send_Text.TabIndex = 17;
            this.button_Send_Text.Text = " Send Text Data";
            this.button_Send_Text.UseVisualStyleBackColor = true;
            this.button_Send_Text.Click += new System.EventHandler(this.button_Send_Text_Click);
            // 
            // TextBox_Input
            // 
            this.TextBox_Input.Location = new System.Drawing.Point(119, 77);
            this.TextBox_Input.Name = "TextBox_Input";
            this.TextBox_Input.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.TextBox_Input.Size = new System.Drawing.Size(182, 20);
            this.TextBox_Input.TabIndex = 11;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(119, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(123, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Size of File You are Sent";
            // 
            // TextBox_Send
            // 
            this.TextBox_Send.Location = new System.Drawing.Point(119, 38);
            this.TextBox_Send.Name = "TextBox_Send";
            this.TextBox_Send.ReadOnly = true;
            this.TextBox_Send.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.TextBox_Send.Size = new System.Drawing.Size(182, 20);
            this.TextBox_Send.TabIndex = 8;
            // 
            // Button_Send
            // 
            this.Button_Send.Location = new System.Drawing.Point(6, 35);
            this.Button_Send.Name = "Button_Send";
            this.Button_Send.Size = new System.Drawing.Size(107, 23);
            this.Button_Send.TabIndex = 7;
            this.Button_Send.Text = "Send File";
            this.Button_Send.UseVisualStyleBackColor = true;
            this.Button_Send.Click += new System.EventHandler(this.button_Send_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.TextBox_Receive_Text_Data);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.TextBox_Receive);
            this.groupBox2.Controls.Add(this.Button_Save);
            this.groupBox2.Location = new System.Drawing.Point(577, 23);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(273, 401);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Receiving";
            // 
            // TextBox_Receive_Text_Data
            // 
            this.TextBox_Receive_Text_Data.Location = new System.Drawing.Point(19, 82);
            this.TextBox_Receive_Text_Data.Multiline = true;
            this.TextBox_Receive_Text_Data.Name = "TextBox_Receive_Text_Data";
            this.TextBox_Receive_Text_Data.ReadOnly = true;
            this.TextBox_Receive_Text_Data.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.TextBox_Receive_Text_Data.Size = new System.Drawing.Size(239, 308);
            this.TextBox_Receive_Text_Data.TabIndex = 9;
            this.TextBox_Receive_Text_Data.TextChanged += new System.EventHandler(this.TextBox_Receive_Text_Data_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(94, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(174, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Size of   Last File You are Received";
            // 
            // TextBox_Receive
            // 
            this.TextBox_Receive.Location = new System.Drawing.Point(97, 38);
            this.TextBox_Receive.Name = "TextBox_Receive";
            this.TextBox_Receive.ReadOnly = true;
            this.TextBox_Receive.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.TextBox_Receive.Size = new System.Drawing.Size(161, 20);
            this.TextBox_Receive.TabIndex = 7;
            // 
            // Button_Save
            // 
            this.Button_Save.Location = new System.Drawing.Point(16, 36);
            this.Button_Save.Name = "Button_Save";
            this.Button_Save.Size = new System.Drawing.Size(75, 23);
            this.Button_Save.TabIndex = 6;
            this.Button_Save.Text = "save File";
            this.Button_Save.UseVisualStyleBackColor = true;
            this.Button_Save.Click += new System.EventHandler(this.button_Save_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 475);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1192, 22);
            this.statusStrip1.TabIndex = 11;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.Button_Stop_listening);
            this.groupBox3.Controls.Add(this.Button_Start_Listening);
            this.groupBox3.Controls.Add(this.label_PortNumber);
            this.groupBox3.Controls.Add(this.label_ServerIp);
            this.groupBox3.Controls.Add(this.TextBox_PortNumber);
            this.groupBox3.Controls.Add(this.TextBox_ServerIp);
            this.groupBox3.Location = new System.Drawing.Point(12, 23);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(321, 119);
            this.groupBox3.TabIndex = 12;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Setting";
            // 
            // Button_Stop_listening
            // 
            this.Button_Stop_listening.Location = new System.Drawing.Point(135, 82);
            this.Button_Stop_listening.Name = "Button_Stop_listening";
            this.Button_Stop_listening.Size = new System.Drawing.Size(134, 23);
            this.Button_Stop_listening.TabIndex = 14;
            this.Button_Stop_listening.Text = "Stop Listening";
            this.Button_Stop_listening.UseVisualStyleBackColor = true;
            this.Button_Stop_listening.Click += new System.EventHandler(this.Button_Stop_listening_Click);
            // 
            // Button_Start_Listening
            // 
            this.Button_Start_Listening.Location = new System.Drawing.Point(6, 82);
            this.Button_Start_Listening.Name = "Button_Start_Listening";
            this.Button_Start_Listening.Size = new System.Drawing.Size(110, 23);
            this.Button_Start_Listening.TabIndex = 13;
            this.Button_Start_Listening.Text = "Start Listening";
            this.Button_Start_Listening.UseVisualStyleBackColor = true;
            this.Button_Start_Listening.Click += new System.EventHandler(this.Button_Start_Listening_Click);
            // 
            // label_PortNumber
            // 
            this.label_PortNumber.AutoSize = true;
            this.label_PortNumber.Location = new System.Drawing.Point(20, 52);
            this.label_PortNumber.Name = "label_PortNumber";
            this.label_PortNumber.Size = new System.Drawing.Size(61, 13);
            this.label_PortNumber.TabIndex = 12;
            this.label_PortNumber.Text = "Port Nmber";
            // 
            // label_ServerIp
            // 
            this.label_ServerIp.AutoSize = true;
            this.label_ServerIp.Location = new System.Drawing.Point(20, 26);
            this.label_ServerIp.Name = "label_ServerIp";
            this.label_ServerIp.Size = new System.Drawing.Size(50, 13);
            this.label_ServerIp.TabIndex = 11;
            this.label_ServerIp.Text = "Server ip";
            // 
            // TextBox_PortNumber
            // 
            this.TextBox_PortNumber.Location = new System.Drawing.Point(105, 45);
            this.TextBox_PortNumber.Name = "TextBox_PortNumber";
            this.TextBox_PortNumber.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.TextBox_PortNumber.Size = new System.Drawing.Size(182, 20);
            this.TextBox_PortNumber.TabIndex = 10;
            this.TextBox_PortNumber.Text = "5000";
            // 
            // TextBox_ServerIp
            // 
            this.TextBox_ServerIp.Location = new System.Drawing.Point(105, 19);
            this.TextBox_ServerIp.Name = "TextBox_ServerIp";
            this.TextBox_ServerIp.ReadOnly = true;
            this.TextBox_ServerIp.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.TextBox_ServerIp.Size = new System.Drawing.Size(182, 20);
            this.TextBox_ServerIp.TabIndex = 9;
            // 
            // dataGridView_Clients
            // 
            this.dataGridView_Clients.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_Clients.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column_Clients,
            this.Column_State,
            this.Column_Client_IP,
            this.Size_of_Sent_File});
            this.dataGridView_Clients.Location = new System.Drawing.Point(856, 45);
            this.dataGridView_Clients.Name = "dataGridView_Clients";
            this.dataGridView_Clients.Size = new System.Drawing.Size(464, 382);
            this.dataGridView_Clients.TabIndex = 13;
            // 
            // Column_Clients
            // 
            this.Column_Clients.HeaderText = "Clients";
            this.Column_Clients.Name = "Column_Clients";
            this.Column_Clients.ReadOnly = true;
            this.Column_Clients.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // Column_State
            // 
            this.Column_State.HeaderText = "State";
            this.Column_State.Name = "Column_State";
            // 
            // Column_Client_IP
            // 
            this.Column_Client_IP.HeaderText = "Client IP:Port";
            this.Column_Client_IP.Name = "Column_Client_IP";
            // 
            // Size_of_Sent_File
            // 
            this.Size_of_Sent_File.HeaderText = "Size_of_last_Sent_File";
            this.Size_of_Sent_File.Name = "Size_of_Sent_File";
            this.Size_of_Sent_File.Width = 120;
            // 
            // button_Show_Table
            // 
            this.button_Show_Table.Location = new System.Drawing.Point(856, 17);
            this.button_Show_Table.Name = "button_Show_Table";
            this.button_Show_Table.Size = new System.Drawing.Size(173, 23);
            this.button_Show_Table.TabIndex = 14;
            this.button_Show_Table.Text = "Show Connected Client Now";
            this.button_Show_Table.UseVisualStyleBackColor = true;
            this.button_Show_Table.Click += new System.EventHandler(this.button_Show_Table_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.button_add_room);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.textBox_room_name);
            this.groupBox4.Location = new System.Drawing.Point(339, 23);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(221, 160);
            this.groupBox4.TabIndex = 15;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Add Chat Room ";
            // 
            // button_add_room
            // 
            this.button_add_room.Location = new System.Drawing.Point(23, 96);
            this.button_add_room.Name = "button_add_room";
            this.button_add_room.Size = new System.Drawing.Size(110, 23);
            this.button_add_room.TabIndex = 13;
            this.button_add_room.Text = "Add Room";
            this.button_add_room.UseVisualStyleBackColor = true;
            this.button_add_room.Click += new System.EventHandler(this.button_add_room_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 26);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Room Name";
            // 
            // textBox_room_name
            // 
            this.textBox_room_name.Location = new System.Drawing.Point(23, 49);
            this.textBox_room_name.Name = "textBox_room_name";
            this.textBox_room_name.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox_room_name.Size = new System.Drawing.Size(182, 20);
            this.textBox_room_name.TabIndex = 9;
            // 
            // sever
            // 
            this.AcceptButton = this.button_Send_Text;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1192, 497);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.button_Show_Table);
            this.Controls.Add(this.dataGridView_Clients);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "sever";
            this.Text = "server";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.server_FormClosing);
            this.Load += new System.EventHandler(this.sever_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Clients)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SaveFileDialog SaveFileDialog;
        private System.Windows.Forms.OpenFileDialog OpenFileDialog;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TextBox_Send;
        private System.Windows.Forms.Button Button_Send;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TextBox_Receive;
        private System.Windows.Forms.Button Button_Save;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox TextBox_PortNumber;
        private System.Windows.Forms.TextBox TextBox_ServerIp;
        private System.Windows.Forms.Label label_PortNumber;
        private System.Windows.Forms.Label label_ServerIp;
        private System.Windows.Forms.Button Button_Stop_listening;
        private System.Windows.Forms.Button Button_Start_Listening;
        private System.Windows.Forms.TextBox TextBox_Receive_Text_Data;
        private System.Windows.Forms.TextBox TextBox_Input;
        private System.Windows.Forms.Button button_Send_Text;
        private System.Windows.Forms.CheckedListBox checkedListBox_Clients;
        private System.Windows.Forms.DataGridView dataGridView_Clients;
        private System.Windows.Forms.CheckBox checkBox_Send2All;
        private System.Windows.Forms.Button button_Show_Table;
        private System.Windows.Forms.Label Label_Client_Show;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_Clients;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_State;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_Client_IP;
        private System.Windows.Forms.DataGridViewTextBoxColumn Size_of_Sent_File;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button button_add_room;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox_room_name;
    }
}

