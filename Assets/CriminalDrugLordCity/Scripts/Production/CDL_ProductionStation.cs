using CriminalDrugLordCity.CDL.Core;
using CriminalDrugLordCity.CDL.Employees;
using CriminalDrugLordCity.CDL.Inventory;
using CriminalDrugLordCity.CDL.Properties;
using UnityEngine;

namespace CriminalDrugLordCity.CDL.Production
{
    public class CDL_ProductionStation : MonoBehaviour, ICDLInteractable
    {
        public string stationName = "Production Station";
        public int stationLevel = 1;
        public CDL_RecipeDefinition recipe;
        public CDL_InventoryComponent storage;
        public CDL_EmployeeDefinition assignedWorker;
        public bool isProducing;
        public float timerRemaining;

        private void Update()
        {
            if (!isProducing)
            {
                return;
            }

            float workerBoost = assignedWorker != null ? assignedWorker.skill : 0f;
            timerRemaining -= Time.deltaTime * (1f + workerBoost);
            if (timerRemaining <= 0f)
            {
                CompleteBatch();
            }
        }

        public string GetInteractionLabel()
        {
            return isProducing ? $"{stationName}: Working {timerRemaining:0}s" : $"Use {stationName}";
        }

        public void Interact(CDL_PlayerController player)
        {
            if (isProducing)
            {
                CDL_GameManager.Instance.PushMessage("Batch already running.");
                return;
            }

            if (recipe == null || storage == null)
            {
                CDL_GameManager.Instance.PushMessage("Station setup missing.");
                return;
            }

            foreach (var input in recipe.inputs)
            {
                if (!storage.HasItem(input.item, input.amount))
                {
                    CDL_GameManager.Instance.PushMessage($"Missing {input.item.itemName}.");
                    return;
                }
            }

            foreach (var input in recipe.inputs)
            {
                storage.RemoveItem(input.item, input.amount);
            }

            timerRemaining = recipe.duration;
            isProducing = true;
            CDL_GameManager.Instance.AddHeat(1f + recipe.outputProduct.heatRisk * 0.2f, "Production started");
        }

        private void CompleteBatch()
        {
            isProducing = false;
            int qualityBonus = stationLevel + Mathf.RoundToInt((assignedWorker != null ? assignedWorker.skill : 0f) * 2f);
            int amount = recipe.outputAmount + Mathf.Clamp(qualityBonus - 1, 0, 2);
            storage.AddItem(recipe.outputItem, amount);
            CDL_GameManager.Instance.PushMessage($"{recipe.outputItem.itemName} batch complete.");
        }
    }
}
