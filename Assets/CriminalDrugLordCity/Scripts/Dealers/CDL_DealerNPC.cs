using CriminalDrugLordCity.CDL.Core;
using CriminalDrugLordCity.CDL.Districts;
using CriminalDrugLordCity.CDL.Inventory;
using CriminalDrugLordCity.CDL.Products;
using CriminalDrugLordCity.CDL.Save;
using UnityEngine;

namespace CriminalDrugLordCity.CDL.Dealers
{
    public class CDL_DealerNPC : MonoBehaviour, ICDLInteractable
    {
        public CDL_DealerProfile profile;
        public CDL_DistrictRuntime assignedDistrict;
        public CDL_InventoryComponent inventory;
        public bool hired;
        public float saleTickSeconds = 10f;
        public float hireCost = 400f;

        private float _tickTimer;
        public string DealerId => profile != null ? profile.dealerId : name;

        private void Update()
        {
            if (!hired || assignedDistrict == null || inventory == null)
            {
                return;
            }

            _tickTimer -= Time.deltaTime;
            if (_tickTimer > 0f)
            {
                return;
            }

            _tickTimer = saleTickSeconds;
            if (inventory.slots.Count == 0)
            {
                return;
            }

            var slot = inventory.slots[0];
            inventory.RemoveItem(slot.item, 1);
            float payout = 70f + profile.skill * 60f + assignedDistrict.currentDemand * 30f;
            CDL_GameManager.Instance.AddMoney(payout);
            CDL_GameManager.Instance.AddReputation(0.5f);
            CDL_GameManager.Instance.PushMessage($"{profile.dealerName} moved stock in {assignedDistrict.definition.districtName}.");

            if (Random.value < profile.risk * 0.2f)
            {
                CDL_GameManager.Instance.AddHeat(2f, "Dealer drew attention");
            }
        }

        public string GetInteractionLabel()
        {
            return hired ? $"Manage dealer {profile.dealerName}" : $"Hire dealer {profile.dealerName} (${hireCost:0})";
        }

        public void Interact(CDL_PlayerController player)
        {
            if (!hired)
            {
                if (CDL_GameManager.Instance.SpendMoney(hireCost))
                {
                    hired = true;
                    CDL_GameManager.Instance.hiredDealers.Add(this);
                    CDL_GameManager.Instance.PushMessage($"{profile.dealerName} hired.");
                }
                return;
            }

            if (assignedDistrict == null)
            {
                assignedDistrict = FindAnyObjectByType<CDL_DistrictRuntime>();
            }

            if (player.inventory.slots.Count > 0)
            {
                var slot = player.inventory.slots[0];
                player.inventory.RemoveItem(slot.item, 3);
                inventory.AddItem(slot.item, 3);
                CDL_GameManager.Instance.PushMessage($"Gave inventory to {profile.dealerName}.");
            }
            else
            {
                CDL_GameManager.Instance.PushMessage($"{profile.dealerName} | Loyalty {profile.loyalty:0.00} | Risk {profile.risk:0.00}");
            }
        }

        public CDL_DealerSaveData BuildSave()
        {
            return new CDL_DealerSaveData
            {
                dealerId = DealerId,
                hired = hired
            };
        }

        public void ApplySave(CDL_DealerSaveData data)
        {
            if (data == null) return;
            hired = data.hired;
        }
    }
}
