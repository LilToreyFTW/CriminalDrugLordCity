using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using CriminalDrugLordCity.Bootstrap;

namespace CriminalDrugLordCity.Gameplay
{
    public class PhoneManager : MonoBehaviour
    {
        public static PhoneManager Instance { get; private set; }
        
        [Header("UI Panels")]
        public GameObject phoneContainer;
        public GameObject mapPanel;
        public GameObject inventoryPanel;
        public GameObject missionPanel;

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);
        }

        public void TogglePhone()
        {
            phoneContainer.SetActive(!phoneContainer.activeSelf);
            Cursor.lockState = phoneContainer.activeSelf ? CursorLockMode.None : CursorLockMode.Locked;
            Cursor.visible = phoneContainer.activeSelf;
        }

        [Header("App Content")]
        public Transform mapGrid;
        public GameObject travelButtonPrefab;

        private void Start()
        {
            PopulateMapApp();
        }

        private void PopulateMapApp()
        {
            string path = System.IO.Path.Combine(Application.streamingAssetsPath, "maps");
            if (System.IO.Directory.Exists(path))
            {
                string[] files = System.IO.Directory.GetFiles(path, "*.json");
                foreach (string file in files)
                {
                    string mapId = System.IO.Path.GetFileNameWithoutExtension(file);
                    GameObject btn = Instantiate(travelButtonPrefab, mapGrid);
                    btn.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = mapId.Replace("_", " ").ToUpper();
                    btn.GetComponent<Button>().onClick.AddListener(() => TravelTo(mapId));
                }
            }
        }

        public void TravelTo(string mapId)
        {
            var bootstrap = FindAnyObjectByType<GameBootstrap>();
            if (bootstrap != null)
            {
                bootstrap.LoadMap(mapId);
                TogglePhone(); // Close phone after travel
            }
        }
    }
}
