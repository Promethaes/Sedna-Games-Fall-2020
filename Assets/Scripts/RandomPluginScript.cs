//
// @Cleanup: Maybe we can factor out a lot of the grudge work to load the DLL functions...
//
// Basic idea is: 
// - Make init function that loads dll using OpenLibrary (called in Start() or Awake())
// - Make shutdown function that unloads dll so it can be hotloaded (called in OnDestroy())
// - *PER FUNCTION EXPORTED BY DLL*:
//    - Make DelegateType with function definition
//    - Make variable with DelegateType
//    - In init(), use GetDelegate<DelegateType> to get the function pointer
// - To call, use variable(params) or variable.Invoke(params) 
//    - Invoke will give tooltips in VS Code (not sure about VS2019), but either will call the delegate function without error (hopefully)
//    - Delegate calls should ideally be wrapped in C# static functions so that they can be called from anywhere in the project
//    - Good practice would be to make sure the delegates are != null, but I don't know how much overhead this has...
//

using System;
using UnityEngine;

public class RandomPluginScript : MonoBehaviour
{

#if UNITY_EDITOR
    const string _dllPath = "/Plugins/TestDLL-cpp.dll";
#else
    const string _dllPath = "/Plugins/x86_x64/TestDLL-cpp.dll";
#endif

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
        
        _pluginHandle = ManualPluginImporter.OpenLibrary(Application.dataPath + _dllPath);
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
        return _dllRandomInt.Invoke(low, high);
    }

    public static uint GetRandomUIntCPP(uint low = 0u, uint high = 10u) {
        if(_dllRandomUInt == null) return 0;            // @Incomplete: maybe log here?
        return _dllRandomUInt.Invoke(low, high);
    }

    public static float GetRandomFloat(float low = 0.0f, float high = 1.0f) {
        if(_dllRandomFloat == null) return 0;            // @Incomplete: maybe log here?
        return _dllRandomFloat.Invoke(low, high);
    }

    void Start() {
        initRandomPlugin();
    }

    void Update() {
        int rand1 = GetRandomIntCPP(0, 5);
        //Debug.Log("Random int is: " + rand1);

        uint rand2 = GetRandomUIntCPP(0, 20);
        //Debug.Log("Random uint is: " + rand2);

        float rand3 = GetRandomFloat(0.0f, 6.0f);
        //Debug.Log("Random float is: " + rand3);
    }

    void OnDestroy() {
        shutdownRandomPlugin();
    }
}
