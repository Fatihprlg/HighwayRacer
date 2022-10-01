using UnityEngine;
using System.Reflection;
#if UNITY_EDITOR
using UnityEditor;

namespace CubeEditor.Inspector
{
    public class FileReadView
    {
        public MethodInfo Method;
        public TextAsset FileText;

        public FileReadView(MethodInfo method)
        {
            Method = method;
        }

        public void Draw(object target)
        {
            ParameterInfo[] parameters = Method.GetParameters();
            ParameterInfo targetParameter = parameters.Find(x => x.Name == "file" && x.ParameterType == typeof(string));

            if (targetParameter == null)
            {
                return;
            }

            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

            FileText = (TextAsset)EditorGUILayout.ObjectField("File", FileText, typeof(TextAsset), false);

            if (GUILayout.Button("Read File"))
            {
                object[] objParameters = new object[1];
                objParameters[0] = FileText.text;

                Method.Invoke(target, objParameters);
            }

            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
        }
    }

#endif
}
