using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Reppertum.Network
{
    public class TCPClient
    {
        private const int port = 11000; // The port number for the remote device.

        // ManualResetEvent instances signal completion.  
        private static ManualResetEvent connectDone = new ManualResetEvent(false);
        private static ManualResetEvent sendDone = new ManualResetEvent(false);
        private static ManualResetEvent receiveDone = new ManualResetEvent(false);

        private static String response = String.Empty; // The response from the remote device. 

//        public static int Main(String[] args)
//        {
//            StartClient();
//            return 0;
//        }
        
        private static void StartClient()
        {
            // Connect to a remote device.  
            try
            {
                // Establish the remote endpoint for the socket.
                IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
                IPAddress ipAddress = ipHostInfo.AddressList[0];
                IPEndPoint remoteEp = new IPEndPoint(ipAddress, port);

                Socket client = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp); // Create a TCP/IP socket.

                // Connect to the remote endpoint.  
                client.BeginConnect(remoteEp, new AsyncCallback(ConnectCallback), client);
                connectDone.WaitOne();

                // Send test data to the remote device.  
                Send(client, "This is a test<EOF>");
                sendDone.WaitOne();

                // Receive the response from the remote device.  
                Receive(client);
                receiveDone.WaitOne();
  
                Console.WriteLine("Response received : {0}", response); // Write the response to the console.

                // Release the socket.  
                client.Shutdown(SocketShutdown.Both);
                client.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private static void ConnectCallback(IAsyncResult ar)
        {
            try
            {
                Socket client = (Socket) ar.AsyncState; // Retrieve the socket from the state object. 
                client.EndConnect(ar); // Complete the connection.

                Console.WriteLine("Socket connected to {0}", client.RemoteEndPoint.ToString());

                connectDone.Set(); // Signal that the connection has been made. 
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private static void Receive(Socket client)
        {
            try
            {
                StateObject state = new StateObject(); // Create the state object. 
                state.WorkSocket = client;

                client.BeginReceive(state.ClientBuffer, 0, StateObject.ClientBufferSize, 0, new AsyncCallback(ReceiveCallback), state); // Begin receiving the data from the remote device. 
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private static void ReceiveCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the state object and the client socket from the asynchronous state object.  
                StateObject state = (StateObject) ar.AsyncState;
                Socket client = state.WorkSocket;

                int bytesRead = client.EndReceive(ar); // Read data from the remote device.

                if (bytesRead > 0)
                {
                    state.Sb.Append(Encoding.ASCII.GetString(state.ClientBuffer, 0, bytesRead)); // There might be more data, so store the data received so far.  
                    client.BeginReceive(state.ClientBuffer, 0, StateObject.ClientBufferSize, 0, new AsyncCallback(ReceiveCallback), state); // Get the rest of the data.
                }
                else
                {
                    // All the data has arrived; put it in response.  
                    if (state.Sb.Length > 1)
                    {
                        response = state.Sb.ToString();
                    }

                    receiveDone.Set(); // Signal that all bytes have been received.
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private static void Send(Socket client, String data)
        {
            byte[] byteData = Encoding.ASCII.GetBytes(data); // Convert the string data to byte data using ASCII encoding.
            client.BeginSend(byteData, 0, byteData.Length, 0, new AsyncCallback(SendCallback), client); // Begin sending the data to the remote device.
        }

        private static void SendCallback(IAsyncResult ar)
        {
            try
            {
                Socket client = (Socket) ar.AsyncState; // Retrieve the socket from the state object.
                int bytesSent = client.EndSend(ar); // Complete sending the data to the remote device.
                
                Console.WriteLine("Sent {0} bytes to server.", bytesSent);

                sendDone.Set(); // Signal that all bytes have been sent.
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}