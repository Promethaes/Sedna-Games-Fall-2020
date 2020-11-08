using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
public class UserMetricsLoggerScript : MonoBehaviour
{
    public List<UMLCheckpoint> checkpoints = new List<UMLCheckpoint>();

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

    [DllImport(dllName)]
    public static extern void LogCheckpointTime(float time);
    [DllImport(dllName)]
    public static extern void WriteUserMetricsToFile();
    [DllImport(dllName)]
    public static extern void SetDefaultWritePath(String str);
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        SetDefaultWritePath(new String("./Assets/Plugins/"));
    }

    float time = 3.0f;
    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        if (time <= 0.0f)
        {
            Debug.Log("Set off logger");
            LogCheckpointTime(Time.time);
            WriteUserMetricsToFile();
            time = 3.0f;
        }
    }
}
