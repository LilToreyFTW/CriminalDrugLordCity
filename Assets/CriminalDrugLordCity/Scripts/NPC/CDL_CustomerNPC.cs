using CriminalDrugLordCity.CDL.Core;
using CriminalDrugLordCity.CDL.Inventory;
using CriminalDrugLordCity.CDL.Products;
using UnityEngine;

namespace CriminalDrugLordCity.CDL.NPC
{
    public class CDL_CustomerNPC : MonoBehaviour, ICDLInteractable, ICDLDamageable
    {
        public string customerName = "Customer";
        public CDL_ItemDefinition desiredItem;
        public CDL_ProductDefinition desiredProduct;
        public float patienceTimer = 120f;
        public float trustLevel = 0.5f;
        public float offerPrice = 120f;
        public float health = 30f;

        private void Update()
        {
            patienceTimer -= Time.deltaTime;
            if (patienceTimer <= 0f)
            {
                patienceTimer = Random.Range(60f, 120f);
                trustLevel = Mathf.Max(0f, trustLevel - 0.05f);
            }
        }

        public string GetInteractionLabel()
        {
            return $"Sell {desiredItem.itemName} to {customerName}";
        }

        public void Interact(CDL_PlayerController player)
        {
            if (player.inventory.HasItem(desiredItem, 1))
            {
                player.inventory.RemoveItem(desiredItem, 1);
                float payout = offerPrice * (1f + trustLevel);
                CDL_GameManager.Instance.AddMoney(payout);
                CDL_GameManager.Instance.AddReputation(2f);
                CDL_GameManager.Instance.AddHeat(desiredProduct != null ? desiredProduct.heatRisk * 0.4f : 1f, "Street sale");
                trustLevel = Mathf.Clamp01(trustLevel + 0.1f);
                patienceTimer = Random.Range(90f, 180f);
                CDL_GameManager.Instance.PushMessage($"{customerName} paid ${payout:0}.");
            }
            else
            {
                trustLevel = Mathf.Clamp01(trustLevel - 0.1f);
                CDL_GameManager.Instance.AddReputation(-1f);
                CDL_GameManager.Instance.PushMessage("Order failed.");
            }
        }

        public void ApplyDamage(float amount, GameObject source)
        {
            health -= amount;
            if (health <= 0f)
            {
                CDL_GameManager.Instance.AddHeat(8f, "Civilian assault");
                Destroy(gameObject);
            }
        }
    }
}
