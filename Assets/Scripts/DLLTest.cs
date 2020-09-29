using System;
using TestDLL;
using UnityEngine;

public class DLLTest : MonoBehaviour
{

#if UNITY_EDITOR
    const string dllPath = "/Plugins/TestDLL-cpp.dll";
#else
    const string dllPath = "/Plugins/x86_x64/TestDLL-cpp.dll";
#endif

    private delegate int RandomInt(int low, int high);
    private static RandomInt _dllRandomInt;

    private static IntPtr _pluginHandle = IntPtr.Zero;

    public static void initRandomPlugin() {
        if(_pluginHandle != IntPtr.Zero) return;       // @Incomplete: maybe log/error here?
        _pluginHandle = ManualPluginImporter.OpenLibrary(Application.dataPath + dllPath);
        if(_pluginHandle == IntPtr.Zero) return;       // @Incomplete: maybe log/error here?

        _dllRandomInt = ManualPluginImporter.GetDelegate<RandomInt>(_pluginHandle, "dllRandomInt");
    }

    public static void shutdownRandomPlugin() {
        if(_pluginHandle == IntPtr.Zero) return;       // @Incomplete: maybe log/error here?
        ManualPluginImporter.CloseLibrary(_pluginHandle);
    }

    public static int GetRandomIntCPP(int low = 0, int high = 10) {
        if(_dllRandomInt == null) return 0;            // @Incomplete: maybe log here?
        return _dllRandomInt.Invoke(low, high);
    }

    void Start()
    {
        initRandomPlugin();
        DLLClass test = new DLLClass();
        
        
        int rand = test.GetRandomInt();
        print("Random int is: " + rand);

        int rand2 = GetRandomIntCPP();
        print("Random int 2 is: " + rand);


        shutdownRandomPlugin();
    }
}
