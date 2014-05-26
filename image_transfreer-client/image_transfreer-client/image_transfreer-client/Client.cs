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
using System.ServiceModel;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Security.Cryptography;


namespace Files_transfreer_client
{
    public partial class Client : Form
    {
        #region DataMembers

           #region Static DataMember To Access the userControls on Form From SendRequset Class
                public static TextBox user_TextBox_Receive;

                public static TextBox user_TextBox_Receive_Text_Data;

                public static TextBox user_TextBox_ServerIP;

                public static TextBox user_TextBox_ClientName; //new 

                public static ComboBox compobox_Clientrooms; //new 

                public static ComboBox compobox_Clientrooms_2_send; //new 



                public static CheckedListBox checkedListBox_Clients1; //new 

                public static ComboBox combobox_ClientsNames; //new 

                public static TextBox user_TextBox_portnmber;

                public static ToolStripStatusLabel label_Server_info;
                

          #endregion 

             
            

            SendRequest clientSendRequset; // Camel Form 

            // key=ClientName   Value=ip:port                           
           public static Dictionary<string, string> Client_list = null; // new Dictionary<string, string>(); //new

           // key=client name   Value=Public Key      
           public static Dictionary<string, string> Clients_public_keys = new Dictionary<string, string>();

           // key=Client name   Value=room name      
           public static Dictionary<string, string> Clients_rooms = new Dictionary<string, string>();

           public static List<string> Rooms = null;
               
        #endregion 



           [Serializable]
          public class AES_key
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
                   set { Iv = value; }
               }

               public AES_key(byte[] key, byte[] IV)
               {
                   this.key = key;
                   this.Iv = IV;
               }
           }


        public static AesCryptoServiceProvider aescsp = new AesCryptoServiceProvider();

     

        #region public Client()
         /// <summary>
        /// initialization Datamember for Begin Connecting With Server 
        /// </summary>
        public Client()
        {
            InitializeComponent(); 

            user_TextBox_portnmber = TextBox_portNumber;

            user_TextBox_ServerIP = TextBox_serverIp;

            user_TextBox_ClientName = textBox_ClientName;//new 

            compobox_Clientrooms = comboBox_rooms;

            checkedListBox_Clients1 = checkedListBox_Clients;//new 

            combobox_ClientsNames = comboBox_Remote_Client_Name; //new 

            compobox_Clientrooms_2_send = comboBox_room_to_send;

            user_TextBox_Receive = textBox_Receive;

            user_TextBox_Receive_Text_Data = TextBox_Receive_Text_Data;

            label_Server_info = ToolStripStatusLabel_Server_ingo;

            BinaryFormatter bf = new BinaryFormatter();


            /*FileStream fs = new FileStream(@"D:\Keys\AES_Key", FileMode.Create);

            aescsp.GenerateKey();

            aescsp.GenerateIV();

            byte [] k=aescsp.Key;

            byte [] iv=aescsp.IV;

            AES_key keys = new  AES_key(k,iv);

            bf.Serialize(fs, keys); 

            fs.Close(); */


            //Deserialize 
            /*
            FileStream  fs = new FileStream(@"D:\Keys\AES_Key", FileMode.Open);

            AES_key keys = (AES_key)bf.Deserialize(fs);


            aescsp.Key = keys.Key;

            aescsp.IV = keys.IV;

            fs.Close(); */

            


            clientSendRequset = new SendRequest();

        }
        #endregion


        //Send Button 
        #region private void Button_Send_Click_1(object sender, EventArgs e)
        private void Button_Send_Click_1(object sender, EventArgs e)
        {
            if (clientSendRequset.Client.Connected)
            {
                try
                {
                    OpenFileDialog.ShowDialog();

                    string path = OpenFileDialog.FileName;

                    clientSendRequset.Writer = new BinaryWriter(clientSendRequset.MynetworkStream);//**

                    clientSendRequset.Writer.Write((byte)1);

                    byte[] buffer = File.ReadAllBytes(path); 

                    clientSendRequset.Writer.Write(buffer.Length); //send the size of file  

                    //Sending Data 
                    clientSendRequset.MynetworkStream.Write(buffer, 0, buffer.Length);  //same as above (2)

                    TextBox_Send.Text = buffer.Length.ToString(); // Show Size of File

                }
                catch (Exception ee)
                {
                    MessageBox.Show(ee.Message);
                }
            }
            else
                MessageBox.Show("the Client is Disconnected");
        }
        #endregion 


        //Save Button 
        #region private void Button_Save_Click_1(object sender, EventArgs e)
        private void Button_Save_Click_1(object sender, EventArgs e)
        {
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                // You need to Try-catch-finally
                FileStream file=null;
                try
                {
                    file = new FileStream(saveFileDialog.FileName, FileMode.Create); ;
                    file.Write(clientSendRequset.ArrFile, 0, clientSendRequset.ArrFile.Length);
                    file.Flush();
                }
                catch (Exception Error) { MessageBox.Show(Error.Message); }
                finally
                {
                    try
                    {
                        file.Close();
                    }
                    catch (Exception err) { MessageBox.Show(err.Message); }
                }
            }
        }
        #endregion 


        //important 
        #region private void Client_FormClosing(object sender, FormClosingEventArgs e)
        private void Client_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Environment.Exit(System.Environment.ExitCode);
        }
        #endregion 




        // Begin Connecting To the Server (Connect Button )
        #region private void Button_Connect_Click(object sender, EventArgs e)
        private void Button_Connect_Click(object sender, EventArgs e)
        {
            /*clientSendRequset.RequestStatus= 1;
            try
            {
                    user_TextBox_ClientName.ReadOnly = true;

                    clientSendRequset = new SendRequest(); 

                    clientSendRequset.start();
            }
            catch (Exception err) { MessageBox.Show(err.Message); } // if the user is Press on Connect button More Than One Time
             * */

            if(clientSendRequset!=null)
                clientSendRequset.ReadThread.Resume();
          
        }
        #endregion 


        //Disconnect Button 
        #region private void Button_Disconnect_Click(object sender, EventArgs e)
        private void Button_Disconnect_Click(object sender, EventArgs e)
        {
            clientSendRequset.AbortRequset();


            clientSendRequset.RequestStatus= 0; // that Means the Client is not Connected with the Server anthor Time
            
            clientSendRequset.Client.Close();

            MessageBox.Show("Client is Disconnected");

            ToolStripStatusLabel_Server_ingo.Text = "";
        }
        #endregion 




        //Send Text Data To The Server
        #region private void TextBox_input_KeyDown(object sender, KeyEventArgs e)
        private void TextBox_input_KeyDown(object sender, KeyEventArgs e)
        {
            if (clientSendRequset.Client.Connected)
            {
                if ((e.KeyCode == Keys.Enter))
                {
                    clientSendRequset.Writer = new BinaryWriter(clientSendRequset.MynetworkStream);

                    clientSendRequset.Writer.Write((byte)0); // 0 is Flag that Represent that the Client Send text Data

                    string message = user_TextBox_ClientName .Text+ ">>" + TextBox_Input.Text;


                    clientSendRequset.Writer.Write(comboBox_AES_Mode.Text);//send AES Mode

                    AES_Encryption.ChangeMode(comboBox_AES_Mode.Text, aescsp);

                    byte [] en_message=AES_Encryption.EncryptString(aescsp, message);



                    clientSendRequset.Writer.Write(en_message.Length); // Send  size of Data

                    clientSendRequset.Writer.Write(en_message); // Send Encrypted Data


                    TextBox_Receive_Text_Data.Text += message+"\r\n";

                    TextBox_Input.Clear();
                }
            }
            else
            {
               // MessageBox.Show("Client is Diconnected");
                TextBox_Input.Clear();
            }
        }
        #endregion 





        //Send File Data To The Selected Client
        #region private void Button_Send_2_Client_Click(object sender, EventArgs e)
        private void Button_Send_2_Client_Click(object sender, EventArgs e)
        {
            if (comboBox_Remote_Client_Name.SelectedItem.ToString() == "")
            {
                MessageBox.Show("Error: Pleas Enter Client IP To Send it", "Error at RemoteEndPoint IP ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (clientSendRequset.Client.Connected)
            {
                try
                {
                    OpenFileDialog.ShowDialog();

                    string path = OpenFileDialog.FileName;

                    clientSendRequset.Writer = new BinaryWriter(clientSendRequset.MynetworkStream); 

                    //1: Send Flag to The Server
                    clientSendRequset.Writer.Write((byte)2);// 2 That Means : the Cleint Wants To Send File To  Selected Client By IP Address 

                    //2 :  Send  IP  To server
                    clientSendRequset.Writer.Write(Client_list[comboBox_Remote_Client_Name.SelectedItem.ToString()]);// Very Important : Send IP Address of Client You Want To Send to it

                    byte[] buffer = File.ReadAllBytes(path);

                    
                    AES_Encryption.ChangeMode(comboBox_AES_Mode.Text, aescsp);


                    clientSendRequset.Writer.Write(comboBox_AES_Mode.Text); // Send AES Mode

                    clientSendRequset.Writer.Write(combobox_ClientsNames.Text); // Send remote client name

                    //new 
                    clientSendRequset.Writer.Write(textBox_ClientName.Text); // Sender client name

                    byte[] enbuffer = AES_Encryption.EncryptBytes(aescsp, buffer); 

                    //3 :send the size of  Encrypted file  
                    clientSendRequset.Writer.Write(enbuffer.Length);

                    //4: Send Data To Server Then the Server Send To the Selected Client(By Destination IP)
                    clientSendRequset.MynetworkStream.Write(enbuffer, 0, enbuffer.Length);

                    clientSendRequset.Rsa.FromXmlString(clientSendRequset.client_privateKey); // ***

                    byte[] signature = clientSendRequset.Rsa.SignData(enbuffer, new SHA1CryptoServiceProvider());

                    clientSendRequset.Writer.Write(signature.Length);

                    clientSendRequset.Writer.Write(signature);

                  
                     


                    TextBox_Send_To_Client.Text = buffer.Length.ToString(); //+ "," + enbuffer.Length.ToString();


                    //TextBox_Send_To_Client.Text = "file Before Encrypt="+ buffer.Length.ToString() +"Bytes and After Encryption ="+ enbuffer.Length.ToString();

                }
                catch (Exception ee)
                {
                    MessageBox.Show(ee.Message);
                }

            }
            else
                MessageBox.Show("the Client is Disconnected");
        }
        #endregion 




        //Send Text Data To The Selected Client
        #region private void TextBox_Input_Send_to_Client_KeyDown(object sender, KeyEventArgs e)
        private void TextBox_Input_Send_to_Client_KeyDown(object sender, KeyEventArgs e)
        {
            if ((comboBox_Remote_Client_Name.Text == "Select Client") && comboBox_room_to_send.Text == "Select Room" && (!checkBox_send_2all.Checked))
            {
                MessageBox.Show("Error: Pleas Enter Client IP To Send it", "Error at RemoteEndPoint IP ", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }
            if (clientSendRequset.Client.Connected)
            {
                if ((e.KeyCode == Keys.Enter))
                {

                    if (comboBox_Remote_Client_Name.Text!= "Select Client")// one 2 one
                    {
                        //Client_list[comboBox_Remote_Client_Name.SelectedItem.ToString()]

                        en_send(Client_list[comboBox_Remote_Client_Name.Text],"");

                        // print what i write

                        TextBox_Receive_Text_Data.Text += textBox_ClientName.Text + ">>" + TextBox_Input_Send_to_Client.Text + "\r\n";

                    }
                    else if (comboBox_room_to_send.Text != "Select Room") // to chat room 
                    {

                        string room_name = comboBox_room_to_send.Text;

                        foreach (string client_name in Clients_rooms.Keys)
                        {
                            if (room_name == Clients_rooms[client_name] && client_name!=textBox_ClientName.Text)
                            {
                               // MessageBox.Show("client_name = " + client_name); // 
                                en_send(Client_list[client_name],client_name);
                            }
                        }

                        // print what i write

                        TextBox_Receive_Text_Data.Text += textBox_ClientName.Text + ">>" + TextBox_Input_Send_to_Client.Text + "\r\n";
                    }
                    else if (checkBox_send_2all.Checked) // send 2 all
                    {

                        // MessageBox.Show("all");

                        foreach (var key in Client_list.Keys)
                        {
                            if (key != textBox_ClientName.Text)
                            {
                               // MessageBox.Show("key= " + key); //                           

                                en_send(Client_list[key], key);
                            }
                        }

                        // print what i write

                        TextBox_Receive_Text_Data.Text += textBox_ClientName.Text + ">>" + TextBox_Input_Send_to_Client.Text + "\r\n";

                    }


                    TextBox_Input_Send_to_Client.Clear();

                }
            }
            else
            {
                // MessageBox.Show("Client is Diconnected");
                TextBox_Input.Clear();
            }
        }
        #endregion 

        private void TextBox_Input_ImeModeChanged(object sender, EventArgs e)
        {

        }



        public static void update_ClientList()
        {
            try
            {
                checkedListBox_Clients1.Items.Clear();

                combobox_ClientsNames.Items.Clear();

                //"Select Client"


                checkedListBox_Clients1.Items.AddRange(Client_list.Keys.ToArray());


                combobox_ClientsNames.Items.Add("Select Client");
                combobox_ClientsNames.Items.AddRange(Client_list.Keys.ToArray());


            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

                //MessageBox.Show("ok");
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (clientSendRequset.Client.Connected)
            {


                clientSendRequset.Writer = new BinaryWriter(clientSendRequset.MynetworkStream);

                clientSendRequset.Writer.Write((byte)20); // 0 is Flag that Represent that the Client Send text Data

            }

        }

        private void Client_Load(object sender, EventArgs e)
        {
            clientSendRequset.RequestStatus = 1;
            try
            {                
                clientSendRequset = new SendRequest();

                clientSendRequset.start();


            }
            catch (Exception err) { MessageBox.Show(err.Message); } // if the user is Press on Connect button More Than One Time
          
        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }



        internal static void update_rooms()
        {

            compobox_Clientrooms.Items.Clear();
            compobox_Clientrooms.Items.AddRange(Rooms.ToArray());


            compobox_Clientrooms_2_send.Items.Clear();

            compobox_Clientrooms_2_send.Items.Add("Select Room");

            compobox_Clientrooms_2_send.Items.AddRange(Rooms.ToArray());

        }


        public void en_send(string clientIp,string client_name)
        {

                clientSendRequset.Writer = new BinaryWriter(clientSendRequset.MynetworkStream);

                //1
                clientSendRequset.Writer.Write((byte)3); // 3 is falg That means : the Cleint Wants To Send  To  Selected Client By IP Address 

                //2- Send IP:port
                clientSendRequset.Writer.Write(clientIp);// Very Important : Send IP Addresss of Client You Want To Send to it


                if (comboBox_TransfeerType.Text == "No_Encryption")
                {

                    clientSendRequset.Writer.Write("No_Encryption"); // Tag 

                    // : Sending  Text Data ; plain Text
                    clientSendRequset.Writer.Write(textBox_ClientName.Text + ">>" + TextBox_Input_Send_to_Client.Text);
                }
                else if (comboBox_TransfeerType.Text == "AES")
                {
                    // send Tag
                    clientSendRequset.Writer.Write("AES");

                    //Send AES Mode
                    clientSendRequset.Writer.Write(comboBox_AES_Mode.Text);


                    //Send Client Name
                    if(client_name=="")
                        clientSendRequset.Writer.Write(combobox_ClientsNames.Text);
                    else if(client_name!="")//broadcast mod
                        clientSendRequset.Writer.Write(client_name);



                    string message = textBox_ClientName.Text + ">>" + TextBox_Input_Send_to_Client.Text;

                    AES_Encryption.ChangeMode(comboBox_AES_Mode.Text, aescsp);

                    // Encryption 
                    byte[] en_message = AES_Encryption.EncryptString(aescsp, message);

                    clientSendRequset.Writer.Write(en_message.Length); // Send  size of Data

                    clientSendRequset.Writer.Write(en_message); // Send Encrypted Data
                    
                    //calculate hash and send to server 

                    MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();

                     byte [] ComputedHash= md5.ComputeHash(en_message);

                     clientSendRequset.Writer.Write(ComputedHash.Length);
                     clientSendRequset.Writer.Write(ComputedHash); // Send hash on en_message ;

                }
                else if (comboBox_TransfeerType.Text == "RSA")
                {

                    try
                    {
                        // send Tag
                        clientSendRequset.Writer.Write("RSA");

                        string pk = "";

                        if (client_name == "")
                            pk = Clients_public_keys[comboBox_Remote_Client_Name.Text]; // 
                        else if (client_name != "")//broadcast mod
                            pk = Clients_public_keys[client_name];

                      

                        RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();

                        rsa.FromXmlString(pk); // Encryption With Puplic Key

                        byte[] message = UTF8Encoding.Unicode.GetBytes(textBox_ClientName.Text + ">>" + TextBox_Input_Send_to_Client.Text);

                        byte[] en_message = rsa.Encrypt(message, false);

                        clientSendRequset.Writer.Write(en_message.Length); // Send  size of Data

                        clientSendRequset.Writer.Write(en_message); // Send Encrypted Data
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                }

               

                //
        }


        


    }//End Client Class

}//End nameSpace
