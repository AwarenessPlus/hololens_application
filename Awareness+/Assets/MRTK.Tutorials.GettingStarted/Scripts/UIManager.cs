using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using System;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

public class UIManager : MonoBehaviour
{

    
    public static UIManager instance;
    public GameObject startMenu;
    public InputField usernameFlied;


    void Start(){
        Connect();
    } 
    private void Awaje(){
        if(instance==null){
            instance=this;
        }else if(instance !=null){
            Destroy(this);
        }
    }

     public static void Connect()
        {
            string server = "10.195.22.30";
            string message= "Hola desde las hololens";
            try
            {
                // Create a TcpClient.
                // Note, for this client to work you need to have a TcpServer
                // connected to the same address as specified by the server, port
                // combination.
                Int32 port = 13000;
                TcpClient client = new TcpClient(server, port);

                // Translate the passed message into ASCII and store it as a Byte array.
                Byte[] data = System.Text.Encoding.ASCII.GetBytes(message);

                // Get a client stream for reading and writing.
                //  Stream stream = client.GetStream();

                NetworkStream stream = client.GetStream();

                // Send the message to the connected TcpServer.
                stream.Write(data, 0, data.Length);

               Debug.Log("Sent: "+message);

                // Receive the TcpServer.response.

                // Buffer to store the response bytes.
                data = new Byte[256];

                // String to store the response ASCII representation.
                string responseData = String.Empty;

                // Read the first batch of the TcpServer response bytes.
                Int32 bytes = stream.Read(data, 0, data.Length);
                responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                Debug.Log("Received:"+responseData);

                // Close everything.
                stream.Close();
                client.Close();
            }
            catch (ArgumentNullException e)
            {
                Debug.Log("ArgumentNullException:"+e );
            }
            catch (SocketException e)
            {
               Debug.Log("SocketException:"+e);
            }

            Debug.Log("\n Press Enter to continue...");
           // Console.Read();
        }
    
}
