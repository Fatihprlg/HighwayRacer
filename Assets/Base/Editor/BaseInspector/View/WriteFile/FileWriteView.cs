using UnityEngine;
using System.Reflection;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;

namespace CubeEditor.Inspector
{
    public class FileWriteView
    {
        public MethodInfo Method;
        public List<ParameterView> Parameters;

        public FileWriteView(MethodInfo method)
        {
            Method = method;
            ParameterInfo[] parameters = method.GetParameters();
            Parameters = new List<ParameterView>();

            for (int i = 0; i < parameters.Length; i++)
            {
                Parameters.Add(new ParameterView(parameters[i]));
            }
        }

        public void Draw(object target)
        {
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

            if (Parameters.Count > 0)
            {
                for (int i = 0; i < Parameters.Count; i++)
                {
                    Parameters[i].Draw();
                }
            }

            if (GUILayout.Button("Write - " + Method.Name))
            {
                string path = EditorUtility.SaveFilePanel("Save Path", "", "", "");

                if (string.IsNullOrEmpty(path) == false)
                {
                    string content = Method.Invoke(target, Parameters.GetParameterValues()).ToString();
                    System.IO.File.WriteAllText(path, content);
                    AssetDatabase.Refresh();
                }
            }

            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
        }

    }
}

#endif