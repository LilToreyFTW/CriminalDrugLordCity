using UnityEngine;

namespace CriminalDrugLordCity.Gameplay
{
    public static class DealershipBuilder
    {
        public static void Build(Vector3 position, GameObject dealershipPrefab)
        {
            if (dealershipPrefab == null)
            {
                GameObject fallback = GameObject.CreatePrimitive(PrimitiveType.Cube);
                fallback.name = "FallbackDealership";
                fallback.transform.position = position;
                fallback.transform.localScale = new Vector3(30, 15, 40);
                return;
            }

            GameObject dealership = Object.Instantiate(dealershipPrefab, position, Quaternion.identity);
            dealership.name = "LuxuryDealership";
            dealership.transform.localScale = Vector3.one * 5f;

            // Add trigger for interaction
            BoxCollider trigger = dealership.GetComponent<BoxCollider>();
            if (trigger == null) trigger = dealership.AddComponent<BoxCollider>();
            trigger.isTrigger = true;
            trigger.size = new Vector3(10, 5, 10);
            
            dealership.AddComponent<DealershipInteractable>();
            }
    }
}