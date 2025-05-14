using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;

namespace Cybersecurity_Awareness_Bot_PART1
{
    public class Program
    {
        
        static void Main(string[] args)
        {
            //creating an instance for a class voice_greeting
            new voice_greeting() { };

            //creating an instance for a class logo_design
            new logo_design() { };

            //creating an instance for a class chatbot
            new cybersecurity_chatbot() { };

            //creating an instance for a class memory_recall
            new memory_recall() { };

        }
    }
}
