using CriminalDrugLordCity.Content;
using CriminalDrugLordCity.Gameplay;
using UnityEditor;
using UnityEngine;
using System.IO;

namespace CriminalDrugLordCity.EditorTools
{
    public static class CityMapTools
    {
        [MenuItem("Criminal Drug Lord City/Build Actual Land")]
        public static void BuildLand()
        {
            string path = Path.Combine(Application.streamingAssetsPath, "maps", "metro_city_4096x2056.json");
            if (!File.Exists(path))
            {
                Debug.LogError("Map JSON not found at: " + path);
                return;
            }

            string json = File.ReadAllText(path);
            MapDefinition map = JsonUtility.FromJson<MapDefinition>(json);
            Material terrainMat = AssetDatabase.LoadAssetAtPath<Material>("Assets/Materials/Terrain_Mat.mat");
            
            TerrainGenerator.Build(map, terrainMat);
            
            GameObject oldGround = GameObject.Find("CityGround");
            if (oldGround != null) Object.DestroyImmediate(oldGround);

            Debug.Log("Actual Land constructed successfully for: " + map.displayName);
        }

        [MenuItem("Criminal Drug Lord City/Build All DLC Lands")]
        public static void BuildDlcLands()
        {
            string mapsDir = Path.Combine(Application.streamingAssetsPath, "maps");
            string[] files = Directory.GetFiles(mapsDir, "*.json");

            foreach (var file in files)
            {
                if (file.Contains("metro_city")) continue; 

                string json = File.ReadAllText(file);
                MapDefinition map = JsonUtility.FromJson<MapDefinition>(json);
                Material terrainMat = AssetDatabase.LoadAssetAtPath<Material>("Assets/Materials/Terrain_Mat.mat");
                
                Debug.Log("Building DLC Land Template for: " + map.displayName);
                TerrainGenerator.Build(map, terrainMat);
            }
        }

        [MenuItem("Criminal Drug Lord City/Snap Buildings to Terrain")]
        public static void SnapBuildings()
        {
            GameObject cityRoot = GameObject.Find("ProceduralCity");
            if (cityRoot == null) return;

            Terrain terrain = Object.FindAnyObjectByType<Terrain>();
            if (terrain == null) return;

            Undo.RecordObjects(cityRoot.GetComponentsInChildren<Transform>(), "Snap Buildings");

            int count = 0;
            foreach (Transform t in cityRoot.GetComponentsInChildren<Transform>())
            {
                if (t == cityRoot.transform) continue;
                float y = terrain.SampleHeight(t.position);
                t.position = new Vector3(t.position.x, y, t.position.z);
                count++;
            }

            Debug.Log($"Snapped {count} city objects to terrain height.");
        }
    }
}