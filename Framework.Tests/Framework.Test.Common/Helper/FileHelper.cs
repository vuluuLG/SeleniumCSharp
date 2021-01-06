using System;
using System.Diagnostics;
using System.IO;

namespace Framework.Test.Common.Helper
{
    public static class FileHelper
    {
        public static void CopyFile(string inputFilePath, string outputFilePath)
        {
            int bufferSize = 1024 * 1024;
            using (FileStream input = new FileStream(inputFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (FileStream output = new FileStream(outputFilePath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite))
                {
                    output.SetLength(input.Length);
                    int bytesRead = -1;
                    byte[] bytes = new byte[bufferSize];
                    while ((bytesRead = input.Read(bytes, 0, bufferSize)) > 0)
                    {
                        output.Write(bytes, 0, bytesRead);
                    }
                }
            }
        }

        public static string ReadAllTextFromFile(string filePath)
        {
            string contents = null;
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (StreamReader sr = new StreamReader(fs))
                {
                    contents = sr.ReadToEnd();
                }
            }
            return contents;
        }

        public static void WriteAllTextToFile(string filePath, string contents)
        {
            if (contents != null)
            {
                using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite))
                {
                    using (StreamWriter sw = new StreamWriter(fs))
                    {
                        sw.Write(contents);
                        fs.SetLength(contents.Length);
                    }
                }
            }
        }

        public static string GetProjectPath(bool includeBinFolder = false)
        {
            //string path_old = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\";
            string path = Directory.GetCurrentDirectory() + "\\";
            string actualPath = path;
            if (!includeBinFolder)
            {
                actualPath = path.Substring(0, path.LastIndexOf("bin"));
            }
            string projectPath = new Uri(actualPath).LocalPath; // project path of your solution
            return projectPath;
        }

        public static bool DoesFileExist(string filePath, int timeOutInSecond)
        {
            bool exists = false;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            do
            {
                if (File.Exists(filePath))
                {
                    exists = true;
                    break;
                }
            }
            while (stopwatch.ElapsedMilliseconds <= timeOutInSecond * 1000);
            stopwatch.Stop();

            return exists;
        }
    }
}
