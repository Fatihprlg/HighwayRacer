using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public static class DebugHelper 
{
    public static void PauseEditor()
    {
#if UNITY_EDITOR
        EditorApplication.isPaused = true;
#endif
    }

    public static void PlayEditor()
    {
#if UNITY_EDITOR
        EditorApplication.isPaused = false;
        EditorApplication.isPlaying = true;
#endif
    }
}
