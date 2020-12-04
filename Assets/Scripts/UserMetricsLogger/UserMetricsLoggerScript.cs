using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
public class UserMetricsLoggerScript : MonoBehaviour
{
    #region DLLStuff

    const string dllName = "UserMetricsLogger";

    [StructLayout(LayoutKind.Sequential)]
    public struct String
    {
        [MarshalAs(UnmanagedType.LPStr)]
        public string data;
        public String(string s)
        {
            data = s;
        }
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct Death
    {
        public float timeOfDeath;
        [MarshalAs(UnmanagedType.LPStr)]
        public string causeOfDeath;
        public int playerNum;

        public Death(string cause, float time, int pNum)
        {
            causeOfDeath = cause;
            timeOfDeath = time;
            playerNum = pNum;
        }
    }

    [DllImport(dllName)]
    private static extern void LogCheckpointTime(float time);
    [DllImport(dllName, CharSet = CharSet.Ansi)]
    private static extern void LogDeath(Death death);
    [DllImport(dllName)]
    private static extern void WriteUserMetricsToFile();
    [DllImport(dllName)]
    private static extern void SetDefaultWritePath(String str);
    [DllImport(dllName)]
    private static extern void ClearUserMetricsLogger();
    [DllImport(dllName)]
    private static extern void ClearUserMetricsLoggerFileOnly();

    public void csLogCheckpointTime(float time)
    {
        LogCheckpointTime(time);
    }
    public void csLogDeath(Death death)
    {
        LogDeath(death);
    }
    public void csWriteUserMetricsToFile()
    {
        WriteUserMetricsToFile();
    }
    public void csClearUserMetricsLogger()
    {
        ClearUserMetricsLogger();
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        SetDefaultWritePath(new String("./Assets/Plugins/"));
    }
    //@Temp: remove later
    public bool writeToFile = false;
    public bool clearData = false;
    public bool clearFile = false;
    void Update()
    {
        if (clearData)
        {
            clearData = false;
            ClearUserMetricsLogger();
        }
        if (clearFile)
        {
            clearFile = false;
            ClearUserMetricsLoggerFileOnly();
        }
        if (writeToFile)
        {
            writeToFile = false;
            WriteUserMetricsToFile();
        }
    }
    private void OnDestroy()
    {
        WriteUserMetricsToFile();
    }
}
