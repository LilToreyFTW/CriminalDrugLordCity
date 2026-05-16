using CriminalDrugLordCity.Content;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace CriminalDrugLordCity.Gameplay
{
    public static class TerrainGenerator
    {
        public static void Build(MapDefinition map, Material terrainMaterial)
        {
            GameObject root = EnsureRoot("MapTerrain");
            
            Terrain terrain = root.GetComponent<Terrain>();
            if (terrain == null) terrain = root.AddComponent<Terrain>();
            
            TerrainCollider collider = root.GetComponent<TerrainCollider>();
            if (collider == null) collider = root.AddComponent<TerrainCollider>();

            TerrainData terrainData = new TerrainData();
            terrainData.heightmapResolution = 1025; 
            terrainData.size = new Vector3(map.width, 400, map.height);
            
            float[,] heights = new float[terrainData.heightmapResolution, terrainData.heightmapResolution];
            Vector2 cityCenter = new Vector2(0.5f, 0.5f); 
            float cityRadius = 0.25f;

            for (int i = 0; i < terrainData.heightmapResolution; i++)
            {
                for (int j = 0; j < terrainData.heightmapResolution; j++)
                {
                    float x_norm = (float)i / (terrainData.heightmapResolution - 1);
                    float y_norm = (float)j / (terrainData.heightmapResolution - 1);
                    
                    float distFromCenter = Vector2.Distance(new Vector2(x_norm, y_norm), new Vector2(0.5f, 0.5f));
                    
                    // Irregular island shape using Perlin
                    float edgeNoise = Mathf.PerlinNoise(x_norm * 5f, y_norm * 5f) * 0.15f;
                    float continent = Mathf.SmoothStep(0.5f + edgeNoise, 0.4f + edgeNoise, distFromCenter);
                    
                    // Rugged Hills
                    float hills = (Mathf.PerlinNoise(x_norm * 4f, y_norm * 4f) * 0.5f + 
                                   Mathf.PerlinNoise(x_norm * 10f, y_norm * 10f) * 0.3f + 
                                   Mathf.PerlinNoise(x_norm * 25f, y_norm * 25f) * 0.2f);
                    
                    float height = hills * continent;

                    // Flatten and Raise City Plateau
                    float distToCity = Vector2.Distance(new Vector2(x_norm, y_norm), cityCenter);
                    if (distToCity < cityRadius)
                    {
                        float cityFlatten = Mathf.SmoothStep(0, 1, distToCity / cityRadius);
                        height = Mathf.Lerp(0.08f, height, cityFlatten); // City plateau at ~32 units
                    }
                    
                    heights[i, j] = height;
                    }
                    }
                    terrainData.SetHeights(0, 0, heights);

                    #if UNITY_EDITOR
                    TerrainLayer[] layers = new TerrainLayer[3];
                    layers[0] = AssetDatabase.LoadAssetAtPath<TerrainLayer>("Assets/Materials/TerrainLayers/Asphalt.terrainlayer");
                    layers[1] = AssetDatabase.LoadAssetAtPath<TerrainLayer>("Assets/Materials/TerrainLayers/Grass.terrainlayer");
                    layers[2] = AssetDatabase.LoadAssetAtPath<TerrainLayer>("Assets/Materials/TerrainLayers/Dirt.terrainlayer");
            
                    if (layers[0] != null) terrainData.terrainLayers = layers;

                    // Painting Layers
                    float[,,] alphaMaps = new float[terrainData.alphamapWidth, terrainData.alphamapHeight, 3];
                    for (int y = 0; y < terrainData.alphamapHeight; y++)
                    {
                    for (int x = 0; x < terrainData.alphamapWidth; x++)
                    {
                    float h = heights[x, y];
                    float x_f = (float)x / terrainData.alphamapWidth;
                    float y_f = (float)y / terrainData.alphamapHeight;
                    float dCity = Vector2.Distance(new Vector2(x_f, y_f), cityCenter);

                    if (dCity < cityRadius * 0.8f)
                    {
                        alphaMaps[x, y, 0] = 1f; // Solid Asphalt
                    }
                    else if (h > 0.02f)
                    {
                        alphaMaps[x, y, 1] = 0.8f; // Grass
                        alphaMaps[x, y, 2] = 0.2f; // Dirt mix
                    }
                    else
                    {
                        alphaMaps[x, y, 2] = 1f; // Beach/Edge Dirt
                    }
                    }
                    }

                    // Paint road/asphalt under preserved buildings
                    foreach (var b in map.cityBuildings)
                    {
                    int mapX = Mathf.RoundToInt((b.position[0] / map.width) * terrainData.alphamapWidth);
                    int mapY = Mathf.RoundToInt((b.position[2] / map.height) * terrainData.alphamapHeight);
                
                    for (int dy = -4; dy <= 4; dy++)
                    {
                    for (int dx = -4; dx <= 4; dx++)
                    {
                        int tx = Mathf.Clamp(mapX + dx, 0, terrainData.alphamapWidth - 1);
                        int ty = Mathf.Clamp(mapY + dy, 0, terrainData.alphamapHeight - 1);
                        alphaMaps[tx, ty, 0] = 1f;
                        alphaMaps[tx, ty, 1] = 0f;
                        alphaMaps[tx, ty, 2] = 0f;
                    }
                    }
                    }
                    terrainData.SetAlphamaps(0, 0, alphaMaps);
                    #endif

            terrain.terrainData = terrainData;
            collider.terrainData = terrainData;
            
            if (terrainMaterial != null)
            {
                terrain.materialTemplate = terrainMaterial;
            }
        }

        private static GameObject EnsureRoot(string name)
        {
            GameObject root = GameObject.Find(name);
            if (root == null)
            {
                root = new GameObject(name);
                root.transform.position = Vector3.zero;
            }
            return root;
        }
    }
}
