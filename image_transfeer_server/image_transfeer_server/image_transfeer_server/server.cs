using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Net.Sockets;
using System.IO;
using System.Threading;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Binary;
using System.Net;



namespace Files_transfeer_server
{

    public partial class sever : Form
    {
        
        #region Class DataMember

                public static TextBox user_TextBox_Receive; 

                public static TextBox user_TextBox_Receive_Text_Data;

                public static ToolStripStatusLabel label_client_info;
                
                TcpListener listener;

                FileStream file; 

                Handle_Request server1=null;

                List<Handle_Request> server_requests;

                public  static Dictionary<string, Socket>clientsList;

                Thread threadStrart;

                public static List<string> Rooms = new List<string>();


        #endregion 



                [Serializable]
                class AES_key
                {
                    byte[] key;

                    public byte[] Key
                    {
                        get { return key; }
                        set { key = value; }
                    }

                    byte[] Iv;

                    public byte[] IV
                    {
                        get { return Iv; }
                        set { IV = value; }
                    }

                    public AES_key(byte[] key, byte[] IV)
                    {
                        this.key = key;
                        this.Iv = IV;
                    }
                }



        static int i = 0;


        #region public server()
        /// <summary>
        /// initialization  Datamember for the Server
        /// </summary>
        public sever()
        {
            InitializeComponent();

            user_TextBox_Receive = TextBox_Receive;

            user_TextBox_Receive_Text_Data = TextBox_Receive_Text_Data;

            label_client_info = toolStripStatusLabel1;

            TextBox_ServerIp.Text = GetIP();

            server_requests = new List<Handle_Request>();

            clientsList= new Dictionary<string, Socket>();

            if (TextBox_PortNumber.Text != null)
            {
                listener = new TcpListener(Convert.ToInt32(TextBox_PortNumber.Text));

                listener.Start(); //Start Listening  for Incoming connection By Clients

                threadStrart = new Thread(new ThreadStart(run_threads));

                threadStrart.Start();// You Can Puton  in StartListening Button 

               // listener.AcceptSocket();



            }
            else
                MessageBox.Show("Pleas Enter the Port Number");





/*            BinaryFormatter bf = new BinaryFormatter();

            FileStream fs = new FileStream(@"D:\Keys\AES_Key", FileMode.Open);

            Files_transfreer_client.Client.AES_key keys = (Files_transfreer_client.Client.AES_key)bf.Deserialize(fs);

            Handle_Request.aescsp.Key = keys.Key;

            Handle_Request.aescsp.IV = keys.IV;

            fs.Close();*/




        }
        #endregion 



        #region public void build_Table()
        /// <summary>
        /// Show the Connected Clients and Details for connected Clients 
        /// </summary>
         public void build_Table()
        {
            try
            {
                dataGridView_Clients.Rows.Clear();

                checkedListBox_Clients.Items.Clear();
            }
            catch (Exception err) { }
            try
            {
                int j = 0;
                for (int i = 0; i < server_requests.Count; i++)
                {
                    try
                    {
                        if ((server_requests[i].Myscoket != null) && (server_requests[i].Myscoket.Connected))
                        {
                            dataGridView_Clients.Rows.Add(); //Add Row

                            dataGridView_Clients.Rows[j].Cells[0].Value = server_requests[i].Client_name;

                            dataGridView_Clients.Rows[j].Cells[1].Value = "Connected";

                            dataGridView_Clients.Rows[j].Cells[2].Value = server_requests[i].Myscoket.RemoteEndPoint.ToString();

                            if (server_requests[i].ArrFile != null)
                                dataGridView_Clients.Rows[j].Cells[3].Value = server_requests[i].ArrFile.Length.ToString() + " Byte";
                            else
                                dataGridView_Clients.Rows[j].Cells[3].Value = "Not Send Any File";

                            j++;

                            checkedListBox_Clients.Items.Add(server_requests[i].Client_name);

                        }
                    }
                    catch (Exception err) { MessageBox.Show(err.Message); }
                }
            }
            catch (Exception err) { }


        }
        #endregion


        #region  public void run_thread()
         /// <summary>
        /// Thread that Generates Thread for Any Client you Want To Coonect With Server
        /// using serverThread  in   Handle_Requset Class
        /// </summary>
        public void run_threads()
        {
            while(true)
            {
                try
                {
                    server_requests.Add(new Handle_Request(listener, listener.AcceptSocket()));

                    //checkedListBox_Clients.Items.Add("Client " + i.ToString());

                    server_requests[i].start();

                    
                   string s=server_requests[i].Myscoket.RemoteEndPoint.ToString();

                   string IP=null;

                   //for (int j = 0; j < s.IndexOf(':'); j++)
                   //     if (s[j] != ':')
                   //         IP += s[j];

                   IP = s;


                    //Dictionary  == Hash Table
                    clientsList.Add(IP,server_requests[i].Myscoket);

                   // MessageBox.Show(IP);

                  
                    //build_Table();

                    i++;

                }
                catch (Exception err) { }//MessageBox.Show(err.Message); }
            }

        }
         #endregion


        #region  public void send_file(int i)
        /// <summary>
        /// Send Any  File to The Endpoint 
        /// </summary>
        /// <param name="i">index of Client Socket in server_requests List </param>
        public void send_file(int i)
        {
         
            if ((server_requests[i].Myscoket != null) && (server_requests[i].Myscoket.Connected))
            {
                try
                {
                    server1 = server_requests[i];

                    server1.Writer = new BinaryWriter(server1.MynetworkStream);

                    server1.Writer.Write((byte)1); // Sending Any  File 

                    byte[] Buffer = File.ReadAllBytes(path);

                    server1.Writer.Write("No_Encryption"); //send Tag

                    server1.Writer.Write(Buffer.Length); //send the size of file  

                    // Writing Data 
                    server1.MynetworkStream.Write(Buffer, 0, Buffer.Length);

                    TextBox_Send.Text = Buffer.Length.ToString();

                }
                catch (Exception error) { }//MessageBox.Show(error.Message); }
            }
            else
            {
                //MessageBox.Show("Client is Disconnected");

            }
        }
        #endregion


        string path = null;

        //Send File Button
        #region private void button_Send_Click(object sender, EventArgs e)
        private void button_Send_Click(object sender, EventArgs e)
        {
            if ((!(checkBox_Send2All.Checked))&&(checkedListBox_Clients.SelectedItems.Count==0))
            {
                MessageBox.Show("Pleas Select Client Or  more Client To Send File To it","Error !",MessageBoxButtons.OK,MessageBoxIcon.Error);

                return;
            }

            try
            {
                OpenFileDialog.ShowDialog();

                path = OpenFileDialog.FileName;
            }
            catch (Exception err) { MessageBox.Show("pleas select any file to Send "); return; }

            if (checkBox_Send2All.Checked)
            {
                
                for (int i = 0; i < server_requests.Count; i++)
                    send_file(i);
            }
            else
            {
                for (int i = 0; i < checkedListBox_Clients.CheckedItems.Count; i++)
                {
                    try
                    {
                        string C_name = Convert.ToString(checkedListBox_Clients.CheckedItems[i]);

                        int index = index_of(C_name);

                        send_file(index);

                    }
                    catch (Exception err) { }
                }
            }
        }
        #endregion


        #region public void send_Text_Data(int i)
        /// <summary>
        /// Send Tex Data To EndPoint 
        /// </summary>
        /// <param name="i">index of Client Socket in server_requests List </param>
        public void send_Text_Data(int i)
        {
            if ((server_requests[i].Myscoket != null) && (server_requests[i].Myscoket.Connected))
            {
                try
                {
                    server1 = server_requests[i];

                    server1.Writer = new BinaryWriter(server1.MynetworkStream);

                    if (TextBox_Input.Text != " ")
                    {

                        server1.Writer.Write((byte)0); //Sending Text Data 

                        server1.Writer.Write("AES"); //Sending Tag 

                        server1.Writer.Write(server1.aescsp.Mode.ToString()); //Sending AES Mode

                        server1.Writer.Write((byte)0); //Sending Tag2=0 means that the message is from server 

                        byte[] en_message = AES_Encryption.EncryptString(server1.aescsp, "Server>>" + TextBox_Input.Text);

                        server1.Writer.Write(en_message.Length);

                        server1.Writer.Write(en_message);
                    }

                }
                catch (Exception Err) { } //MessageBox.Show(Err.Message); }

                if (TextBox_Input.Text.ToLower() == "terminate") // Optional ; this Way Can be used To Disconnect The Connection
                    server1.Myscoket.Close();
            }

            else
            {
                //MessageBox.Show("Reomote End point is Abort The Connecton");

                //TextBox_Input.Clear();
            }

        }
       #endregion


        



        //Send Text Data Button
        #region  private void button_Send_Text_Click(object sender, EventArgs e)
        private void button_Send_Text_Click(object sender, EventArgs e)
        {
            if ((!(checkBox_Send2All.Checked)) && (checkedListBox_Clients.SelectedItems.Count == 0))
            {
                MessageBox.Show("Pleas Select Client Or  more Client To Send File To it", "Error !", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (checkBox_Send2All.Checked)
            {
                for (int i = 0; i < server_requests.Count; i++)
                    send_Text_Data(i);


            }
            else
            {
                for (int i = 0; i < checkedListBox_Clients.CheckedItems.Count; i++)
                {
                    try
                    {
                        string C_name = Convert.ToString(checkedListBox_Clients.CheckedItems[i]);

                        int index = index_of(C_name);

                        send_Text_Data(index);
                    }
                    catch (Exception err) { }
                }
            }

            TextBox_Receive_Text_Data.Text +="Server>>" + TextBox_Input.Text + "\r\n";

            TextBox_Input.Clear();

        }

        #endregion


        private int index_of(string name)
        {
            for (int i = 0; i < server_requests.Count; i++)
                if (name == server_requests[i].Client_name)
                    return i;

            return -1;
        }



        //Save Button ; Very Important ; Click at ShowConnected Client To See the Sizes Files that Server is Received
        #region  private void button_Save_Click(object sender, EventArgs e)
        private void button_Save_Click(object sender, EventArgs e)
        {
            FileStream file = null;

            if ((!(checkBox_Send2All.Checked)) && (checkedListBox_Clients.SelectedItems.Count == 0))
                MessageBox.Show("Pleas Select The Client You Want To Save  File form it ","Error !",MessageBoxButtons.OK,MessageBoxIcon.Error);

            else if (checkBox_Send2All.Checked) //Send To All Conncected Client 
            {
                for (int i = 0; i < server_requests.Count; i++)
                {
                    SaveFileDialog.Title = "Saving The Received File From " + Convert.ToString(i);

                    if ((server_requests[i] != null) && (server_requests[i].Myscoket.Connected))
                    {
                        server1 = server_requests[i];

                        if (SaveFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            // You need to Try-catch-finally
                            try
                            {
                                if (server1.ArrFile != null)
                                {
                                    file = new FileStream(SaveFileDialog.FileName, FileMode.Create); ;

                                    file.Write(server1.ArrFile, 0, server1.ArrFile.Length);

                                    file.Flush();

                                    MessageBox.Show("The Received File From " +"Client " + i.ToString() + "is Saved ","Confirm The Save Operation ",MessageBoxButtons.OK,MessageBoxIcon.Information);
                                }
                                else
                                    MessageBox.Show("the Client " + i.ToString() + " Does Not Send Any File to the Server ");
                            }
                            catch (Exception Error) { MessageBox.Show(Error.Message); }
                            finally
                            {
                                try
                                {
                                    file.Close();
                                }
                                catch (Exception err) { }
                            }

                        }
                    }

                }//End For 

            }//End Else

            else // Send To more Clients
            {

                for (int i = 0; i < checkedListBox_Clients.CheckedItems.Count; i++)
                {

                    SaveFileDialog.Title = "Saving The Received File From " + Convert.ToString(checkedListBox_Clients.CheckedItems[i]);

                    string s = Convert.ToString(checkedListBox_Clients.CheckedItems[i]).Remove(0, 6);

                    int clientnumber = Convert.ToInt32(s);

                    server1 = server_requests[clientnumber];

                    if (SaveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        // You need to Try-catch-finally

                        try
                        {
                            if (server1.ArrFile != null)
                            {
                                file = new FileStream(SaveFileDialog.FileName, FileMode.Create); ;

                                file.Write(server1.ArrFile, 0, server1.ArrFile.Length);

                                file.Flush();
                                MessageBox.Show("The Received File From " + Convert.ToString(checkedListBox_Clients.CheckedItems[i]) + "is Saved ", "Confirm The Save Operation ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            }
                            else
                                MessageBox.Show("the Client " + clientnumber.ToString() + " Does Not Send Any File to the Server ");
                        }
                        catch (Exception Error) { MessageBox.Show(Error.Message); }
                        finally
                        {
                            try
                            {
                                file.Close();
                            }
                            catch (Exception err) { }
                        }
                    }

                }//End For 

            }//end Else


        }
        #endregion




        // important
        #region private void server_FormClosing(object sender, FormClosingEventArgs e)
                private void server_FormClosing(object sender, FormClosingEventArgs e)
                {
                    System.Environment.Exit(System.Environment.ExitCode); //Terminates All Running Thread
                }
        #endregion


        #region private void Button_Start_Listening_Click(object sender, EventArgs e)
        private void Button_Start_Listening_Click(object sender, EventArgs e)
        {
            try
            {
                //threadStrart.Start(); // To Enable The Start listening button
            }
            catch (Exception err) { MessageBox.Show(err.Message); }

        }
        #endregion 



        #region private void Button_Stop_listening_Click(object sender, EventArgs e)
        private void Button_Stop_listening_Click(object sender, EventArgs e)
        {
            try
            {
                listener.Stop(); //Stop Listening 
            }
            catch (Exception err) { }
        }
        #endregion


        #region String GetIP()
        /// <summary>
        /// Get IP of this Computer 
        /// </summary>
        /// <returns>Get IP of this Computer  </returns>
        String GetIP()
        {
            String strHostName = Dns.GetHostName();
          
            // Find host by name
            IPHostEntry iphostentry = Dns.GetHostByName(strHostName);

            // Grab the first IP addresses
            String IPStr = "";
            foreach (IPAddress ipaddress in iphostentry.AddressList)
            {
                IPStr = ipaddress.ToString();

                return IPStr;
            }
            return IPStr;
        }
        #endregion


        #region private void button_Show_Table_Click(object sender, EventArgs e)
        private void button_Show_Table_Click(object sender, EventArgs e)
        {
            build_Table();
        }
        #endregion 

        private void checkBox_Send2All_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void TextBox_Receive_Text_Data_TextChanged(object sender, EventArgs e)
        {

        }

        private void button_add_room_Click(object sender, EventArgs e)
        {
            if (!Rooms.Contains(textBox_room_name.Text))
            {
                Rooms.Add(textBox_room_name.Text);

                MessageBox.Show("room Added Successfully ");
            }
            else
                MessageBox.Show("duplicated room !");
        }

        private void sever_Load(object sender, EventArgs e)
        {

        }

    }
}
