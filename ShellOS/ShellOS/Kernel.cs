using System;
using System.Collections.Generic;
using System.Text;
using Sys = Cosmos.System;

namespace ShellOS
{
    public class Kernel : Sys.Kernel
    {
        protected override void BeforeRun()
        {
            Console.Clear();
            Console.WriteLine("ShellOS has booted successfully. Need help type 'help' as a command");
        }

        protected override void Run()
        {
            Input();
        }

        public void Input(/*string arg*/) {
            Console.Write("\n$ ");
            string input = Console.ReadLine();

            if (input == "SayHello")
            {
                Console.Write("Hello, World!");
            } else if (input == "help")
            {
                Console.WriteLine("The available commands currently are help, SayHello, and " +
                    "echo.");
            } else if (input == "echo")
            {
                Console.Write("What would you like to echo: ");
                Console.ReadLine();
            } else
            {
                Console.WriteLine("Command not supported; Type 'help' for any help on supported" +
                    "commands.");
            }
        }
    }
}
