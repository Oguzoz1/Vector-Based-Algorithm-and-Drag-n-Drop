using UnityEngine;
using System.IO;
public static class TextWriter
{
    public static void Write(string input)
    {
        string path = Application.dataPath + "/Output/receipt.txt";
        StreamWriter writer = new StreamWriter(path, true);
        writer.WriteLine(input);
        writer.Close();
    }
    public static void Clean()
    {
        string path = Application.dataPath + "/Output/receipt.txt";
        StreamWriter writer = new StreamWriter(path, false);
        writer.Write(" ");
        writer.Close();
    }
    public static void Read()
    {
        string path = Application.dataPath + "/Output/receipt.txt";
        StreamReader reader = new StreamReader(path);

        Debug.Log(reader.ReadToEnd());
        reader.Close();
    }
}
