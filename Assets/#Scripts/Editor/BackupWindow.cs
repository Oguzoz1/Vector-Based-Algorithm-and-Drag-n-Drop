using UnityEditor;
using UnityEngine;
using System;
using System.IO;
using System.IO.Compression;
using System.Threading;

public class BackupWindow : EditorWindow
{
    private string _version;
    private string _author;
    private string _description;
    private string _link;
    private string _loadPath;
    private string _savePath;
    private static GUIStyle _boldStyle;

    [MenuItem("Window/Backup")]
    public static void ShowWindow()
    {
        Initialise();
    }
    private static void Initialise()
    {
        WindowInitialising();
        TextInitialising();
    }
    private static void WindowInitialising()
    {
        BackupWindow window = EditorWindow.GetWindow<BackupWindow>();
        GUIContent titleContent = new GUIContent("Back Up Window");
        window.titleContent = titleContent;
    }
    private static void TextInitialising()
    {
        _boldStyle = new GUIStyle(EditorStyles.label);
        _boldStyle.fontStyle = FontStyle.Bold;
        _boldStyle.alignment = TextAnchor.MiddleCenter;
        _boldStyle.normal.textColor = Color.white;
    }
    private void OnGUI()
    {
        EditorGUILayout.BeginVertical();
        EditorGUILayout.LabelField("System Message", _boldStyle);
        EditorGUILayout.LabelField("Fill in the blanks to be able to back up your files.");
        EditorGUILayout.Space(10);

        VariableGUI();

        EditorGUILayout.EndVertical();
    }
    //Entire GUI thats related to variables
    private void VariableGUI()
    {
        //Author
        EditorGUILayout.LabelField("Author:");
        GUI.color = string.IsNullOrEmpty(_author) ? Color.red : Color.green;
        _author = EditorGUILayout.TextField(_author, GUILayout.Width(200), GUILayout.Height(20));

        GUI.color = Color.white;
        //Version
        EditorGUILayout.LabelField("Version(ex: v1,v2...):");
        GUI.color = string.IsNullOrEmpty(_version) ? Color.red : Color.green;
        _version = EditorGUILayout.TextField(_version, GUILayout.Width(50), GUILayout.Height(20));

        //Description
        if (!string.IsNullOrEmpty(_author))
        {
            GUI.color = Color.white;
            EditorGUILayout.LabelField("Description:");
            GUI.color = string.IsNullOrEmpty(_description) ? Color.red : Color.green;
            _description = EditorGUILayout.TextArea(_description, GUILayout.Width(400), GUILayout.Height(200));
        }

        //Link
        if (!string.IsNullOrEmpty(_description) && !string.IsNullOrEmpty(_author))
        {
            GUI.color = Color.white;
            EditorGUILayout.LabelField("Slack Link:");
            GUI.color = string.IsNullOrEmpty(_link) ? Color.red : Color.green;
            _link = EditorGUILayout.TextField(_link);
        }


        //Button and PATH
        if (!string.IsNullOrEmpty(_link) && !string.IsNullOrEmpty(_description))
        {
            GUI.color = Color.white;

            //PATH
            EditorGUILayout.Space(10);

            //Files to back up
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(_loadPath);
            if (GUILayout.Button("Load Files", GUILayout.Width(100), GUILayout.Height(20)))
            {
                _loadPath = EditorUtility.OpenFolderPanel("Files To Load", "", "");

            }
            EditorGUILayout.EndHorizontal();

            //Save Location
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(_savePath);
            GUI.color = string.IsNullOrEmpty(_loadPath) ? Color.red : Color.white;
            if (GUILayout.Button("Save Folder", GUILayout.Width(100), GUILayout.Height(20)) && !string.IsNullOrEmpty(_loadPath))
            {

                _savePath = EditorUtility.OpenFolderPanel("Back Up Location", "", "");

            }
            EditorGUILayout.EndHorizontal();

            //BUTTON
            GUI.color = string.IsNullOrEmpty(_savePath) ? Color.red : Color.white;
            EditorGUILayout.Space(30);
            if (GUILayout.Button("SAVE") && !string.IsNullOrEmpty(_savePath))
            {
                //Add something to check if its slack link or else give a pop up message
                BackUp();
            }
        }
    }
    private void BackUp()
    {
        //WE NEED TO MAKE SURE WE ARE COPYING THE RIGHT FILES.
        string fileName = Path.GetFileName(_loadPath);
        InitialiseBackupTextFile(fileName + "_" + _version, _savePath);
        CreateZip(fileName + "_" + _version, _loadPath);
    }
   
    private void InitialiseBackupTextFile(string fileName, string savePath)
    {
        fileName = fileName + ".txt";
        string newSavePath = Path.Combine(savePath, fileName);

        if (!File.Exists(newSavePath))
        {
            using (StreamWriter sw = File.CreateText(newSavePath))
            {
                sw.WriteLine("Date: " + DateTime.Now);
                sw.WriteLine("Author: " + _author);
                sw.WriteLine("Description: \n" + _description);
                sw.WriteLine("Link: " + _link);
                sw.Flush();
            }
        }
    }
    private void CreateZip(string name,string sourcePath)
    {
        string zipLocationPath = Path.Combine(_savePath, name + ".zip");
        if (!File.Exists(zipLocationPath))
        {
            ZipFile.CreateFromDirectory(sourcePath, zipLocationPath, System.IO.Compression.CompressionLevel.Optimal, true);
        }
    }
 
}