using UnityEngine;
using System.IO;
public static class TextWriter
{
    public static void Write(string input, string fileName)
    {
        string path = Application.dataPath + "/Output/" + fileName + ".txt";
        StreamWriter writer = new StreamWriter(path, true);
        writer.WriteLine(input);
        writer.Close();
    }
    public static void Clean(string fileName)
    {
        string path = Application.dataPath + "/Output/" + fileName + ".txt";
        StreamWriter writer = new StreamWriter(path, false);
        writer.Write(" ");
        writer.Close();
    }
    public static void Read(string fileName)
    {
        string path = Application.dataPath + "/Output/" + fileName + ".txt";
        StreamReader reader = new StreamReader(path);

        Debug.Log(reader.ReadToEnd());
        reader.Close();
    }
}
