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


namespace image_transfeer_server
{
    class Server_Socket
    {

        TcpListener listener = null;

        Socket myscoket = null;

        NetworkStream mynetworkStream = null;

        BinaryReader reader = null;

        BinaryWriter writer = null;

        FileStream file = null;

        byte[] arrFile = null; // Containing the file in the form of Bytes Array

        Thread serverThread = new Thread(new ThreadStart(RunServer));

        public void RunServer()
        {

            BinaryFormatter Formatter = new BinaryFormatter();
            object o;
            int file_size = 0;

            try
            {
                myscoket = listener.AcceptSocket();

                server.toolStripStatusLabel1.Text = "Connect To Host That Has:" + listener.LocalEndpoint.AddressFamily.ToString();

                mynetworkStream = new NetworkStream(myscoket);

                reader = new BinaryReader(mynetworkStream);

                byte flage = reader.ReadByte();

                if (flage == 0)
                {
                    TextBox_Receive_Text_Data.Text += reader.ReadString() + "\r\n";

                }
                else if (flage == 1)
                {

                    file_size = reader.ReadInt32(); //Reading the file size from the stream

                    o = Formatter.Deserialize(mynetworkStream); // all thing is Reading after this  Statment  1

                    // 1
                    arrFile = reader.ReadBytes(file_size); // this Way is the best to improve performance 

                    TextBox_Receive.Text = Convert.ToString(arrFile.Length);

                    TextBox_ServerIp.Text = myscoket.AddressFamily.ToString();

                    //TextBox_PortNumber

                }

            }
            catch (Exception error)
            {
                //MessageBox.Show(error.Message);
            }

            if (myscoket.Connected)
            {
                while (true)
                    RunServer();
            }

        }


        public void send()
        {

             if (s1.myscoket.Connected)
            {
                try
                {
                    OpenFileDialog.ShowDialog();
                    string path = OpenFileDialog.FileName;

                    s1.writer = new BinaryWriter(s1.mynetworkStream);

                    writer.Write((byte)1); // Sending Any  File 

                    // Sending Any file to the Client
                    FileStream File = new FileStream(OpenFileDialog.FileName, FileMode.Open);

                    byte[] Buffer = new byte[File.Length];

                    //2 the same as A bove  by File Class 
                    File.Read(Buffer, 0, (int)File.Length); //fill the array with the bytes form file 

                    File.Close();

                    // Very important Class 
                    BinaryFormatter Formatter = new BinaryFormatter();

                    writer.Write(Buffer.Length); //send the size of file  

                    //1
                    Formatter.Serialize(mynetworkStream, path);// very important

                    mynetworkStream.Write(Buffer, 0, Buffer.Length);  //same as above (2)

                    TextBox_Send.Text = Buffer.Length.ToString();



                }
                catch (Exception error) { MessageBox.Show(error.Message); }
            }
            else
                MessageBox.Show("Client is Disconnected");
       }
   


    }
}
