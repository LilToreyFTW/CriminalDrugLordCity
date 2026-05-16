using CriminalDrugLordCity.CDL.Inventory;
using UnityEngine;

namespace CriminalDrugLordCity.CDL.Core
{
    public class CDL_WeaponPickup : MonoBehaviour, ICDLInteractable
    {
        public CDL_ItemDefinition weaponItem;

        public string GetInteractionLabel() => $"Pick up {weaponItem.itemName}";

        public void Interact(CDL_PlayerController player)
        {
            if (player.inventory.AddItem(weaponItem, 1))
            {
                CDL_GameManager.Instance.PushMessage($"{weaponItem.itemName} picked up.");
                Destroy(gameObject);
            }
        }
    }

    public class CDL_PlayerCombat : MonoBehaviour
    {
        public CDL_PlayerController player;
        public float meleeRange = 2.2f;
        public float fistsDamage = 10f;
        public float meleeDamage = 20f;
        public float firearmDamage = 30f;

        private void Update()
        {
            if (player == null || player.IsDriving)
            {
                return;
            }

            if (Input.GetMouseButtonDown(0))
            {
                TryAttack();
            }
        }

        private void TryAttack()
        {
            float damage = fistsDamage;
            bool firearm = false;
            foreach (var slot in player.inventory.slots)
            {
                if (slot.item == null || !slot.item.isWeapon) continue;
                if (slot.item.itemName.Contains("Firearm"))
                {
                    damage = firearmDamage;
                    firearm = true;
                    break;
                }

                if (slot.item.itemName.Contains("Melee"))
                {
                    damage = meleeDamage;
                }
            }

            if (Physics.Raycast(player.playerCamera.transform.position, player.playerCamera.transform.forward, out RaycastHit hit, meleeRange + (firearm ? 18f : 0f)))
            {
                var damageable = hit.collider.GetComponentInParent<ICDLDamageable>();
                if (damageable != null && hit.collider.gameObject != player.gameObject)
                {
                    damageable.ApplyDamage(damage, gameObject);
                    if (firearm)
                    {
                        CDL_GameManager.Instance.AddHeat(6f, "Gunfire");
                    }
                }
            }
        }
    }
}
