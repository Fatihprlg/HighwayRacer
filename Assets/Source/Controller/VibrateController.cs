using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.NiceVibrations;

public class VibrateController : ControllerBaseModel
{
    static float lastVibrationTime;

    public static void SetHaptic(VibrationTypes type)
    {
        switch (type)
        {
            case VibrationTypes.Light:
                MMVibrationManager.Haptic(HapticTypes.LightImpact);
                break;
            case VibrationTypes.Medium:
                MMVibrationManager.Haptic(HapticTypes.MediumImpact);
                break;
            case VibrationTypes.Heavy:
                MMVibrationManager.Haptic(HapticTypes.HeavyImpact);
                break;
            case VibrationTypes.Succes:
                MMVibrationManager.Haptic(HapticTypes.Success);
                break;
            case VibrationTypes.Fail:
                MMVibrationManager.Haptic(HapticTypes.Failure);
                break;
            case VibrationTypes.RigidImpact:
                MMVibrationManager.Haptic(HapticTypes.RigidImpact);
                break;
            case VibrationTypes.Soft:
                MMVibrationManager.Haptic(HapticTypes.SoftImpact);
                break;
            case VibrationTypes.Warning:
                MMVibrationManager.Haptic(HapticTypes.Warning);
                break;
            default:
                break;
        }
    }

    public static void SetHaptic(VibrationTypes type, float threshold)
    {
        if (Time.time < lastVibrationTime + threshold)
            return;

        lastVibrationTime = Time.time;
        switch (type)
        {
            case VibrationTypes.Light:
                MMVibrationManager.Haptic(HapticTypes.LightImpact);
                break;
            case VibrationTypes.Medium:
                MMVibrationManager.Haptic(HapticTypes.MediumImpact);
                break;
            case VibrationTypes.Heavy:
                MMVibrationManager.Haptic(HapticTypes.HeavyImpact);
                break;
            case VibrationTypes.Succes:
                MMVibrationManager.Haptic(HapticTypes.Success);
                break;
            case VibrationTypes.Fail:
                MMVibrationManager.Haptic(HapticTypes.Failure);
                break;
            case VibrationTypes.RigidImpact:
                MMVibrationManager.Haptic(HapticTypes.RigidImpact);
                break;
            case VibrationTypes.Soft:
                MMVibrationManager.Haptic(HapticTypes.SoftImpact);
                break;
            case VibrationTypes.Warning:
                MMVibrationManager.Haptic(HapticTypes.Warning);
                break;
            default:
                break;
        }
    }
}
