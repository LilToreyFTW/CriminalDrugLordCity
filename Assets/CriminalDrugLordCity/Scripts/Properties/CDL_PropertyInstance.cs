using CriminalDrugLordCity.CDL.Core;
using CriminalDrugLordCity.CDL.Employees;
using CriminalDrugLordCity.CDL.Inventory;
using CriminalDrugLordCity.CDL.Save;
using System.Collections.Generic;
using UnityEngine;

namespace CriminalDrugLordCity.CDL.Properties
{
    public class CDL_PropertyInstance : MonoBehaviour, ICDLInteractable
    {
        public CDL_PropertyDefinition definition;
        public bool IsOwned;
        public int upgradeLevel;
        public CDL_InventoryComponent storage;
        public List<CDL_EmployeeDefinition> assignedEmployees = new();

        public string PropertyId => definition != null ? definition.propertyId : name;

        private void Start()
        {
            if (storage != null && definition != null)
            {
                storage.slotLimit = definition.storageCapacity;
            }

            CDL_GameManager.Instance?.RegisterProperty(this);
        }

        public string GetInteractionLabel()
        {
            return IsOwned ? $"Manage {definition.propertyName}" : $"Buy {definition.propertyName} (${definition.purchasePrice:0})";
        }

        public void Interact(CDL_PlayerController player)
        {
            if (!IsOwned)
            {
                if (CDL_GameManager.Instance.SpendMoney(definition.purchasePrice))
                {
                    IsOwned = true;
                    CDL_GameManager.Instance.RegisterProperty(this);
                    CDL_GameManager.Instance.PushMessage($"{definition.propertyName} purchased.");
                }
                return;
            }

            int managerCount = 0;
            int guardCount = 0;
            foreach (var employee in assignedEmployees)
            {
                if (employee == null) continue;
                if (employee.role == CDL_EmployeeRole.Manager) managerCount++;
                if (employee.role == CDL_EmployeeRole.Guard) guardCount++;
            }

            CDL_GameManager.Instance.PushMessage($"{definition.propertyName} L{upgradeLevel + 1} | Staff {assignedEmployees.Count} | Guards {guardCount} | Managers {managerCount}");
        }

        public CDL_PropertySaveData BuildSave()
        {
            return new CDL_PropertySaveData
            {
                propertyId = PropertyId,
                owned = IsOwned,
                upgradeLevel = upgradeLevel
            };
        }

        public void ApplySave(CDL_PropertySaveData data)
        {
            if (data == null) return;
            IsOwned = data.owned;
            upgradeLevel = data.upgradeLevel;
        }
    }
}
