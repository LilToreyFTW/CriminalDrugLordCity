using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CriminalDrugLordCity.Gameplay
{
    public class ProductionStation : MonoBehaviour
    {
        public string stationId;
        public string productTypeId;
        public float batchTime = 30f;
        public int yieldAmount = 50;
        public float powerUsage = 10f;
        public float heatGeneration = 5f;
        
        [Header("Status")]
        public bool isProducing;
        public float currentProgress;
        public float stability = 1.0f;
        public float contamination = 0.0f;

        public void StartBatch()
        {
            if (!isProducing) StartCoroutine(ProduceBatch());
        }

        private IEnumerator ProduceBatch()
        {
            isProducing = true;
            currentProgress = 0;
            while (currentProgress < batchTime)
            {
                currentProgress += Time.deltaTime;
                // Stability/Contamination logic here
                yield return null;
            }
            
            CompleteBatch();
            isProducing = false;
        }

        private void CompleteBatch()
        {
            ProductInstance batch = new ProductInstance
            {
                productId = productTypeId,
                customName = "Batch_" + DateTime.Now.Ticks,
                quantity = yieldAmount,
                stats = new ProductStats { quality = stability, purity = stability * (1 - contamination) }
            };
            
            PlayerInventory.Instance.AddProduct(batch);
            Debug.Log("Batch Completed: " + productTypeId);
        }
    }
}
