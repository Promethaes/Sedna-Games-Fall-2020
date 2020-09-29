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
    DLLClass test = new DLLClass();

    private delegate void RandomSeed();
    private delegate int RandomInt(int low = 0, int high = 10);
    private delegate uint RandomUInt(uint low = 0u, uint high = 10u);
    private delegate float RandomFloat(float low = 0.0f, float high = 1.0f);
    private static RandomSeed _dllRandomSeed;
    private static RandomInt _dllRandomInt;
    private static RandomUInt _dllRandomUInt;
    private static RandomFloat _dllRandomFloat;

    private static IntPtr _pluginHandle = IntPtr.Zero;

    public static void initRandomPlugin() {
        if(_pluginHandle != IntPtr.Zero) return;       // @Incomplete: maybe log/error here?
        _pluginHandle = ManualPluginImporter.OpenLibrary(Application.dataPath + dllPath);
        if(_pluginHandle == IntPtr.Zero) return;       // @Incomplete: maybe log/error here?

        _dllRandomSeed  = ManualPluginImporter.GetDelegate<RandomSeed> (_pluginHandle, "dllRandomSeed");
        _dllRandomInt   = ManualPluginImporter.GetDelegate<RandomInt>  (_pluginHandle, "dllRandomInt");
        _dllRandomUInt  = ManualPluginImporter.GetDelegate<RandomUInt> (_pluginHandle, "dllRandomUInt");
        _dllRandomFloat = ManualPluginImporter.GetDelegate<RandomFloat>(_pluginHandle, "dllRandomFloat");

        if(_dllRandomSeed == null) return;             // @Incomplete: maybe log/error here?
        _dllRandomSeed.Invoke();
        Debug.Log("New seed invoked");
    }

    public static void shutdownRandomPlugin() {
        if(_pluginHandle == IntPtr.Zero) return;       // @Incomplete: maybe log/error here?
        ManualPluginImporter.CloseLibrary(_pluginHandle);
    }

    public static int GetRandomIntCPP(int low = 0, int high = 10) {
        if(_dllRandomInt == null) return 0;            // @Incomplete: maybe log here?
        return _dllRandomInt/*.Invoke*/(low, high);
    }

    public static uint GetRandomUIntCPP(uint low = 0u, uint high = 10u) {
        if(_dllRandomUInt == null) return 0;            // @Incomplete: maybe log here?
        return _dllRandomUInt/*.Invoke*/(low, high);
    }

    public static float GetRandomFloat(float low = 0.0f, float high = 1.0f) {
        if(_dllRandomFloat == null) return 0;            // @Incomplete: maybe log here?
        return _dllRandomFloat/*.Invoke*/(low, high);
    }

    void Start() {
        initRandomPlugin();
    }

    void Update() {
        int rand = test.GetRandomInt();
        Debug.Log("Random int is: " + rand);

        int rand2 = GetRandomIntCPP(0, 5);
        Debug.Log("Random int 2 is: " + rand2);

        uint rand3 = GetRandomUIntCPP(0, 20);
        Debug.Log("Random uint is: " + rand3);

        float rand4 = GetRandomFloat(0.0f, 6.0f);
        Debug.Log("Random float is: " + rand4);
    }

    void OnDestroy() {
        shutdownRandomPlugin();
    }
}
