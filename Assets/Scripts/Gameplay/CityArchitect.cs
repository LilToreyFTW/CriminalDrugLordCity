using CriminalDrugLordCity.Content;
using UnityEngine;

namespace CriminalDrugLordCity.Gameplay
{
    public static class CityArchitect
    {
        public static void BuildCity(BuildingDefinition[] buildings, GameObject apartmentPrefab, GameObject warehousePrefab)
        {
            GameObject cityRoot = new GameObject("ProceduralCity");

            if (buildings == null || buildings.Length == 0)
            {
                // Fallback residential district
                for (int i = 0; i < 15; i++)
                {
                    Vector3 pos = new Vector3(Random.Range(50, 400), 8, Random.Range(50, 400));
                    InstantiateBuilding(cityRoot.transform, pos, apartmentPrefab, 15f);
                }
                
                // Fallback industrial district
                for (int i = 0; i < 10; i++)
                {
                    Vector3 pos = new Vector3(Random.Range(600, 1000), 8, Random.Range(50, 600));
                    InstantiateBuilding(cityRoot.transform, pos, warehousePrefab, 20f);
                }
                return;
            }

            foreach (var b in buildings)
            {
                GameObject targetPrefab = b.architectureId.Contains("warehouse") ? warehousePrefab : apartmentPrefab;
                InstantiateBuilding(cityRoot.transform, b.position.ToVector3(), targetPrefab, b.scale * 15f);
            }
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
