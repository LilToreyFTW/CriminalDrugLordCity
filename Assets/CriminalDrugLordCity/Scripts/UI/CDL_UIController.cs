using CriminalDrugLordCity.CDL.Core;
using CriminalDrugLordCity.CDL.Dealers;
using CriminalDrugLordCity.CDL.Inventory;
using CriminalDrugLordCity.CDL.Properties;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace CriminalDrugLordCity.CDL.UI
{
    public class CDL_UIController : MonoBehaviour
    {
        public Text hudText;
        public Text inventoryText;
        public Text phoneText;
        public GameObject inventoryPanel;
        public GameObject phonePanel;
        public GameObject pausePanel;
        public GameObject mainMenuPanel;

        private CDL_InventoryComponent _inventory;

        private void Start()
        {
            _inventory = CDL_GameManager.Instance.playerInventory;
            if (mainMenuPanel != null)
            {
                mainMenuPanel.SetActive(true);
                Time.timeScale = 0f;
                Cursor.lockState = CursorLockMode.None;
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Tab) && inventoryPanel != null)
            {
                inventoryPanel.SetActive(!inventoryPanel.activeSelf);
            }

            if (Input.GetKeyDown(KeyCode.P) && phonePanel != null)
            {
                phonePanel.SetActive(!phonePanel.activeSelf);
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                bool paused = !pausePanel.activeSelf;
                pausePanel.SetActive(paused);
                Time.timeScale = paused ? 0f : 1f;
                Cursor.lockState = paused ? CursorLockMode.None : CursorLockMode.Locked;
            }

            if (Input.GetKeyDown(KeyCode.Return) && mainMenuPanel != null && mainMenuPanel.activeSelf)
            {
                mainMenuPanel.SetActive(false);
                Time.timeScale = 1f;
                Cursor.lockState = CursorLockMode.Locked;
            }

            Refresh();
        }

        private void Refresh()
        {
            if (hudText != null)
            {
                hudText.text = $"Money ${CDL_GameManager.Instance.playerMoney:0}\nRep {CDL_GameManager.Instance.reputation:0}\nHeat {CDL_GameManager.Instance.globalHeat:0}\nHP {FindAnyObjectByType<CDL_PlayerController>()?.health:0}\n{CDL_GameManager.Instance.currentPrompt}\n{CDL_GameManager.Instance.currentMessage}";
            }

            if (inventoryText != null && _inventory != null)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("Inventory");
                foreach (var slot in _inventory.slots)
                {
                    sb.AppendLine($"{slot.item.itemName} x{slot.quantity}");
                }
                inventoryText.text = sb.ToString();
            }

            if (phoneText != null)
            {
                int dealers = CDL_GameManager.Instance.hiredDealers.Count(d => d.hired);
                int props = CDL_GameManager.Instance.ownedProperties.Count(p => p.IsOwned);
                int employees = CDL_GameManager.Instance.hiredEmployees.Count;
                phoneText.text =
                    "Empire Dashboard\n" +
                    $"Dealers: {dealers}\n" +
                    $"Properties: {props}\n" +
                    $"Employees: {employees}\n" +
                    $"Orders UI: Customer encounters live in world\n" +
                    $"Dealer UI: Interact with dealer NPC\n" +
                    $"Property UI: Interact with properties\n" +
                    $"Supplier Shop: Shift+F on supplier for bulk\n" +
                    $"District Map: Placeholder text map";
            }
        }
    }
}
