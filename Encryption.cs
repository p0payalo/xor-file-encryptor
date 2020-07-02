using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Encryptor
{
    public class Encryption
    {
        public int ThreadsCount { get; private set; }
        public string Path { get; set; }
        public string Password { get; set; }
        private string[] AllFiles;
        private List<string> AvailableFiles;
        private Mutex mutex;

        public Encryption(string path, string password, int threads = 20)
        {
            Path = path;
            ThreadsCount = threads;
            Password = password;
            AllFiles = Directory.GetFiles(path);
            AvailableFiles = new List<string>((string[])AllFiles.Clone());
            mutex = new Mutex();
        }

        public void StartEncryption()
        {
            List<Task> threads = new List<Task>(ThreadsCount);
            int counter = 0;
            for(int i = 0; i < ThreadsCount; i++)
            {
                Task t = Task.Run(() => {
                    int count = counter;
                    counter++;
                    int result = 1;
                    do
                    {
                        result = EncryptNextFileAsync(count);
                        count++;
                    } while (result != 0);
                });
                threads.Add(t);
            }
            Task.WaitAll(threads.ToArray(), -1);
        }

        //results
        //-1 - file is busy, 0 - count > AllFiles.Length, 1 - Encryption success
        private int EncryptNextFileAsync(int count)
        {
            try
            {
                mutex.WaitOne();
                if (count < AllFiles.Length && !AvailableFiles.Contains(AllFiles[count]))
                {
                    mutex.ReleaseMutex();
                    return -1;
                }
                if (count < AllFiles.Length && AvailableFiles.Contains(AllFiles[count]))
                {
                    AvailableFiles.Remove(AllFiles[count]);
                    mutex.ReleaseMutex();
                    Console.WriteLine("Encrypting/Decrypting " + AllFiles[count]);
                    byte[] data = File.ReadAllBytes(AllFiles[count]);
                    for (int i = 0; i < data.Length; i++)
                    {
                        data[i] ^= Convert.ToByte(Password[i % Password.Length]);
                    }
                    using (FileStream fs = new FileStream(AllFiles[count], FileMode.Open))
                    {
                        fs.Write(data, 0, data.Length);
                    }
                    Console.WriteLine("Encrypting/Decrypting " + AllFiles[count] + " was ended");
                    return 1;
                }
                else
                {
                    mutex.ReleaseMutex();
                    return 0;
                }
            }
            catch (UnauthorizedAccessException e)
            {
                Console.WriteLine(e.Message);
                mutex.ReleaseMutex();
                return -1;
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
                mutex.ReleaseMutex();
                return -1;
            }
        }

        public static void EncryptFile(string filename, string password)
        {
            try
            {
                Console.WriteLine("Encrypting/Decrypting " + filename);
                byte[] data = File.ReadAllBytes(filename);
                for (int i = 0; i < data.Length; i++)
                {
                    data[i] ^= Convert.ToByte(password[i % password.Length]);
                }
                using (FileStream fs = new FileStream(filename, FileMode.Open))
                {
                    fs.Write(data, 0, data.Length);
                }
                Console.WriteLine("Encrypting/Decrypting " + filename + " was ended");
            }
            catch (UnauthorizedAccessException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
