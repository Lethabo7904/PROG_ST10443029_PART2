using System.Collections.Generic;
using System.IO;
using System;

namespace Cybersecurity_Awareness_Bot_PART1
{
    public class memory_recall
    {
        public memory_recall()
        {
            try
            {
                string fullPath = AppDomain.CurrentDomain.BaseDirectory;
                string newPath = fullPath.Replace("bin\\Debug\\", "");
                string path = Path.Combine(newPath, "memory.txt");

                var memory_loaded = memory_load(path);

                foreach (var check in memory_loaded)
                {
                    Console.WriteLine(check);
                }

                // Append to file to avoid overwriting
                File.AppendAllLines(path, new List<string> { "Lungisani, what is password" });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error accessing memory file: {ex.Message}");
            }
        }

        private List<string> memory_load(string path)
        {
            if (File.Exists(path))
            {
                return new List<string>(File.ReadAllLines(path));
            }
            else
            {
                File.WriteAllText(path, "");
                return new List<string>();
            }
        }
    }
}
