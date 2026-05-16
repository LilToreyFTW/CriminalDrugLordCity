using CriminalDrugLordCity.CDL.Dealers;
using CriminalDrugLordCity.CDL.Districts;
using CriminalDrugLordCity.CDL.Employees;
using CriminalDrugLordCity.CDL.Inventory;
using CriminalDrugLordCity.CDL.Police;
using CriminalDrugLordCity.CDL.Properties;
using CriminalDrugLordCity.CDL.Save;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CriminalDrugLordCity.CDL.Core
{
    public sealed class CDL_GameManager : MonoBehaviour
    {
        public static CDL_GameManager Instance { get; private set; }

        [Header("Progression")]
        public float playerMoney = 2500f;
        public float reputation = 10f;
        public float globalHeat = 0f;

        [Header("State")]
        public CDL_InventoryComponent playerInventory;
        public List<CDL_PropertyInstance> ownedProperties = new();
        public List<CDL_DealerNPC> hiredDealers = new();
        public List<CDL_EmployeeDefinition> hiredEmployees = new();
        public List<CDL_DistrictRuntime> districts = new();

        [Header("UI Signals")]
        public string currentPrompt = string.Empty;
        public string currentMessage = "Build your empire.";

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        private void Update()
        {
            globalHeat = Mathf.MoveTowards(globalHeat, 0f, Time.deltaTime * 0.5f);
        }

        public void RegisterDistrict(CDL_DistrictRuntime district)
        {
            if (!districts.Contains(district))
            {
                districts.Add(district);
            }
        }

        public void RegisterProperty(CDL_PropertyInstance property)
        {
            if (property.IsOwned && !ownedProperties.Contains(property))
            {
                ownedProperties.Add(property);
            }
        }

        public bool SpendMoney(float amount)
        {
            if (playerMoney < amount)
            {
                PushMessage($"Need ${amount:0}.");
                return false;
            }

            playerMoney -= amount;
            return true;
        }

        public void AddMoney(float amount)
        {
            playerMoney += amount;
        }

        public void AddHeat(float amount, string reason)
        {
            globalHeat = Mathf.Clamp(globalHeat + amount, 0f, 100f);
            currentMessage = $"{reason} (+{amount:0.0} heat)";
        }

        public void AddReputation(float amount)
        {
            reputation = Mathf.Clamp(reputation + amount, 0f, 100f);
        }

        public void PushMessage(string message)
        {
            currentMessage = message;
        }

        public CDL_SaveData BuildSaveData()
        {
            return new CDL_SaveData
            {
                money = playerMoney,
                reputation = reputation,
                heat = globalHeat,
                inventory = playerInventory != null ? playerInventory.BuildSave() : new List<CDL_InventoryEntryData>(),
                properties = ownedProperties.Select(p => p.BuildSave()).ToList(),
                dealers = hiredDealers.Select(d => d.BuildSave()).ToList(),
                employees = hiredEmployees.Select(e => e != null ? e.employeeName : string.Empty).ToList()
            };
        }

        public void ApplySaveData(CDL_SaveData data)
        {
            playerMoney = data.money;
            reputation = data.reputation;
            globalHeat = data.heat;

            if (playerInventory != null)
            {
                playerInventory.ApplySave(data.inventory);
            }

            foreach (var property in FindObjectsByType<CDL_PropertyInstance>())
            {
                property.ApplySave(data.properties.FirstOrDefault(p => p.propertyId == property.PropertyId));
            }

            foreach (var dealer in FindObjectsByType<CDL_DealerNPC>())
            {
                dealer.ApplySave(data.dealers.FirstOrDefault(d => d.dealerId == dealer.DealerId));
            }
        }
    }
}
