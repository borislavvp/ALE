using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace logic.FiniteAutomataService.Helpers
{
    public static class FileHelper
    {
        public static void CreateFile(string directoryName,string filePath,string content)
        {
            // Create a new file
            DirectoryInfo di = null;
            if (!Directory.Exists(directoryName))
            {
                // Try to create the directory.
                Directory.CreateDirectory(directoryName);
            }

            //await File.WriteAllTextAsync(localFilePath, "Hello, World!");
            using (StreamWriter sw = File.CreateText(filePath))
            {
                sw.Write(content);
                sw.Close();
            }
        }
        public static void DeleteFiles(string directoryName)
        {
            string[] txtList = Directory.GetFiles(directoryName, "*.txt");
            foreach (string f in txtList)
            {
                File.Delete(f);
            }
        }
    }
}
