using UnityEngine;

[CreateAssetMenu(fileName = "New Lighting Preset", menuName = "CriminalDrugLordCity/Lighting Preset")]
public class LightingPreset : ScriptableObject
{
    public Color skyColor = Color.blue;
    public Color equatorColor = Color.gray;
    public Color groundColor = Color.black;
    public Color sunColor = Color.white;
    public float sunIntensity = 1.0f;
    public Vector3 sunRotation = new Vector3(50, -30, 0);
    public float fogDensity = 0.01f;
    public Color fogColor = Color.gray;
}
