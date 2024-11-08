using UnityEngine;

public class FPSLimiter : MonoBehaviour
{
    [SerializeField] private int targetFrameRate = 60;
    [SerializeField] private bool vSyncEnabled = false;
    [SerializeField] private bool allowDynamicChange = true;

    private void Awake()
    {
        Apply();
    }

    private void Apply()
    {
        // Set VSync (0 = disabled, 1 = enabled)
        QualitySettings.vSyncCount = vSyncEnabled ? 1 : 0;

        // Set target frame rate (-1 = unlimited)
        Application.targetFrameRate = targetFrameRate;
    }

    // Optional: Allow changing FPS limit at runtime
    public void SetFrameRate(int newTargetFrameRate)
    {
        if (!allowDynamicChange) return;

        targetFrameRate = newTargetFrameRate;
        Apply();
    }

    public void SetVSync(bool enabled)
    {
        if (!allowDynamicChange) return;

        vSyncEnabled = enabled;
        Apply();
    }

    // Optional: Add to Windows Project Settings under Edit > Project Settings > Player > Resolution and Presentation
    private void OnValidate()
    {
        if (Application.isPlaying && allowDynamicChange)
        {
            Apply();
        }
    }
}