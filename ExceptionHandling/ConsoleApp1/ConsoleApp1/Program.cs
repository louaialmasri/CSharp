using System;
using System.IO;

namespace Exceptionhandling
{
    class Program
    {
        static void Main(string[] args)
        {
            string logPath = "log.txt";
            string fileName = "test.txt";
            string path = "/folder/folder2/"; // invalid

            try
            {
                string finalPath = Path.Combine(path, fileName);
                File.Create(Path.Combine(path + fileName));

                File.AppendAllText(finalPath, "hallo Welt");

                File.AppendAllText(logPath, "Log: Successfully created file.\n");
            }
            catch(IOException e) 
            {
                File.AppendAllText(logPath, "Log: File already in use." + e + "\n");
            }
            catch (Exception e) 
            {
                Directory.CreateDirectory(path);
                File.AppendAllText(logPath, "Log: Something went wrong during file creation: Exception: " + e + "\n");
            }
            

            File.AppendAllText(logPath, "Log: Tried to create file.\n");

        }
    }
}