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
                if (target.Equals("-d") || target.Equals("-dir"))
                {
                    if (Directory.Exists(path))
                    {
                        EncryptDirectory(path, password);
                        Console.WriteLine("Encrypt was ended");
                    }
                    else
                    {
                        Console.WriteLine("Directory not exist");
                    }
                }
                else if (target.Equals("-f") || target.Equals("-file"))
                {
                    if (File.Exists(path))
                    {
                        EncryptFile(path, password);
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

        static void EncryptDirectory(string path, string pass)
        {
            string[] files = Directory.GetFiles(path);
            List<Task> tasks = new List<Task>();
            foreach (string file in files)
            {
                tasks.Add(Task.Run(new Action(() =>
                {
                    EncryptFile(file, pass);
                })));
            }
            Task.WaitAll(tasks.ToArray(), -1); 
        }

        static void EncryptFile(string filename, string pass)
        {
            try
            {
                Console.WriteLine("Encrypting/Decrypting " + filename);
                byte[] data = File.ReadAllBytes(filename);
                for (int i = 0; i < data.Length; i++)
                {
                    data[i] ^= Convert.ToByte(pass[i % pass.Length]);
                }
                using (FileStream fs = new FileStream(filename, FileMode.Open))
                {
                    fs.Write(data, 0, data.Length);
                }
                Console.WriteLine("Encrypting/Decrypting " + filename + " was ended");
            }
            catch(UnauthorizedAccessException e)
            {
                Console.WriteLine(e.Message);
            }
            catch(IOException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
