#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEditor;
using UnityEngine.Rendering;


[CustomEditor(typeof(PostProcessor))]
public class PostProcessorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        PostProcessor postProcessor = target as PostProcessor;
        Volume volume = postProcessor.volume;
        if (postProcessor == null || volume == null || postProcessor.Profiles == null) return;
        for (int i = 0; i < postProcessor.Profiles.Count; i++)
        {
            if (postProcessor.Profiles[i] != null)
                if (GUILayout.Button(postProcessor.Profiles[i].name))
                {
                    volume.profile = postProcessor.Profiles[i];
                }
        }
        
    }
}
#endif