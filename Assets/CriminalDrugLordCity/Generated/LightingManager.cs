using UnityEngine;

[ExecuteAlways]
public class LightingManager : MonoBehaviour
{
    public LightingPreset preset;
    public Light sunLight;

    void Update()
    {
        if (preset == null) return;

        if (sunLight != null)
        {
            sunLight.color = preset.sunColor;
            sunLight.intensity = preset.sunIntensity;
            sunLight.transform.localRotation = Quaternion.Euler(preset.sunRotation);
        }

        RenderSettings.ambientSkyColor = preset.skyColor;
        RenderSettings.ambientEquatorColor = preset.equatorColor;
        RenderSettings.ambientGroundColor = preset.groundColor;
        RenderSettings.fogColor = preset.fogColor;
        RenderSettings.fogDensity = preset.fogDensity;
    }
}
