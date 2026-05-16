using CriminalDrugLordCity.CDL.Core;
using UnityEngine;

namespace CriminalDrugLordCity.CDL.Suppliers
{
    public class CDL_SupplierNPC : MonoBehaviour, ICDLInteractable
    {
        public CDL_SupplierDefinition definition;
        private float _restockTimer;

        private void Update()
        {
            if (definition == null) return;
            _restockTimer -= Time.deltaTime;
            if (_restockTimer <= 0f)
            {
                _restockTimer = definition.restockSeconds;
                foreach (var entry in definition.stock)
                {
                    entry.stock = Mathf.Min(entry.stock + 2, 20);
                }
            }
        }

        public string GetInteractionLabel()
        {
            return $"Trade with {definition.supplierName}";
        }

        public void Interact(CDL_PlayerController player)
        {
            if (definition == null || definition.stock.Length == 0)
            {
                return;
            }

            var entry = definition.stock[0];
            if (entry.stock <= 0)
            {
                CDL_GameManager.Instance.PushMessage("Supplier is out of stock.");
                return;
            }

            float price = entry.price;
            int bulkAmount = 1;
            if (Input.GetKey(KeyCode.LeftShift))
            {
                bulkAmount = 5;
                price *= bulkAmount * (1f - definition.bulkDiscountPercent);
            }

            bulkAmount = Mathf.Min(bulkAmount, entry.stock);
            if (CDL_GameManager.Instance.SpendMoney(price))
            {
                player.inventory.AddItem(entry.item, bulkAmount);
                entry.stock -= bulkAmount;
                CDL_GameManager.Instance.PushMessage($"Bought {bulkAmount}x {entry.item.itemName}.");
            }
        }
    }
}
