using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Reppertum.Network
{
    public class TCPListener
    {
        public static ManualResetEvent allDone = new ManualResetEvent(false); // Thread signal. 

//        public static int Main(String[] args)
//        {
//            StartListening();
//            return 0;
//        }
        
        public static void StartListening()
        {
            byte[] bytes = new Byte[1024]; // Data buffer for incoming data.

            // Establish the local endpoint for the socket.
            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[0];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11000);

            Socket listener = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp); // Create a TCP/IP socket.

            // Bind the socket to the local endpoint and listen for incoming connections.  
            try
            {
                listener.Bind(localEndPoint);
                listener.Listen(100);

                while (true)
                {
                    allDone.Reset(); // Set the event to nonsignaled state.

                    // Start an asynchronous socket to listen for connections.  
                    Console.WriteLine("Waiting for a connection...");
                    listener.BeginAccept(new AsyncCallback(AcceptCallback), listener);

                    allDone.WaitOne(); // Wait until a connection is made before continuing.  
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            Console.WriteLine("\nPress ENTER to continue...");
            Console.Read();
        }

        public static void AcceptCallback(IAsyncResult ar)
        {
            allDone.Set(); // Signal the main thread to continue.

            // Get the socket that handles the client request.  
            Socket listener = (Socket) ar.AsyncState;
            Socket handler = listener.EndAccept(ar);

            StateObject state = new StateObject(); // Create the state object. 
            state.WorkSocket = handler;
            handler.BeginReceive(state.ServerBuffer, 0, StateObject.ServerBufferSize, 0, new AsyncCallback(ReadCallback), state);
        }

        public static void ReadCallback(IAsyncResult ar)
        {
            String content = String.Empty;

            // Retrieve the state object and the handler socket from the asynchronous state object.
            StateObject state = (StateObject) ar.AsyncState;
            Socket handler = state.WorkSocket;

            int bytesRead = handler.EndReceive(ar); // Read data from the client socket.

            if (bytesRead > 0)
            {  
                state.Sb.Append(Encoding.ASCII.GetString(state.ServerBuffer, 0, bytesRead)); // There  might be more data, so store the data received so far.

                // Check for end-of-file tag. If it is not there, read more data.
                content = state.Sb.ToString();
                if (content.IndexOf("<EOF>") > -1)
                {
                    Console.WriteLine("Read {0} bytes from socket. \n Data : {1}", content.Length, content.Substring(0, content.Length-5)); // All the data has been read from the client. Display it on the console.
                    Send(handler, "Server successfully received: " + content); // Echo the data back to the client.
                }
                else
                {
                    handler.BeginReceive(state.ServerBuffer, 0, StateObject.ServerBufferSize, 0, new AsyncCallback(ReadCallback), state); // Not all data received. Get more. 
                }
            }
        }

        private static void Send(Socket handler, String data)
        {
            byte[] byteData = Encoding.ASCII.GetBytes(data); // Convert the string data to byte data using ASCII encoding.
            handler.BeginSend(byteData, 0, byteData.Length, 0, new AsyncCallback(SendCallback), handler); // Begin sending the data to the remote device. 
        }

        private static void SendCallback(IAsyncResult ar)
        {
            try
            {
                Socket handler = (Socket) ar.AsyncState; // Retrieve the socket from the state object.

                int bytesSent = handler.EndSend(ar); // Complete sending the data to the remote device.
                Console.WriteLine("Sent {0} bytes to client.", bytesSent);

                handler.Shutdown(SocketShutdown.Both);
                handler.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}