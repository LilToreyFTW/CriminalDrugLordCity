using CriminalDrugLordCity.CDL.Core;
using CriminalDrugLordCity.CDL.Inventory;
using UnityEngine;

namespace CriminalDrugLordCity.CDL.Vehicles
{
    public class CDL_VehicleController : MonoBehaviour, ICDLInteractable
    {
        public float driveSpeed = 12f;
        public float turnSpeed = 80f;
        public CDL_InventoryComponent trunkInventory;
        public Transform driverSeat;

        private CDL_PlayerController _driver;

        private void Update()
        {
            if (_driver == null)
            {
                return;
            }

            float move = Input.GetAxisRaw("Vertical");
            float turn = Input.GetAxisRaw("Horizontal");
            transform.position += transform.forward * move * driveSpeed * Time.deltaTime;
            transform.Rotate(Vector3.up, turn * turnSpeed * Time.deltaTime);

            if (Mathf.Abs(move) > 0.1f)
            {
                CDL_GameManager.Instance.AddHeat(0.02f, "Driving");
            }
        }

        public string GetInteractionLabel()
        {
            return _driver == null ? "Drive vehicle" : "Vehicle in use";
        }

        public void Interact(CDL_PlayerController player)
        {
            if (_driver != null)
            {
                return;
            }

            _driver = player;
            player.EnterVehicle(this);
        }

        public void ExitDriver(CDL_PlayerController player)
        {
            if (_driver == player)
            {
                _driver = null;
            }
        }
    }

    public class CDL_DeliveryMission : MonoBehaviour, ICDLInteractable
    {
        public string missionName = "Timed Delivery";
        public CDL_ItemDefinition requiredItem;
        public int requiredAmount = 2;
        public float timeLimit = 90f;
        public float reward = 350f;
        public Transform dropoff;

        private bool _active;
        private float _timer;
        private CDL_PlayerController _player;

        private void Update()
        {
            if (!_active || _player == null)
            {
                return;
            }

            _timer -= Time.deltaTime;
            if (_timer <= 0f)
            {
                _active = false;
                CDL_GameManager.Instance.AddHeat(3f, "Delivery failed");
                CDL_GameManager.Instance.PushMessage("Delivery failed.");
                return;
            }

            if (dropoff != null && Vector3.Distance(_player.transform.position, dropoff.position) < 4f)
            {
                if (_player.inventory.HasItem(requiredItem, requiredAmount))
                {
                    _player.inventory.RemoveItem(requiredItem, requiredAmount);
                    CDL_GameManager.Instance.AddMoney(reward);
                    CDL_GameManager.Instance.AddReputation(3f);
                    CDL_GameManager.Instance.PushMessage($"Delivery complete: ${reward:0}");
                }

                _active = false;
            }
        }

        public string GetInteractionLabel()
        {
            return _active ? $"Delivery active {_timer:0}s" : $"Start {missionName}";
        }

        public void Interact(CDL_PlayerController player)
        {
            _player = player;
            _active = true;
            _timer = timeLimit;
            CDL_GameManager.Instance.PushMessage($"{missionName} started.");
        }
    }
}
