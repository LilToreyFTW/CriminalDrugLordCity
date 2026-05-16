using UnityEngine;

namespace CriminalDrugLordCity.Gameplay
{
    public sealed class VehicleInteractable : MonoBehaviour
    {
        [SerializeField] private string vehicleName = "NPC Sedan";
        [SerializeField] private bool showPrompt = true;

        private bool _playerNearby;
        private GameObject _playerRef;

        private void Update()
        {
            if (_playerNearby && Input.GetKeyDown(KeyCode.F))
            {
                StealVehicle();
            }
        }

        private void StealVehicle()
        {
            if (_playerRef == null) return;

            Debug.Log("Stealing vehicle: " + vehicleName);
            
            CrimVehicleController controller = GetComponent<CrimVehicleController>();
            if (controller == null)
            {
                controller = gameObject.AddComponent<CrimVehicleController>();
            }
            
            controller.EnterVehicle(_playerRef);
            _playerNearby = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _playerNearby = true;
                _playerRef = other.gameObject;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _playerNearby = false;
            }
        }

        private void OnGUI()
        {
            if (!_playerNearby || !showPrompt)
            {
                return;
            }

            GUI.Box(new Rect(Screen.width - 280f, Screen.height - 80f, 240f, 44f), "Press F to steal " + vehicleName);
        }
    }
}
