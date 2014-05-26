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
using System.Runtime.Serialization;

using System.Security.Cryptography.X509Certificates;

using System.Security.Cryptography;

namespace Files_transfeer_server
{

    /// <summary>
    ///  to build application As Server we have to :
    ///  1- object form Tcplistener(Class) 
    ///  2- object from Socket(Class) that takes object from Tcplistener By AcceptSocket() method ; 
    ///  3- object from Networkstream that tackes object from socket class 
    ///  4- two  object from streamreader and streamwriter  Or  BinaryReader and BinaryWritter ; each of this take object from newrorkstram(Class)    /// 
    /// </summary>
    class Handle_Request
    {

        #region DataMember
            
            // TcpListener is Represnet The Socket in Server  that Open Connection at  Selected  Port  and Listen to Clients Request 
            TcpListener listener;

            //Socket is Port and IP  : (Ex: mysocket=new TCplistener().Acceptsocket(); )
            Socket myscoket;//Socket is used to Link  Transport layer(TCPlistener OR TCPClient) With Network layer(NetworkStream)  

            NetworkStream mynetworkStream;// Encapsulate object from Socket Class(mysockets) 

            BinaryReader reader;// Encapsulate object from Networkstream(mynetworkStream)  and  Reading from Network  

            BinaryWriter writer;// Encapsulate object from Networkstream(mynetworkStream)  and  Writing on Network 

        
            byte[] arrFile; // Containing the file in the form of Bytes Array

            EndPoint remoteEndPoint; //Information About Remote EndPoint

            Thread serverThread = null; // // to Run this Process With anthor processes 

            // key=ClientName   Value=ip:port                           
           public static Dictionary<string, string> Client_list = new Dictionary<string, string>(); //new

           // key=Client name   Value=Public Key      
           public static Dictionary<string, string> Clients_public_keys = new Dictionary<string, string>();

               // key=Client name   Value=room name      
               public static Dictionary<string, string> Clients_rooms = new Dictionary<string, string>();    


           public  AesCryptoServiceProvider aescsp = new AesCryptoServiceProvider();


           
           public string Client_name;


        #endregion


        #region Properties
            public TcpListener Listener
            {
                get { return listener; }
                set { listener = value; }
            }

            public Socket Myscoket
            {
                get { return myscoket; }
                set { myscoket = value; }
            }

            public NetworkStream MynetworkStream
            {
                get { return mynetworkStream; }
                set { mynetworkStream = value; }
            }

            public BinaryReader Reader
            {
                get { return reader; }
                set { reader = value; }
            }

            public BinaryWriter Writer
            {
                get { return writer; }
                set { writer = value; }
            }

            public byte[] ArrFile
            {
                get { return arrFile; }
                set { arrFile = value; }
            }

            public EndPoint RemoteEndPoint
            {
                get { return remoteEndPoint; }
                set { remoteEndPoint = value; }
            }

            public Thread ServerThread
            {
                get { return serverThread; }
                set { serverThread = value; }
            }

        #endregion


        #region   Handle_Request  Constuctor
        /// <summary>
        /// Costructor 
        /// </summary>
        /// <param name="port"> port number is Adderss of this  Aplication ; must be Uniq</param>
        /// <param name="listener"> </param>
        /// <param name="sock"> sock is Socket between Server And Client After the Server Accept the Clien Requset</param>
        public Handle_Request(TcpListener listener , Socket sock)
        {
            this.listener = listener;

            myscoket = sock;

            serverThread= new Thread(new ThreadStart(RunServer));
        }
        #endregion 


        #region public void start()
        /// <summary>
        /// Start Handle_Requset thread 
        /// </summary>
        public void start()
        {
            try
            {
                serverThread.Start();
            }
            catch (Exception ee) { MessageBox.Show(ee.Message); }
        }
        #endregion


        #region public void RunServer()
        /// <summary>
        /// The Most important method in this Class ;
        /// this Method is invoked by serverThread 
        /// </summary>
        public void RunServer()
        {
            int file_size = 0;

            BinaryFormatter bf = new BinaryFormatter();                              

            try
            {
                // myscoket = listener.AcceptSocket(); //  Active in ThreadPool 

                remoteEndPoint = listener.LocalEndpoint;

                sever.label_client_info.Text = "Connect To Host That Has:" + myscoket.RemoteEndPoint.ToString(); //end poin is ip and port 

                mynetworkStream = new NetworkStream(myscoket);

                reader = new BinaryReader(mynetworkStream);

                writer = new BinaryWriter(mynetworkStream);

                bf.Serialize(mynetworkStream, Files_transfeer_server.sever.Rooms);

                //new Code for Log in  
               Client_name = reader.ReadString(); //Read Client Name

               string client_publicKey = reader.ReadString(); //Read Public Key 

               string client_room = reader.ReadString(); //Read Client Room

              // MessageBox.Show(client_room);




              /*
                
                // Authentication Code  Start 

               Random Rand = new Random(DateTime.Now.TimeOfDay.Milliseconds);

               int R = Rand.Next();

                writer.Write(R);  //Server Send Random Number to Client


                //Calculate Hash Function value (MD5)

                byte [] Calculated_hash=getMD5MAC(client_publicKey, R);

                // Recieve Hash Function value (MD5)

                int size_of_data= Reader.ReadInt32();

                byte[] Recieved_Hash = reader.ReadBytes(size_of_data);

                bool is_successed = Compare_2_Arrays(Calculated_hash, Recieved_Hash);

                if (!is_successed)
                {
                    MessageBox.Show("Authentication Faild");
                    return;
                }

                //  Authentication Code  End

               * */


                //PGP and CA

                // PGP is A Key Exchange Algorithm , (between two Clients or Client and Server)

             /*
                byte [] cert_file=File.ReadAllBytes((@"D:\Keys\Server_Certifiacte"));

                RSACryptoServiceProvider Server_RSA = new RSACryptoServiceProvider();

                StreamReader r = new StreamReader(@"D:\Keys\Server_pv_key");

                Server_RSA.FromXmlString(r.ReadToEnd()); // Get Private Key from File

                r.Close();

                RSAParameters keys = Server_RSA.ExportParameters(true); //Public and Private Key

                string server_PK = Server_RSA.ToXmlString(false);


                writer.Write(cert_file.Length); // Send Certificate file size 

                writer.Write(cert_file); // Send Certificate file 
                */
                
               //new code
               RSACryptoServiceProvider Server_RSA = new RSACryptoServiceProvider();

               string server_PK = Server_RSA.ToXmlString(false);
                
                writer.Write(server_PK); //server send its PK  to Client

                //new code

                int key_size =reader.ReadInt32();

                byte [] AES_key = reader.ReadBytes(key_size);

                int IV_size = reader.ReadInt32();

                byte[] AES_IV = reader.ReadBytes(IV_size);

                aescsp.Key = Server_RSA.Decrypt(AES_key, false); // Decrypt With Private Key 

                aescsp.IV = Server_RSA.Decrypt(AES_IV, false); // Decrypt With Private Key 




                //Degital Signature                 
                
               /* string message = "Hello Client I am server";

                //1- Encrypt Message With AES Secret Key
                byte[] en_message = AES_Encryption.EncryptString(aescsp, message);

                //2- compute hash and then Encrypt the result with server Private key
                byte [] signature=Server_RSA.SignData(en_message, new SHA1CryptoServiceProvider());


                //3-Now Server send en_message and signature value

                writer.Write(en_message.Length); //Send Size of En_message

                writer.Write(en_message); //Send En_message

                writer.Write(signature.Length); //Send Size of signature

                writer.Write(signature); //Send signature
                */

                           
                lock (Client_list)
                {

                    Client_list.Add(Client_name, myscoket.RemoteEndPoint.ToString());

                    bf.Serialize(mynetworkStream, Client_list);
                }

                lock (Clients_public_keys)
                {
                    Clients_public_keys.Add(Client_name, client_publicKey);

                    bf.Serialize(mynetworkStream, Clients_public_keys);
                }

                lock (Clients_rooms)
                {
                    Clients_rooms.Add(Client_name, client_room);

                    bf.Serialize(mynetworkStream, Clients_rooms);
                }





                if (Client_list.Count > 1)
                {
                    Socket[] Clients_arr = sever.clientsList.Values.ToArray();

                    NetworkStream mynetworkstream1;

                    for (int i = 0; i < Clients_arr.Length; i++)
                    {

                        if ( (Clients_arr[i] != null) && (Clients_arr[i].Connected) )
                        {

                            mynetworkstream1 = new NetworkStream(Clients_arr[i]);

                            Writer = new BinaryWriter(mynetworkstream1);

                            writer.Write((byte)20);

                            bf.Serialize(mynetworkstream1, Client_list);     // 1

                            bf.Serialize(mynetworkstream1, Clients_public_keys); //2

                            bf.Serialize(mynetworkstream1, Clients_rooms); //3
                        }
                    }
                }
             
                while (myscoket.Connected)
                {
                    byte flage = reader.ReadByte();

                    if (flage == 0) //Receive Text Data 
                    {
                        string AES_Mode = reader.ReadString(); // CBC,CTS,OFB,...

                        AES_Encryption.ChangeMode(AES_Mode, aescsp);

                        int size = reader.ReadInt32();

                        byte[] encQuote = reader.ReadBytes(size);

                        string original_message = AES_Encryption.DecryptBytes(aescsp, encQuote);

                        sever.user_TextBox_Receive_Text_Data.Text += original_message+"\r\n";
                    }
                    else if (flage == 1) //Receive File
                    {
                        file_size = reader.ReadInt32(); //Reading the file size from the stream
                        // 1
                        arrFile = reader.ReadBytes(file_size); // this Way is not the best to improve performance 

                        sever.user_TextBox_Receive.Text = arrFile.Length.ToString();
                    }
                    else if (flage == 2)// Client Want To Send File To Selected Client
                    {
                        string ip = reader.ReadString(); // Receive Client  IP  To Send to it 

                        // if Client is EXIST and Conncected To the Server Send File To it 

                        if ((sever.clientsList[ip] != null) && (sever.clientsList[ip].Connected))
                        {

                            string AES_Mode = reader.ReadString(); // CBC,CTS,OFB,...

                            string clientName = reader.ReadString();

                            //new code 
                            string senderclientName = reader.ReadString();


                            file_size = reader.ReadInt32(); //Reading the file size from the stream

                            // 1
                            byte[] temp_arrFile = reader.ReadBytes(file_size); // this Way is the best to improve performance 


                            int signature_size = reader.ReadInt32();

                            byte[] File_signature = reader.ReadBytes(signature_size);



                            //----------Send File to Client--------------------------

                            NetworkStream mynetworkstream1 = new NetworkStream(sever.clientsList[ip]);

                            try
                            {


                                string Destenation_Pk = Clients_public_keys[clientName];

                                RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();

                                rsa.FromXmlString(Destenation_Pk);

                                byte[] en_key = rsa.Encrypt(aescsp.Key, false); //Encrypt AES Key with RSA

                                byte[] en_iv = rsa.Encrypt(aescsp.IV, false);


                                Writer = new BinaryWriter(mynetworkstream1);

                                Writer.Write((byte)1); // Sending Any  File 

                                writer.Write("AES"); // Send Tag

                                Writer.Write(AES_Mode); // Send AES Mode


                                Writer.Write((byte)1); //Sending Tag2=1 means that the message is from client to another 

                                writer.Write(en_key.Length); //send en_key size

                                writer.Write(en_key); //send en_key 

                                writer.Write(en_iv.Length); //send en_IV size

                                writer.Write(en_iv); //send en_IV  


                                Writer.Write(temp_arrFile.Length); //send the size of file  

                                //Writing Data
                                mynetworkstream1.Write(temp_arrFile, 0, temp_arrFile.Length);

                                //Send Signature of the File
                                writer.Write(File_signature.Length);

                                writer.Write(File_signature);

                                writer.Write(senderclientName);


                                temp_arrFile = null; //
                            }
                            catch (Exception err)
                            {
                                MessageBox.Show(err.Message);
                            }

                        }//End If

                    }//End Else if
                    else if (flage == 3) //Client Want To Send TextData To Selected Client
                    {
                        string ip = reader.ReadString(); //First : Read IP Address 

                        string Tag = reader.ReadString(); // Tag is Transfeer Mode

                        if ( (sever.clientsList[ip] != null) && (sever.clientsList[ip].Connected) )
                        {

                            NetworkStream mynetworkstream1 = new NetworkStream(sever.clientsList[ip]);

                            Writer = new BinaryWriter(mynetworkstream1);

                            if (Tag == "No_Encryption")
                            {

                                string s = reader.ReadString(); // Second: Read TextData from Client 

                                //----------Send Text Data to Client--------------------------

                                Writer.Write((byte)0); //Sending Text Data 

                                Writer.Write("No_Encryption"); // Tag

                                Writer.Write(s);

                            }
                            else if (Tag == "AES")
                            {
                                string AES_Mode = reader.ReadString(); // CBC,CTS,OFB,...

                                string clientName = reader.ReadString();

                                int size_of_Data=reader.ReadInt32();

                                byte[] buff = reader.ReadBytes(size_of_Data);

                                //read hash vallue from client 
                                int size_of_hash=reader.ReadInt32();

                                byte[] recived_hash = reader.ReadBytes(size_of_hash);

                               
                                string Destenation_Pk = Clients_public_keys[clientName];

                                RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();

                                rsa.FromXmlString(Destenation_Pk);

                                byte[] en_key = rsa.Encrypt(aescsp.Key, false); //Encrypt AES Key with RSA

                                byte[] en_iv =  rsa.Encrypt(aescsp.IV, false);


                                Writer.Write((byte)0); //Sending Text Data 

                                Writer.Write("AES"); // Tag

                                writer.Write(AES_Mode);//Send AES_Mode

                                Writer.Write((byte)1); //Sending Tag2=1 means that the message is from client to another 

                                writer.Write(en_key.Length); //send en_key size

                                writer.Write(en_key); //send en_key 

                                writer.Write(en_iv.Length); //send en_IV size

                                writer.Write(en_iv); //send en_IV  



                                Writer.Write(buff.Length); // Send  size of Data

                                Writer.Write(buff); // Send Encrypted Data

                                //write received hash from client  and send it to client 
                                Writer.Write(recived_hash.Length); // Send  size of Data

                                Writer.Write(recived_hash); // Send Encrypted Data


                            }
                            else if (Tag =="RSA")
                            {

                                int size_of_Data = reader.ReadInt32();

                                byte[] buff = reader.ReadBytes(size_of_Data);


                                Writer.Write((byte)0); //Sending Text Data 

                                Writer.Write("RSA"); // Tag

                                Writer.Write(buff.Length); // Send  size of Data

                                Writer.Write(buff); // Send Encrypted Data
                            }
                        }
                    }


                }//end While 

            }
            catch (Exception error)
            {

                 //MessageBox.Show(error.Message);
            }
            finally
            {

                if (Client_name != null)
                {
                    string k = Client_list[Client_name];

                    Clients_public_keys.Remove(k);

                    sever.clientsList.Remove(k);

                    Client_list.Remove(Client_name);

                    if (Clients_rooms != null)
                        Clients_rooms.Remove(k);

                }
                    Socket[] Clients_arr = sever.clientsList.Values.ToArray();


                    NetworkStream mynetworkstream1;


                    for (int i = 0; i < Clients_arr.Length; i++)
                    {

                        try
                        {
                            if ((Clients_arr[i] != null) && (Clients_arr[i].Connected))
                            {
                                mynetworkstream1 = new NetworkStream(Clients_arr[i]);

                                Writer = new BinaryWriter(mynetworkstream1);

                                writer.Write((byte)20);

                                bf.Serialize(mynetworkstream1, Client_list);

                                bf.Serialize(mynetworkstream1, Clients_public_keys);

                                bf.Serialize(mynetworkstream1, Clients_rooms);

                            }
                        }
                        catch (Exception ex) { MessageBox.Show("finally "+ex.Message); }
                    }
                
            }

            myscoket.Close();

            mynetworkStream.Close();
        }

        #endregion 



        private byte [] getMD5MAC(string key, int Randomvalue)
        {

            UnicodeEncoding encoding = new UnicodeEncoding();

            byte[] keyByte = encoding.GetBytes(key);

            HMACMD5 hmacmd5 = new HMACMD5(keyByte);

            byte[] RandomMessageeBytes = BitConverter.GetBytes(Randomvalue);

            byte[] Hash_Result = hmacmd5.ComputeHash(RandomMessageeBytes);

            return Hash_Result;
        }

        private bool Compare_2_Arrays(byte[] arr1, byte[] arr2)
        {

            for (int i = 0; i < arr1.Length; i++)
                if (arr1[i] != arr2[i])
                    return false;

            return true;
        }



    }


}
