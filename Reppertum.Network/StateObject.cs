using System;
using System.Net.Sockets;
using System.Text;

namespace Reppertum.Network
{
    // State object for reading client data asynchronously  
    public class StateObject
    {
        public Socket WorkSocket = null; // Client socket
        public const Int32 ClientBufferSize = 1024; // Size of receive buffer
        public const Int32 ServerBufferSize = 256; // Size of receive buffer
        public byte[] ClientBuffer = new byte[ClientBufferSize]; // Receive buffer
        public byte[] ServerBuffer = new byte[ServerBufferSize]; // Receive buffer
        public StringBuilder Sb = new StringBuilder(); // Received data string
    }
}