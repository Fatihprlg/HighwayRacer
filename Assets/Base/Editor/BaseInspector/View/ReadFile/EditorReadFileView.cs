using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
#if UNITY_EDITOR

namespace CubeEditor.Inspector
{
    public class EditorReadFileView : MethodAttributeViewModel
    {
        public List<FileReadView> RendererFileReads;

        public override void CheckMethod(MethodInfo method)
        {
            object[] attributeDatas = method.GetCustomAttributes(typeof(EditorReadFile), true);

            if (attributeDatas.Length > 0)
            {
                if (RendererFileReads == null)
                    RendererFileReads = new List<FileReadView>();
            }

            for (int j = 0; j < attributeDatas.Length; j++)
            {
                RendererFileReads.Add(new FileReadView(method));
            }

            base.CheckMethod(method);
        }

        public override void Draw(Object target)
        {
            if (RendererFileReads == null)
                return;

            for (int i = 0; i < RendererFileReads.Count; i++)
            {
                RendererFileReads[i].Draw(target);
            }

            base.Draw(target);
        }
    }

#endif
}
