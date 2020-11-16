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

    [StructLayout(LayoutKind.Sequential)]
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
    public static extern void LogCheckpointTime(float time);

    [DllImport(dllName)]
    public static extern void LogDeath(Death death);

    [DllImport(dllName)]
    public static extern void WriteUserMetricsToFile();
    [DllImport(dllName)]
    public static extern void SetDefaultWritePath(String str);

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
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        SetDefaultWritePath(new String("./Assets/Plugins/"));

    }

    float timer = 1.0f;
    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0.0f){
            timer = 1.0f;
            csLogCheckpointTime(Time.time);
            csLogDeath(new Death("Memes",Time.time,1));
            csWriteUserMetricsToFile();
        }
    }
    private void OnDestroy()
    {
        WriteUserMetricsToFile();
    }
}
