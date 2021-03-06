using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System;

public class Client : MonoBehaviour
{
    public static Client instance;
    public static int dataBufferSize = 4096;
    public string ip = Dns.Resolve("localhost").AddressList[0].ToString();
    public int port= 26950;
    public int myId=1;
    public TCP tcp;

    private void Awaje(){
        if(instance==null){
            instance=this;
        }else if(instance !=null){
            Destroy(this);
        }
    }

    private void Start(){
        tcp= new TCP();
    }
    public void ConnectToServer(){
        tcp.Connect();
    }

    public class TCP{
        public TcpClient socket;
        private NetworkStream stream;
        private byte[] receiveBuffer;

        public void Connect(){
        socket = new TcpClient{
            ReceiveBufferSize = dataBufferSize,
            SendBufferSize= dataBufferSize

        };
        receiveBuffer= new Byte[dataBufferSize];
        socket.BeginConnect(instance.ip, instance.port, ConnectCallback,socket);

    }

    private void ConnectCallback(IAsyncResult _result){
        if( !socket.Connected){
            return;
        }
        stream = socket.GetStream();
        stream.BeginRead(receiveBuffer,0,dataBufferSize,receiveCallback,null);  

    }

    private void receiveCallback(IAsyncResult _reslut)
            {
                try
                {
                    int byteLength = stream.EndRead(_reslut);
                    if (byteLength <= 0) {
                        return;
                    }
                    byte[] data = new byte[byteLength];
                    Array.Copy(receiveBuffer, data, byteLength);
                    stream.BeginRead(receiveBuffer, 0, dataBufferSize, receiveCallback, null);


                }
                catch (Exception _ex) {
                    Console.WriteLine("bla bla ");
                }
            }
    }

    

    


    
}
