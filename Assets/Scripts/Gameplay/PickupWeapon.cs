using UnityEngine;

namespace CriminalDrugLordCity.Gameplay
{
    public sealed class PickupWeapon : MonoBehaviour
    {
        [SerializeField] private string weaponName = "Street Pistol";

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player"))
            {
                return;
            }

            if (RuntimeGameState.PlayerState == null)
            {
                RuntimeGameState.PlayerState = new PlayerRuntimeState();
            }

            RuntimeGameState.PlayerState.EquippedWeapon = weaponName;
            gameObject.SetActive(false);
        }
    }
}
