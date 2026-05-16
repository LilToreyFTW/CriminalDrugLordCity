using CriminalDrugLordCity.Bootstrap;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace CriminalDrugLordCity.EditorTools
{
    public static class CitySceneBuilder
    {
        private const string ScenePath = "Assets/Scenes/Main.unity";

        [MenuItem("Criminal Drug Lord City/Build Starter Scene")]
        public static void BuildStarterScene()
        {
            EditorSceneManager.SaveOpenScenes();
            var scene = EditorSceneManager.OpenScene(ScenePath, OpenSceneMode.Single);

            ClearScene(scene);

            GameObject bootstrap = new GameObject("GameBootstrap");
            var bootstrapComponent = bootstrap.AddComponent<GameBootstrap>();
            AssignBootstrapAssets(bootstrapComponent);

            GameObject director = new GameObject("CityGameplayDirector");
            director.AddComponent<CriminalDrugLordCity.Gameplay.CityGameplayDirector>();

            GameObject light = new GameObject("Directional Light");
            Light directional = light.AddComponent<Light>();
            directional.type = LightType.Directional;
            directional.intensity = 1.2f;
            light.transform.rotation = Quaternion.Euler(50f, -30f, 0f);

            GameObject ground = GameObject.CreatePrimitive(PrimitiveType.Plane);
            ground.name = "RoadGrid";
            ground.transform.position = Vector3.zero;
            ground.transform.localScale = new Vector3(20f, 1f, 20f);

            EditorSceneManager.MarkSceneDirty(scene);
            EditorSceneManager.SaveScene(scene);

            Debug.Log("Starter scene built. Press Play to generate the city block.");
        }

        private static void AssignBootstrapAssets(GameBootstrap bootstrap)
        {
            SerializedObject serialized = new SerializedObject(bootstrap);
            serialized.FindProperty("mapJson").objectReferenceValue = AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/StreamingAssets/maps/metro_city_4096x2056.json");
            serialized.FindProperty("dlcJson").objectReferenceValue = AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/StreamingAssets/dlc/season_manifest.json");
            serialized.ApplyModifiedPropertiesWithoutUndo();
        }

        private static void ClearScene(UnityEngine.SceneManagement.Scene scene)
        {
            GameObject[] roots = scene.GetRootGameObjects();
            for (int i = 0; i < roots.Length; i++)
            {
                Object.DestroyImmediate(roots[i]);
            }
        }
    }
}
