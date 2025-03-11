using System.Runtime.InteropServices;
using UnityEngine;

public class VibrationManager : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void Vibrate(int duration);

    public void StartVibration(int duration = 200)
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        Vibrate(duration);
#endif
    }
}
