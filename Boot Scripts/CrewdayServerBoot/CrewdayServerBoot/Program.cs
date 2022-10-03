using System;
using System.IO;
using System.Diagnostics;
using System.Xml;

// Crewday Server Boot Utility
// W. Burgin 2022
//
// A small utility console program that reads an XML file and executes
// the program or batch file at the supplied path. It will not wait
// for a return since most do not until the game server is shutdown.
//
// Does not currently support passing arguments; for my use case
// the XML is usually pointed to another boot script.
//
// This program should be configured to start up at PC boot time.

namespace CrewdayServerBoot
{
    class Program
    {
        private static String Base_Path;
        private static String Path_Data;

        static void Main(string[] args)
        {
            // Construct an absolute path for the config file.
            Base_Path = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            Path_Data = Base_Path + "\\paths.xml";

            // Load XML file with paths to server executables.
            Console.WriteLine("Reading Path Data XML...");
            XmlDocument paths = new XmlDocument();
            paths.Load(Path_Data);

            // Parse each node read and execute the file at given path.
            foreach(XmlNode node in paths.DocumentElement.ChildNodes)
            {
                Console.WriteLine("Starting: " + node.InnerText);

                try
                {
                    Process.Start(node.InnerText);
                }
                catch (Exception e)
                {
                    Console.WriteLine("ERROR: Could not start program: " + e.Message);
                }
            }

        }
    }
}
