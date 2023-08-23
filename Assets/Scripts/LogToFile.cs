using UnityEngine;
using System.IO;
using System.Text;
using System;

public class LogToFile : MonoBehaviour
{
    private StreamWriter writer;

    private static LogToFile instance;

    private void Awake()
    {
        Debug.Log("Log file initialised...");
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        //DontDestroyOnLoad(gameObject);

        // Define the file path for the log file
        string logFilePath = Path.Combine(Application.persistentDataPath, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "UnityLogs.txt");

        // Open the file in append mode
        writer = File.AppendText(logFilePath);

        // Subscribe to Unity's log message event
        Application.logMessageReceived += HandleLog;
    }

    private void OnDestroy()
    {
        // Unsubscribe from the log message event
        Application.logMessageReceived -= HandleLog;

        // Dispose the writer
        writer?.Dispose();
        writer = null;
    }


    private StringBuilder stringBuilder = new StringBuilder();

    private void HandleLog(string logString, string stackTrace, LogType type)
    {
        stringBuilder.Clear();
        string logMessage = string.Format("[{0}] {1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), logString);

        stringBuilder.Append(logMessage);
        writer.WriteLine(stringBuilder.ToString());
        writer.Flush();
    }

}
