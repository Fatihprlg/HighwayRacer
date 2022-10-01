using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
#if UNITY_EDITOR

namespace CubeEditor.Inspector
{
    public class EditorWriteFileView : MethodAttributeViewModel
    {
        public List<FileWriteView> RendererFileWrites;

        public override void CheckMethod(MethodInfo method)
        {
            object[] attributeDatas = method.GetCustomAttributes(typeof(EditorWriteFile), true);

            if (attributeDatas.Length > 0)
            {
                if (RendererFileWrites == null)
                    RendererFileWrites = new List<FileWriteView>();
            }

            for (int j = 0; j < attributeDatas.Length; j++)
            {
                RendererFileWrites.Add(new FileWriteView(method));
            }

            base.CheckMethod(method);
        }

        public override void Draw(Object target)
        {
            if (RendererFileWrites == null)
                return;

            for (int i = 0; i < RendererFileWrites.Count; i++)
            {
                RendererFileWrites[i].Draw(target);
            }

            base.Draw(target);
        }
    }
}

#endif