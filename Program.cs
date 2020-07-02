using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Encryptor
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string path = args[0];
                string target = args[1];
                string password = args[2];
                bool ThreadsParameter = false;
                int threads;
                if (args.Length == 4)
                {
                    bool result = Int32.TryParse(args[3], out threads);
                    if(!result)
                    {
                        Console.WriteLine("Bad threads argument");
                        Console.ReadLine();
                        return;
                    }
                    if(threads < 1 || threads > 100)
                    {
                        Console.WriteLine("Bad threads argument\nMax threads: 100, minimum threads: 1");
                        Console.ReadLine();
                        return;
                    }
                    ThreadsParameter = true;
                }
                else threads = 20;
                if (target.Equals("-d") || target.Equals("-dir"))
                {
                    if (Directory.Exists(path))
                    {
                        Encryption encryption = new Encryption(path, password, threads);
                        encryption.StartEncryption();
                        Console.WriteLine("Encrypt was ended");
                    }
                    else
                    {
                        Console.WriteLine("Directory not exist");
                    }
                }
                else if ((target.Equals("-f") || target.Equals("-file")) && !ThreadsParameter)
                {
                    if (File.Exists(path))
                    {
                        Encryption.EncryptFile(path, password);
                        Console.WriteLine("Encrypt was ended");
                    }
                    else
                    {
                        Console.WriteLine("File not exist");
                    }
                }
                else Console.WriteLine("Bad flag argument");
            }
            catch (IndexOutOfRangeException e)
            {
                Console.WriteLine(e.Message + "\nBad arguments");
            }
            catch (Exception e)
            {
                Console.WriteLine("Undefined error, " + e.Message);
            }
            finally
            {
                Console.ReadLine();
            }
        }
    }
}
