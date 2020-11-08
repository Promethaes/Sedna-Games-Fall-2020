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

    [DllImport(dllName)]
    public static extern void LogCheckpointTime(float time);
    [DllImport(dllName)]
    public static extern void WriteUserMetricsToFile();
    [DllImport(dllName)]
    public static extern void SetDefaultWritePath(String str);

    public void csLogCheckpointTime(float time){
        LogCheckpointTime(time);
    }
    public void csWriteUserMetricsToFile(){
        WriteUserMetricsToFile();
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        SetDefaultWritePath(new String("./Assets/Plugins/"));

    }

    // Update is called once per frame
    void Update()
    {
    }
    private void OnDestroy() {
        WriteUserMetricsToFile();
    }
}
