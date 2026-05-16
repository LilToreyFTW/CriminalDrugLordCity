using UnityEngine;

namespace CriminalDrugLordCity.Gameplay
{
    public sealed class DealershipInteractable : MonoBehaviour
    {
        private bool _playerNearby;
        private string _shopStatus = "WELCOME TO THE LUXURY DEALERSHIP";

        private void Update()
        {
            if (_playerNearby && Input.GetKeyDown(KeyCode.E))
            {
                BuyCar();
            }
        }

        private void BuyCar()
        {
            if (RuntimeGameState.PlayerState == null) return;

            if (RuntimeGameState.PlayerState.Cash >= 10000)
            {
                RuntimeGameState.PlayerState.Cash -= 10000;
                _shopStatus = "PURCHASE SUCCESSFUL! NEW LUXURY CAR DELIVERED.";
                Debug.Log("Bought a new car!");
                // Spawning logic could go here
            }
            else
            {
                _shopStatus = "INSUFFICIENT FUNDS. NEED MORE RESPECT.";
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player")) _playerNearby = true;
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _playerNearby = false;
                _shopStatus = "WELCOME TO THE LUXURY DEALERSHIP";
            }
        }

        private void OnGUI()
        {
            if (!_playerNearby) return;

            GUI.Box(new Rect(Screen.width / 2 - 150, Screen.height / 2 - 50, 300, 100), _shopStatus);
            GUI.Label(new Rect(Screen.width / 2 - 140, Screen.height / 2, 280, 40), "Press E to buy Luxury Sport for $10,000");
        }
    }
}
