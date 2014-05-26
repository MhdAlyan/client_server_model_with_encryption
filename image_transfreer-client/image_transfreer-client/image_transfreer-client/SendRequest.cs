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

using Files_transfreer_client;
using System.Security.Cryptography;
using System.Reflection;

using CerteficateAuthority;

namespace Files_transfreer_client
{

    /// <summary>
    /// Send Requset To The Server To Connect With it By the Folowing Way , you neeed to:
    /// 1- Object Form TcpClient to connect to the Server that Have IP and Port number ;client = new TcpClient(server_ip,port_number);
    /// 2- reference form Networkstream and Get the stram from TcpClient Object ; mynetworkStream = client.GetStream();
    /// 3- Two  object from BinaryReader and BinaryWriter (That Encapsulate the mynetworkstream )To Read and Write on Network
    /// </summary>
    class SendRequest
    {

        #region DataMembers
        
            TcpClient client = null; // if the Name is Client it will be Wrong Because the Client is The Class Name 

            NetworkStream mynetworkStream;

            BinaryReader reader;

            BinaryWriter writer;

            byte[] arrFile;

            Thread readThread;

            int requestStatus = 1; // for Disconnecting the Connection

            int Client_count = 0;

            AesCryptoServiceProvider aescsp = new AesCryptoServiceProvider();

            public  RSACryptoServiceProvider Rsa = new RSACryptoServiceProvider(); // Create Public/Private Keys

            string remote_client_name = null;

            

            

            public  string client_publicKey = null;

            public  string client_privateKey = null;
            
            


        #endregion 


        #region Properties
            public TcpClient Client
            {
                get { return client; }
                set { client = value; }
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

            public Thread ReadThread
            {
                get { return readThread; }
                set { readThread = value; }
            }

            public int RequestStatus
            {
                get { return requestStatus; }
                set { requestStatus = value; }
            }



        #endregion


        #region SendRequset Constuctor 
        /// <summary>
        /// Constructor
        /// </summary>
        public SendRequest()
        {

            aescsp.Key = Files_transfreer_client.Client.aescsp.Key;

            aescsp.IV = Files_transfreer_client.Client.aescsp.IV;


            readThread = new Thread(new ThreadStart(Run_client));

        }
        #endregion 


        #region public void start()
        /// <summary>
        /// Start Sending Request
        /// </summary>
        public void start()
        {
            try
            {
                readThread.Start();
            }
            catch (Exception err) { MessageBox.Show(err.Message); }

        }
        #endregion


        #region public void AbortRequset()
        /// <summary>
        /// Abort Sending and Receiving Thread 
        /// </summary>
        public void AbortRequset()
        {
            try
            {
                readThread.Abort();
            }
            catch (Exception err) { MessageBox.Show(err.Message); }

        }
        #endregion





        #region public void Run_client()
        /// <summary>
        /// Connecting With Server ,and Receiving Data From it 
        /// </summary>
        public void Run_client()
        {

            try
            {
                if (requestStatus == 1) // if the ueser presses on the Disconnect Button  then requestStatus=0 
                {
                    if (client != null)
                    {
                        if (!(client.Connected))
                            client.Connect(Files_transfreer_client.Client.user_TextBox_ServerIP.Text, Convert.ToInt32(Files_transfreer_client.Client.user_TextBox_portnmber.Text));
                    }
                    else
                        client = new TcpClient(Files_transfreer_client.Client.user_TextBox_ServerIP.Text, Convert.ToInt32(Files_transfreer_client.Client.user_TextBox_portnmber.Text));

                    Files_transfreer_client.Client.label_Server_info.Text = "Connect To Host That Has: " + client.Client.RemoteEndPoint.ToString();

                    mynetworkStream = client.GetStream();

                    reader = new BinaryReader(mynetworkStream);

                    writer = new BinaryWriter(mynetworkStream);

                    BinaryFormatter bf = new BinaryFormatter();
               
                    Files_transfreer_client.Client.Rooms = (List<string>)bf.Deserialize(mynetworkStream);

                    Files_transfreer_client.Client.update_rooms();// Fill
                                                            
                    readThread.Suspend();
                   

                    //new Code  for Log in  and negotiation for AES Key

                    writer.Write(Files_transfreer_client.Client.user_TextBox_ClientName.Text); //  Send Client name                    

                    RSAParameters param = Rsa.ExportParameters(true);

                    client_publicKey = Rsa.ToXmlString(false); // Get Public Key

                    client_privateKey = Rsa.ToXmlString(true); // Get private Key

                    writer.Write(client_publicKey); // Send Client Public Key

                    writer.Write(Files_transfreer_client.Client.compobox_Clientrooms.Text); //  Send Client room to join 

                   //HMAC Authentication Code start

                   /* int R= reader.ReadInt32();

                    //Calculate Hash Function value (MD5)

                    byte[] Calculated_hash = getMD5MAC(client_publicKey, R);


                    writer.Write(Calculated_hash.Length);//Send Size of Hash Value 

                    writer.Write(Calculated_hash); //Send Hash 

                    */
                    //HMAC Authentication Code

                    //------------------------------------------------------



                    // PGP and CA

                    // PGP is A Key Exchange Algorithm , (between two Clients or Client and Server)

                    // this is My Approach 


                    RSACryptoServiceProvider Client_RSA = new RSACryptoServiceProvider();

                    
                 /*   RSACryptoServiceProvider Client_RSA = new RSACryptoServiceProvider();

                    int cert_size = reader.ReadInt32(); // Client receive  Server Certificate 

                    MemoryStream mem = new MemoryStream(reader.ReadBytes(cert_size));

                     //bf = new BinaryFormatter();


                    //Assembly.LoadFrom(

                    CerteficateAuthority.X509Certificate server_cert =(CerteficateAuthority.X509Certificate)bf.Deserialize(mem);

                    
                    StreamReader Sreader = new StreamReader(@"D:\Keys\CA_pu_key");

                    string CA_public_key = Sreader.ReadToEnd();
                                      
                    mem.Close();

                    using (RSACryptoServiceProvider rsa1 = new RSACryptoServiceProvider())
                    {
                        rsa1.FromXmlString(CA_public_key);

                        bool ok = false;
                        if (server_cert.SignatureAlgorithm == "RSA&SH1")
                            ok = rsa1.VerifyData(server_cert.ToByteArray(), new SHA1CryptoServiceProvider(), server_cert.CA_Signature);
                        else if (server_cert.SignatureAlgorithm == "RSA&MD5")
                            ok = rsa1.VerifyData(server_cert.ToByteArray(), new MD5CryptoServiceProvider(), server_cert.CA_Signature);

                        if (!ok)
                        {
                            MessageBox.Show("verification failed");

                            return;
                        }
                        else
                           MessageBox.Show("verification successful ");

                    }
                   
                   string server_PK = server_cert.PublicKey; // Get Server public Key form it is own certifiacte
              
                    */


                    string server_PK = reader.ReadString(); // Client Recieve server PK 

                    Client_RSA.FromXmlString(server_PK);

                    byte[] en_key = Client_RSA.Encrypt(aescsp.Key,false);

                    byte[] en_IV = Client_RSA.Encrypt(aescsp.IV, false);

                    writer.Write(en_key.Length); //send en_key size

                    writer.Write(en_key); //send en_key 

                    writer.Write(en_IV.Length); //send en_IV size

                    writer.Write(en_IV); //send en_IV  


                    //Client Recieve Signed Data

                    /*int size_of_data = reader.ReadInt32();

                    byte[] en_message = reader.ReadBytes(size_of_data);

                    int signature_size = reader.ReadInt32();

                    byte[] signature = reader.ReadBytes(signature_size);


                    string m = AES_Encryption.DecryptBytes(aescsp, en_message); 

                    bool OK = Client_RSA.VerifyData(en_message, new SHA1CryptoServiceProvider(), signature);


                    if (!OK)
                    {
                        MessageBox.Show(m);

                        MessageBox.Show("Digital Signature Failed");
                    }

                 */
                                              
                    //DSerialize two Dictionaries
                     bf = new BinaryFormatter();

                    //1
                    Files_transfreer_client.Client.Client_list=(Dictionary<string,string>)bf.Deserialize(mynetworkStream);

                    //2
                    Files_transfreer_client.Client.Clients_public_keys = (Dictionary<string,string>)bf.Deserialize(mynetworkStream);

                    //3
                    Files_transfreer_client.Client.Clients_rooms = (Dictionary<string, string>)bf.Deserialize(mynetworkStream);



                    Files_transfreer_client.Client.update_ClientList();// Fill


                    AesCryptoServiceProvider aescsp_text_from_client = new AesCryptoServiceProvider();

                    AesCryptoServiceProvider aescsp_file_from_client = new AesCryptoServiceProvider();

           
                    while (client.Connected)//Create Session 
                    {

                        byte flag = reader.ReadByte();

    
                        if (flag == 0)
                        {
                                string Tag = Reader.ReadString();

                                string message ="";

                                if (Tag == "No_Encryption")
                                    message= reader.ReadString();

                                else if(Tag == "AES")
                                {
                                    string AES_Mode = reader.ReadString(); // CBC,CTS,OFB,...

                                    byte tag2 = reader.ReadByte(); //0 ==from Server ,  1 from Client

                                    if (tag2 == 1)
                                    {
                                        int key_size = reader.ReadInt32();

                                        byte[] AES_key = reader.ReadBytes(key_size);

                                        int IV_size = reader.ReadInt32();

                                        byte[] AES_IV = reader.ReadBytes(IV_size);


                                        Rsa.ImportParameters(param); //Set Parameters Algorithm 


                                        aescsp_text_from_client.Key = Rsa.Decrypt(AES_key, false); // Decrypt With Private Key 

                                        aescsp_text_from_client.IV = Rsa.Decrypt(AES_IV, false); // Decrypt With Private Key 

                                        int size_of_Data = reader.ReadInt32();

                                        byte[] buff = reader.ReadBytes(size_of_Data);

                                        AES_Encryption.ChangeMode(AES_Mode, aescsp_text_from_client);

                                     

                                        //client recive hash and compare recive hash and calculated hash 
                                        
                                        
                                        //read hash vallue from server  
                                        int size_of_hash = reader.ReadInt32();

                                        byte[] recived_hash = reader.ReadBytes(size_of_hash);



                                        //calculate hash and  compare with recived 
                                        MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();

                                        byte[] ComputedHash = md5.ComputeHash(buff);

                                        if (Compare_2_Arrays(recived_hash, ComputedHash))
                                        {

                                            MessageBox.Show("message integity check is ok ");
                                        }
                                        else
                                        {
                                            MessageBox.Show("message integity check is not  ok ; message  was  alterd  ");

                                            return;
                                        }
                                        
                                        message = AES_Encryption.DecryptBytes(aescsp_text_from_client, buff);
                                    }
                                    else
                                    {

                                        int size_of_Data = reader.ReadInt32();

                                        byte[] buff = reader.ReadBytes(size_of_Data);

                                        AES_Encryption.ChangeMode(AES_Mode, aescsp);

                                        message = AES_Encryption.DecryptBytes(aescsp, buff);

                                    }

                                }

                                else if (Tag =="RSA")
                                {

                                    int size_of_Data = reader.ReadInt32();

                                    byte[] en_buff = reader.ReadBytes(size_of_Data);

                                     // Public/Private Keys for Decryption 

                                    Rsa.ImportParameters(param); //Set Parameters Algorithm 

                                    byte[] buffer=Rsa.Decrypt(en_buff, false);

                                    message = UTF8Encoding.Unicode.GetString(buffer);

                                }

                                Files_transfreer_client.Client.user_TextBox_Receive_Text_Data.Text += message + "\r\n";

                        }
                        else if (flag == 1)
                        {
                            string Tag = Reader.ReadString();// Tag="No_Encryption" , or Tag="AES"

                            if (Tag == "AES")
                            {
                                string AES_Mode = reader.ReadString(); // CBC,CTS,OFB,...

                                byte tag2 = reader.ReadByte(); //0 ==from Server ,  1 from Client

                                int size_of_Data = 0;

                                if (tag2 == 1)
                                {
                                    int key_size = reader.ReadInt32();

                                    byte[] AES_key = reader.ReadBytes(key_size);

                                    int IV_size = reader.ReadInt32();

                                    byte[] AES_IV = reader.ReadBytes(IV_size);


                                    Rsa.ImportParameters(param); //Set Parameters Algorithm 


                                    aescsp_file_from_client.Key = Rsa.Decrypt(AES_key, false); // Decrypt With Private Key 

                                    aescsp_file_from_client.IV = Rsa.Decrypt(AES_IV, false); // Decrypt With Private Key 

                                    size_of_Data = reader.ReadInt32();

                                    byte [] en_arrFile = reader.ReadBytes(size_of_Data);

                                    AES_Encryption.ChangeMode(AES_Mode, aescsp_file_from_client);

                                    arrFile = AES_Encryption.DecryptFileBytes(aescsp_file_from_client, en_arrFile);


                                   int signature_size = reader.ReadInt32();

                                    byte[] File_signature = reader.ReadBytes(signature_size);

                                    //new code 
                                  //  string senderclientName = reader.ReadString();


                                    string remote_client_name = reader.ReadString();

                                    //remote_client_name = Files_transfreer_client.Client.combobox_ClientsNames.Text;

                                    string PK = Files_transfreer_client.Client.Clients_public_keys[remote_client_name];

                                    Rsa.FromXmlString(PK);



                                    if (!Rsa.VerifyData(en_arrFile, new SHA1CryptoServiceProvider(), File_signature))
                                        MessageBox.Show("signature verification failed");
                                    else
                                        MessageBox.Show("signature verification ok");


                                }
                            }
                            else
                            {

                                int file_size = reader.ReadInt32(); //Reading the file size from the stream

                                arrFile = reader.ReadBytes(file_size); // this Way is not the best to improve performance 

                            }



                            Files_transfreer_client.Client.user_TextBox_Receive.Text = Convert.ToString(arrFile.Length);
                        }

                        else if (flag == 20)//20 means That new Client was logged in or a specific Client Was logged of 
                        {
                            
                            //1
                            Files_transfreer_client.Client.Client_list = (Dictionary<string, string>)bf.Deserialize(mynetworkStream);

                            //2
                            Files_transfreer_client.Client.Clients_public_keys = (Dictionary<string,string>)bf.Deserialize(mynetworkStream);

                            //3
                            Files_transfreer_client.Client.Clients_rooms = (Dictionary<string, string>)bf.Deserialize(mynetworkStream);  


                            Files_transfreer_client.Client.update_ClientList();// Fill                            
                        }

                    }//End While ; End Session 
                }

            }
            catch (Exception ee)
            {

                //MessageBox.Show("1");

                //MessageBox.Show(ee.Message);

                //MessageBox.Show(ee.StackTrace);

            }
        }
        #endregion



        private byte[] getMD5MAC(string key, int Randomvalue)
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
