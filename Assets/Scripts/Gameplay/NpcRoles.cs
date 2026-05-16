using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

namespace CriminalDrugLordCity.Gameplay
{
    public class DealerAi : MonoBehaviour
    {
        public string district;
        public int skillLevel = 1;
        public float loyalty = 1.0f;
        public List<ProductInstance> inventory = new List<ProductInstance>();
        
        private NavMeshAgent _agent;
        
        private void Start()
        {
            _agent = GetComponent<NavMeshAgent>();
            // Start street sales logic
        }

        public void AssignToDistrict(string districtName)
        {
            this.district = districtName;
        }
    }

    public class CustomerAi : MonoBehaviour
    {
        public float trust = 0.5f;
        public float patience = 1.0f;
        public string requestedProduct;
        public int requestedAmount;
        
        private NavMeshAgent _agent;

        private void Start()
        {
            _agent = GetComponent<NavMeshAgent>();
        }

        public void SendOrder()
        {
            // Phone notification logic
        }
    }

    public class WorkerAi : MonoBehaviour
    {
        public ProductionStation assignedStation;
        public float efficiency = 1.0f;
        public float morale = 1.0f;

        private void Update()
        {
            if (assignedStation != null && !assignedStation.isProducing)
            {
                assignedStation.StartBatch();
            }
        }
    }
public class SupplierAi : MonoBehaviour
{
    public string supplierId;
    public string supplierName;
    public List<ProductInstance> stock = new List<ProductInstance>();

    public void OpenTrade()
    {
        // Trade UI logic
    }
}
}
