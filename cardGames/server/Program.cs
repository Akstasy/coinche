using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using server;

namespace server
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Please enter a port number: ");
                string port = Console.ReadLine();
                UInt16 numPort;
                UInt16.TryParse(port, out numPort);
                Server server = new Server(numPort);
                server.start();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
