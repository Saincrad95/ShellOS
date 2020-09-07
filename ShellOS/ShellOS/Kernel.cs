using System;
using Sys = Cosmos.System;
using Cosmos.System.FileSystem.VFS;
using System.IO;

namespace ShellOS {
    public class Kernel : Sys.Kernel {

        public static Sys.FileSystem.CosmosVFS VFS = new Sys.FileSystem.CosmosVFS();
        public static string BOOTDIRECTORY = @"0:/";
        public static string VERSION_ID = "0.0.1.5";
        public string currentDirectory;

        protected override void BeforeRun() {
            VFSManager.RegisterVFS(VFS);
            currentDirectory = BOOTDIRECTORY;
            Console.Clear();
            Console.WriteLine("ShellOS has booted successfully. Need help type 'help' as a command");
        }

        protected override void Run() {
            InputCommands();
        }

        public void InputCommands() {
            Console.Write(currentDirectory + ">");
            string input = Console.ReadLine();

            if (input.StartsWith("help")) {
                availableCommands();
            } else if (input.StartsWith("format")) {
                formatVolume();
            } else if (input.StartsWith("dir")) {
                if (input.Contains("-a")) {
                    getVolumes();
                } else {
                    getAllDirectories();
                }
            } else if (input.StartsWith("mkdir")) {
                makeDirectoryCommand(splitInput(input));
            } /*else if (input.StartsWith("cd")) {
                 changeDirectory(splitInput(input));
             }*/ else if (input.StartsWith("clear")) {
                Console.Clear();
            } else if (input.StartsWith("about")) {
                aboutCommand();
            } else if (input.StartsWith("echo")) {
                echoCommand(splitInput(input));
            } else if (input.StartsWith("restart") || input.StartsWith("reboot")) {
                restartCommand();
            } else if (input.StartsWith("shutdown")) {
                shutdownCommand();
            } else {
                badCommand();
            }
        }

        //Other functions to run
        public string[] splitInput(string command) {
            string[] commandArray = command.Split(' ');

            return commandArray;
        }
        public void shutdownCommand() {
            Console.Write("Are you sure you want to shutdown? (Y/N): ");
            var shutdown = Console.ReadLine();
            switch (shutdown.ToUpper()) {
                case "Y":
                    Sys.Power.Shutdown();
                    break;
                default:
                    Console.WriteLine("Returning to the OS.");
                    break;
            }
        }
        public void restartCommand() {
            Console.Write("Are you sure you want to restart? (Y/N): ");
            var restart = Console.ReadLine();
            switch (restart.ToUpper())
            {
                case "Y":
                    Sys.Power.Reboot();
                    break;
                default:
                    Console.WriteLine("Returning to the OS.");
                    break;
            }
        }
        public void echoCommand(string[] message) {
            string echo = "";
            for (var i = 1; i <= (message.Length - 1); i++) {
                if (message[i].Length == (message.Length - 1)) {
                    echo += message[i];
                } else {
                    echo += message[i] + " ";
                }
            }
            Console.WriteLine($"Echo: '{echo}'");
        }
        public void availableCommands() {
            Console.WriteLine("The available commands currently are help, about, clear, echo " +
                    "restart, and shutdown\n");

            Console.WriteLine("help -- Used to access this printout of available commands.");
            Console.WriteLine("about -- Tells the user about ShellOS");
            Console.WriteLine("clear -- Clears the screen of previous commands");
            Console.WriteLine("echo -- echo [message] -- message is what you want echoed.");
            Console.WriteLine("restart/reboot -- Restarts the computer.");
            Console.WriteLine("shutdown -- Turns the computer off.");
        }
        public void aboutCommand() {
            Console.WriteLine("ShellOS is a modular shell of an operating sytem");
            Console.WriteLine("Author: Austin Raab (Saincrad95)");
            Console.WriteLine("Goal: Create an OS in which is modular that" +
                " you will only use what you need from it.");
            Console.WriteLine($"You are using version: {VERSION_ID}");
            Console.WriteLine("Report any and all bugs to austinraab95@gmail.com");
        }
        public void badCommand() {
            Console.WriteLine("Command not supported; Type 'help' for a list on supported" +
                    " commands.");
        }

        //Filesystem functions
        //Get all directories
        /*public void getAllVolumes() {
            var volumeEntries = VFS.GetVolumes();
            foreach (var volume in volumeEntries) {
                Console.WriteLine($"{volume}"); //ToString() not yet implemented
            }
        }*/

        public void formatVolume() {
            VFS.Format(@"0", "FAT32", false);
        }

        public void getAllDirectories() {
            foreach (var directory in VFS.GetDirectoryListing(currentDirectory)) {
                Console.WriteLine("\tName\tType");
                Console.WriteLine($"\t{directory.mName}\t{directory.mEntryType}");
            }
        }
        public void getVolumes() {
            foreach (var volume in DriveInfo.GetDrives()) {
                Console.WriteLine($"{volume.Name}");
            }
        }
        public void makeDirectoryCommand(string[] args) {
            string dirPath = args[1];
            string localDirectory = currentDirectory + dirPath + "/";
            Console.Write($"Do you want to make the directory: {dirPath} in {currentDirectory} (Y/N): ");
            string pathing = Console.ReadLine();
            switch (pathing.ToUpper()) {
                case "Y":
                    if (!Directory.Exists(dirPath)) {
                        Directory.CreateDirectory(dirPath);
                        Console.WriteLine($"The directory {dirPath} was created at {currentDirectory} making {localDirectory}.");
                        currentDirectory = localDirectory;
                    } else {
                        Console.WriteLine($"Can not create path {localDirectory} becuase the path already exists.");
                    }
                    break;
                default:
                    Console.WriteLine($"The directory {dirPath} will not be created.");
                    break;
            }
        }

        //Change Directories
        /*public void changeDirectory(string[] destArray) {

            var dest = destArray[1];

            if (System.IO.Directory.Exists(dest)) {
                currentDirectory = dest;
            } else {
                Console.WriteLine($"The directory {dest} doesn't exist.");
                Console.Write($"Would you like to create the directory {dest} (Y/N): ");
                var createDirectory = Console.ReadLine();
                switch (createDirectory.ToUpper()) {
                    case "Y":
                        VFS.CreateDirectory(dest);
                        currentDirectory = dest;
                        break;
                    default:
                        Console.WriteLine($"{dest} was not created.");
                        break;
                }
            }
        }*/
    }
}
