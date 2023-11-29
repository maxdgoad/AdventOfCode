using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    internal static class FileReader
    {
        static string MaxPath = "C:\\Users\\Max\\Desktop\\AdventOfCode\\TextFiles\\";
        public static List<List<string>> ReadFile(string fileName)
        {
            var response = new List<List<string>>();
            String? line;
            try
            {
                //Pass the file path and file name to the StreamReader constructor
                StreamReader sr = new StreamReader(MaxPath + fileName);
                //Read the first line of text
                line = sr.ReadLine();
                //Continue to read until you reach end of file
                while (line != null)
                {
                    response.Add(line.Split(" ").ToList());
                    line = sr.ReadLine();
                }
                //close the file
                sr.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            return response;
        }
    }
}
