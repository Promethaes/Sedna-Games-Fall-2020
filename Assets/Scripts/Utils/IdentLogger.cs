using System.Diagnostics;

class Logger {

    private static string getCallerName() {
        var callingMethod = new StackTrace().GetFrame(2).GetMethod();
        return callingMethod.ReflectedType.Name + ":" + callingMethod.Name;
    }

    public static void Log(object log) {
        UnityEngine.Debug.LogFormat("[{0}] {1}", getCallerName(), log.ToString());
    }

    public static void Log(string fmt, params object[] args) {
        UnityEngine.Debug.LogFormat("[" + getCallerName() + "] " + fmt, args);
    }

    public static void Warning(object log) {
        UnityEngine.Debug.LogWarningFormat("[{0}] {1}", getCallerName(), log.ToString());
    }

    public static void Warning(string fmt, params object[] args) {
        UnityEngine.Debug.LogWarningFormat("[" + getCallerName() + "] " + fmt, args);
    }

    public static void Error(object log) {
        UnityEngine.Debug.LogErrorFormat("[{0}] {1}", getCallerName(), log.ToString());
    }

    public static void Error(string fmt, params object[] args) {
        UnityEngine.Debug.LogErrorFormat("[" + getCallerName() + "] " + fmt, args);
    }

    public static void Assert(bool condition, object log) {
        UnityEngine.Debug.AssertFormat(condition, "[{0}] {1}", getCallerName(), log.ToString());
    }

    public static void Assert(bool condition, string fmt, params object[] args) {
        UnityEngine.Debug.AssertFormat(condition, "[" + getCallerName() + "] " + fmt, args);
    }
}