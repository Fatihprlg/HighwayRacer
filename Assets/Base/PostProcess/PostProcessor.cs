#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(Volume))]
public class PostProcessor : MonoBehaviour
{
    public Volume volume;
    public List<VolumeProfile> Profiles;

    private void Reset()
    {
        volume = GetComponent<Volume>();
    }
}
#endif