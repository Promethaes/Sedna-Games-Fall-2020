using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.InteropServices;

public class ManualPluginImporter : MonoBehaviour
{
    // see https://jacksondunstan.com/articles/3945 for source

    // Import the functions that'll let us open the libraries manually, from kernel32
    [DllImport("kernel32")]
    public static extern IntPtr LoadLibrary(string path);

    [DllImport("kernel32")]
    public static extern IntPtr GetProcAddress(IntPtr libraryHandle, string symbolName);

    [DllImport("kernel32")]
    public static extern IntPtr FreeLibrary(IntPtr libraryHandle);

    // Wrapper of the LoadLibrary function above
    public static IntPtr OpenLibrary(string path)
    {
        IntPtr handle = LoadLibrary(path);
        if (handle == IntPtr.Zero)
        {
            throw new Exception("Couldn't open native plugin: " + path);
        }
        return handle;
    }

    // Wraper of the FreeLibrary function above
    public static void CloseLibrary(IntPtr libraryHandle)
    {
        FreeLibrary(libraryHandle);
    }

    // Wrapper of the GetProcAddress above
    public static T GetDelegate<T>(IntPtr libraryHandle, string functionName)
        where T : class
    {
        IntPtr symbol = GetProcAddress(libraryHandle, functionName);
        if (symbol == IntPtr.Zero)
        {
            throw new Exception("Couldn't get function address: " + functionName);
        }
        return Marshal.GetDelegateForFunctionPointer(symbol, typeof(T)) as T;
    }
}

/*

// @EmoryCode @CopyPaste: Usage below
private delegate bool DelegateClass(params);     // Function pointer definition (std::function<bool(params)> or bool(*DelegateClass)(params))
private static DelegateClass _functionHandle;    // Actual function pointer
public static bool FunctionName() {
    if(!_functionHandle) return;                 // @Incomplete: maybe log here?
    _functionHandle(params);
}

#if UNITY_EDITOR
const string dllPath = "Plugins/path/to/dll/file";
#else
const string dllPath = "Plugins/x86_64/path/to/dll/file";
#endif

private static IntPtr _pluginHandle = IntPtr.Zero;

public static void initPlugin() {
    if(_pluginHandle != IntPtr.Zero) return; // @Incomplete: maybe log/error here?
    _pluginHandle = ManualPluginImporter.OpenLibrary(Application.dataPath + dllPath);
    if(_pluginHandle == IntPtr.Zero) return; // @Incomplete: maybe log/error here?

    // Gets function pointers
    // for each...
    _functionHandle = ManualPluginImporter.GetDelegate<DelegateClass>(_pluginHandle, "functionHandleString");
}

public static void shutdownPlugin() {
    if(_pluginHandle == IntPtr.Zero) return; // @Incomplete: maybe log/error here?
    ManualPluginImporter.CloseLibrary(_pluginHandle);
}


// ...
_functionHandle.Invoke(params); // @Note: Invoke is used for tooltips and error checking, _functionHandle(params) would also work

*/