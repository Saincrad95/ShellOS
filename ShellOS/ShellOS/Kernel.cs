using System;
using System.Collections.Generic;
using System.Text;
using Sys = Cosmos.System;

namespace ShellOS
{
    public class Kernel : Sys.Kernel
    {
        public static Sys.FileSystem.CosmosVFS newFS = new Sys.FileSystem.CosmosVFS();

        protected override void BeforeRun()
        {
            newFS.Initialize();
            Console.Clear();
            Console.WriteLine("ShellOS has booted successfully. Need help type 'help' as a command");
        }

        protected override void Run()
        {
            InputCommands();
        }

        public void InputCommands(/*string arg*/) {
            Console.Write("\n$ ");
            string input = Console.ReadLine();

            if (input == "SayHello")
            {
                Console.Write("Hello, World!");
            } else if (input == "help")
            {
                Console.WriteLine("The available commands currently are help, SayHello, " +
                    "echo, restart, and shutdown");
            } else if (input == "echo")
            {
                Console.Write("What would you like to echo: ");
                var echo = Console.ReadLine();
                Console.WriteLine($"Echo: {echo}");
            } else if (input == "restart" || input == "reboot")
            {
                Console.Write("Are you sure you want to restart? (Y/N): ");
                var restart = Console.ReadLine();
                switch (restart.ToUpper())
                {
                    case "Y":
                        Sys.Power.Reboot();
                        break;
                    case "N":
                        Console.WriteLine("Returning to the OS.");
                        break;
                    default:
                        break;
                }
            } else if (input == "shutdown")
            {
                Console.Write("Are you sure you want to shutdown? (Y/N): ");
                var shutdown = Console.ReadLine();
                switch (shutdown.ToUpper())
                {
                    case "Y":
                        Sys.Power.Shutdown();
                        break;
                    case "N":
                        Console.WriteLine("Returning to the OS.");
                        break;
                    default:
                        break;
                }
            } else
            {
                Console.WriteLine("Command not supported; Type 'help' for any help on supported" +
                    " commands.");
            }
        }
    }
}
