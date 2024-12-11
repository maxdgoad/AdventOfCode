namespace AdventOfCode.Utils;
internal static class FileReader
{
    static string FilePath = "\\TextFiles2024\\";
    public static List<List<string>> ReadFile(string fileName, string splitter = "")
    {
        var response = new List<List<string>>();
        var currentDirectory = Directory.GetCurrentDirectory();
        string? line;
        try
        {
            // Pass the file path and file name to the StreamReader constructor
            StreamReader sr = new StreamReader(currentDirectory + FilePath + fileName);
            // Read the first line of text
            line = sr.ReadLine();
            // Continue to read until you reach end of file
            while (line != null)
            {
                response.Add(line.Split(splitter).Where(str => !string.IsNullOrEmpty(str)).Select(str => str.Trim()).ToList());
                line = sr.ReadLine();
            }
            // Close the file
            sr.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine("Exception: " + e.Message);
        }

        return response;
    }

    public static char[][] ReadFileCharArray(string fileName, string splitter = "")
    {
        var response = new List<char[]>();
        var currentDirectory = Directory.GetCurrentDirectory();
        string? line;
        try
        {
            // Pass the file path and file name to the StreamReader constructor
            StreamReader sr = new StreamReader(currentDirectory + FilePath + fileName);
            // Read the first line of text
            line = sr.ReadLine();
            // Continue to read until you reach end of file
            while (line != null)
            {
                response.Add(line.ToCharArray());
                line = sr.ReadLine();
            }
            // Close the file
            sr.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine("Exception: " + e.Message);
        }

        return response.ToArray();
    }
}
