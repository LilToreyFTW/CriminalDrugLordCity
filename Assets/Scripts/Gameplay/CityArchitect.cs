using CriminalDrugLordCity.Content;
using UnityEngine;

namespace CriminalDrugLordCity.Gameplay
{
    public static class CityArchitect
    {
        public static void BuildCity(BuildingDefinition[] buildings, GameObject apartmentPrefab, GameObject warehousePrefab)
        {
            GameObject cityRoot = new GameObject("ProceduralCity");
            string activeMapId = RuntimeGameState.ActiveMap?.id ?? "";

            if (buildings == null || buildings.Length == 0)
            {
                // Varied District Logic
                int buildingCount = activeMapId.Contains("warehouse") ? 25 : 15;
                float cityRadius = 500f;

                for (int i = 0; i < buildingCount; i++)
                {
                    Vector3 pos = new Vector3(Random.Range(-cityRadius, cityRadius), 8, Random.Range(-cityRadius, cityRadius));
                    GameObject targetPrefab = Random.value > 0.3f ? apartmentPrefab : warehousePrefab;
                    
                    if (activeMapId.Contains("mars")) targetPrefab = LoadDLCVariant(targetPrefab.name, "Mars") ?? targetPrefab;
                    else if (activeMapId.Contains("venus")) targetPrefab = LoadDLCVariant(targetPrefab.name, "Venus") ?? targetPrefab;

                    InstantiateBuilding(cityRoot.transform, pos, targetPrefab, Random.Range(10f, 25f));
                }
                return;
            }

            foreach (var b in buildings)
            {
                GameObject targetPrefab = b.architectureId.Contains("warehouse") ? warehousePrefab : apartmentPrefab;
                
                if (activeMapId.Contains("mars")) targetPrefab = LoadDLCVariant(targetPrefab.name, "Mars") ?? targetPrefab;
                else if (activeMapId.Contains("venus")) targetPrefab = LoadDLCVariant(targetPrefab.name, "Venus") ?? targetPrefab;
                
                InstantiateBuilding(cityRoot.transform, b.position.ToVector3(), targetPrefab, b.scale * 15f);
            }
        }

        private static GameObject LoadDLCVariant(string baseName, string variantSuffix)
        {
            // Simple name-based loading to avoid AssetDatabase outside Editor if possible, 
            // but since we are procedural in Unity Editor / Runtime with Assets, this works for now.
            string cleanName = baseName.Replace("(Clone)", "").Trim();
            string path = "Assets/CriminalDrugLordCity/Art/Prefabs/" + cleanName + "_" + variantSuffix + ".prefab";
            #if UNITY_EDITOR
            var prefab = UnityEditor.AssetDatabase.LoadAssetAtPath<GameObject>(path);
            if (prefab != null) return prefab;
            #endif
            return null; 
        }

        private static void InstantiateBuilding(Transform parent, Vector3 position, GameObject prefab, float scale)
        {
            if (prefab != null)
            {
                GameObject building = Object.Instantiate(prefab, position, Quaternion.identity, parent);
                building.name = prefab.name;
                building.transform.localScale = Vector3.one * scale;
            }
            else
            {
                GameObject fallback = GameObject.CreatePrimitive(PrimitiveType.Cube);
                fallback.name = "BuildingFallback";
                fallback.transform.SetParent(parent);
                fallback.transform.position = position;
                fallback.transform.localScale = new Vector3(12, 25, 12);
            }
        }
    }
}
