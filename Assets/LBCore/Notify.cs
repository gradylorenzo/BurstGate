using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Notify
{
    public enum Intent
    {
        Success = 0,
        Message = 1,
        Warning = 2,
        Error = 4
    }

    public static void Log(Intent intent, string text)
    {
        bool useDebugLog = false;

        if (useDebugLog)
        {
            switch (intent)
            {
                case Intent.Success:
                    Debug.Log(text);
                    break;

                case Intent.Message:
                    Debug.Log(text);
                    break;

                case Intent.Warning:
                    Debug.LogWarning(text);
                    break;

                case Intent.Error:
                    Debug.LogError(text);
                    break;
            }
        }

        ENotifyLog(intent, text);
    }

    public delegate void e_NotifyLog(Intent intent, string text);
    public static e_NotifyLog ENotifyLog;
}
