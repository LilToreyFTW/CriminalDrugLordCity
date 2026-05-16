using UnityEngine;
using UnityEngine.AI;

namespace CriminalDrugLordCity.Gameplay
{
    public static class NpcCarSpawner
    {
        public static void SpawnNpcCars(GameObject carPrefab, int count)
        {
            if (carPrefab == null) return;
            
            GameObject npcCarRoot = new GameObject("NPC_Cars");
            
            for (int i = 0; i < count; i++)
            {
                Vector3 spawnPos = GetRandomNavMeshPosition(400f);
                if (spawnPos != Vector3.zero)
                {
                    GameObject car = Object.Instantiate(carPrefab, spawnPos, Quaternion.identity, npcCarRoot.transform);
                    car.name = "NPC_Car_" + i;
                    
                    // Add AI
                    car.AddComponent<NpcCarAi>();
                    NavMeshAgent agent = car.GetComponent<NavMeshAgent>();
                    if (agent == null) agent = car.AddComponent<NavMeshAgent>();
                    agent.speed = 15f;
                    agent.acceleration = 8f;
                    agent.radius = 3f;

                    // Add stealing interactable
                    VehicleInteractable interactable = car.GetComponent<VehicleInteractable>();
                    if (interactable == null) interactable = car.AddComponent<VehicleInteractable>();
                }
            }
        }

        private static Vector3 GetRandomNavMeshPosition(float radius)
        {
            Vector3 randomDirection = Random.insideUnitSphere * radius;
            randomDirection.y = 8; // Offset for road height
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomDirection, out hit, radius, 1))
            {
                return hit.position;
            }
            return Vector3.zero;
        }
    }
}
