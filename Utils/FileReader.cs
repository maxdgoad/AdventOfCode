namespace AdventOfCode.Utils;
internal static class FileReader
{
    static string MaxPath = "C:\\Users\\Max\\Desktop\\AdventOfCode\\TextFiles\\";
    public static List<List<string>> ReadFile(string fileName, string splitter = "")
    {
        var response = new List<List<string>>();
        string? line;
        try
        {
            //Pass the file path and file name to the StreamReader constructor
            StreamReader sr = new StreamReader(MaxPath + fileName);
            //Read the first line of text
            line = sr.ReadLine();
            //Continue to read until you reach end of file
            while (line != null)
            {
                response.Add(line.Split(splitter).Where(str => !string.IsNullOrEmpty(str)).Select(str => str.Trim()).ToList());
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

    public static List<List<char>> ReadFileCharArray(string fileName, string splitter = "")
    {
        var response = new List<List<char>>();
        string? line;
        try
        {
            //Pass the file path and file name to the StreamReader constructor
            StreamReader sr = new StreamReader(MaxPath + fileName);
            //Read the first line of text
            line = sr.ReadLine();
            //Continue to read until you reach end of file
            while (line != null)
            {
                response.Add(line.ToCharArray().ToList());
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
