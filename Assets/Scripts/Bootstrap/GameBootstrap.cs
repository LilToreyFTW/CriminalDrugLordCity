using CriminalDrugLordCity.Content;
using CriminalDrugLordCity.Gameplay;
using System.IO;
using UnityEngine;

namespace CriminalDrugLordCity.Bootstrap
{
    public sealed class GameBootstrap : MonoBehaviour
    {
        [SerializeField] private TextAsset mapJson;
        [SerializeField] private TextAsset dlcJson;
        [SerializeField] private Material terrainMaterial;
        [SerializeField] private Material propMaterial;
        [SerializeField] private GameObject playerPrefab;
        [SerializeField] private GameObject gunPrefab;
        [SerializeField] private GameObject carPrefab;
        [SerializeField] private GameObject dealershipPrefab;
        [SerializeField] private GameObject apartmentPrefab;
        [SerializeField] private GameObject warehousePrefab;
        [SerializeField] private GameObject thugPrefab;
[SerializeField] private GameObject citizenPrefab;
        [SerializeField] private GameObject hudPrefab;
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private GameObject muzzleFlashPrefab;
        [SerializeField] private GameObject policePrefab;
        private static bool _started;

        private void Start()
        {
            if (_started)
            {
                return;
            }

            _started = true;
            EnsureSceneScaffold();

            string mapText = ResolveMapJson();
            string dlcText = ResolveDlcJson();

            if (string.IsNullOrEmpty(mapText) || string.IsNullOrEmpty(dlcText))
            {
                Debug.LogWarning("Unable to load mapJson or dlcJson.");
                return;
            }

            MapDefinition map = JsonUtility.FromJson<MapDefinition>(mapText);
            DlcDefinition[] dlcList = JsonArrayHelper.FromJson<DlcDefinition>(dlcText);

            RuntimeGameState.ActiveMap = map;
            RuntimeGameState.SeasonalDlcs = dlcList;
            RuntimeGameState.PlayerState = new PlayerRuntimeState();

            // Initialize Managers
            if (FindAnyObjectByType<DlcManager>() == null)
            {
                var dlcManager = gameObject.AddComponent<DlcManager>();
                dlcManager.activeDlcs.AddRange(dlcList);
            }
            if (FindAnyObjectByType<MissionManager>() == null) gameObject.AddComponent<MissionManager>();

            TerrainGenerator.Build(map, terrainMaterial);
ArsenalRoomBuilder.Build(map.arsenalRoom, propMaterial, gunPrefab);
            VehicleSpawnBuilder.Build(map.vehicleSpawns, propMaterial, carPrefab);
            PlayerBuilder.Build(map.spawnPoint, propMaterial, playerPrefab, bulletPrefab, muzzleFlashPrefab);
            DealershipBuilder.Build(new Vector3(300, 8, 300), dealershipPrefab);
            CityArchitect.BuildCity(map.cityBuildings, apartmentPrefab, warehousePrefab);
            NpcSpawner.SpawnNpcs(thugPrefab, citizenPrefab, 15);
NpcSpawner.SpawnPolice(policePrefab, 5);
            NpcCarSpawner.SpawnNpcCars(carPrefab, 10);
            CrimHUD.CreateHUD(hudPrefab);
            DlcBillboardBuilder.Build(dlcList);
            ImportedAssetSpawner.BuildFallbackDistrict();

            ApplySeasonalEnvironment(dlcList);

            gameObject.AddComponent<OffsetDisplay>();
        }

        private void ApplySeasonalEnvironment(DlcDefinition[] dlcs)
        {
            if (dlcs == null || dlcs.Length == 0) return;

            // Pick the first active DLC season to set the mood
            string season = dlcs[0].season.ToLower();
            Light sun = FindAnyObjectByType<Light>();
            
            if (sun != null)
            {
                if (season.Contains("fall") || season.Contains("autumn"))
                {
                    sun.color = new Color(1f, 0.7f, 0.4f); // Golden hour
                    RenderSettings.fogColor = new Color(0.4f, 0.3f, 0.2f);
                    RenderSettings.fog = true;
                }
                else if (season.Contains("winter"))
                {
                    sun.color = new Color(0.8f, 0.9f, 1f); // Cold blue
                    RenderSettings.fogColor = Color.white;
                    RenderSettings.fog = true;
                }
                else if (season.Contains("venus") || season.Contains("mars"))
                {
                    sun.color = new Color(1f, 0.4f, 0.2f); // Alien red/orange
                    RenderSettings.fogColor = new Color(0.5f, 0.1f, 0f);
                    RenderSettings.fog = true;
                }
            }
        }

        private string ResolveMapJson()
        {
            if (mapJson != null)
            {
                return mapJson.text;
            }

            string path = Path.Combine(Application.streamingAssetsPath, "maps", "metro_city_4096x2056.json");
            return File.Exists(path) ? File.ReadAllText(path) : string.Empty;
        }

        private string ResolveDlcJson()
        {
            if (dlcJson != null)
            {
                return dlcJson.text;
            }

            string path = Path.Combine(Application.streamingAssetsPath, "dlc", "season_manifest.json");
            return File.Exists(path) ? File.ReadAllText(path) : string.Empty;
        }

        private void EnsureSceneScaffold()
        {
            if (Camera.main == null)
            {
                GameObject cameraObject = new GameObject("Main Camera");
                cameraObject.tag = "MainCamera";
                Camera cameraComponent = cameraObject.AddComponent<Camera>();
                cameraComponent.clearFlags = CameraClearFlags.Skybox;
                cameraObject.AddComponent<AudioListener>();
                SimpleCameraController controller = cameraObject.AddComponent<SimpleCameraController>();
                controller.enabled = true;
            }

            if (FindAnyObjectByType<Light>() == null)
            {
                GameObject lightObject = new GameObject("Directional Light");
                Light lightComponent = lightObject.AddComponent<Light>();
                lightComponent.type = LightType.Directional;
                lightComponent.intensity = 1.2f;
                lightObject.transform.rotation = Quaternion.Euler(50f, -30f, 0f);
            }
        }
    }
}
