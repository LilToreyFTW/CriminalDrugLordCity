using UnityEngine;
using UnityEngine.AI;

namespace CriminalDrugLordCity.Gameplay
{
    public static class NpcSpawner
    {
        public static void SpawnNpcs(GameObject thugPrefab, GameObject citizenPrefab, int count)
        {
            GameObject npcRoot = new GameObject("NPCs");
            
            for (int i = 0; i < count; i++)
            {
                GameObject prefab = (Random.value > 0.5f) ? thugPrefab : citizenPrefab;
                if (prefab == null) continue;

                Vector3 spawnPos = GetRandomNavMeshPosition(300f);
                if (spawnPos != Vector3.zero)
                {
                    GameObject npc = Object.Instantiate(prefab, spawnPos, Quaternion.identity, npcRoot.transform);
                    npc.name = prefab.name + "_" + i;
                    npc.transform.localScale = Vector3.one * 1.8f;
                    npc.AddComponent<NpcPedestrianAi>();
                    
                    NavMeshAgent agent = npc.GetComponent<NavMeshAgent>();
                    if (agent == null) agent = npc.AddComponent<NavMeshAgent>();
                }
            }
        }

        public static void SpawnPolice(GameObject policePrefab, int count)
        {
            GameObject policeRoot = new GameObject("Police_Units");
            for (int i = 0; i < count; i++)
            {
                Vector3 spawnPos = GetRandomNavMeshPosition(400f);
                if (spawnPos != Vector3.zero)
                {
                    GameObject police = Object.Instantiate(policePrefab, spawnPos, Quaternion.identity, policeRoot.transform);
                    police.name = "Police_Officer_" + i;
                    police.transform.localScale = Vector3.one * 1.8f;
                    police.AddComponent<PoliceAi>();
                    
                    NavMeshAgent agent = police.GetComponent<NavMeshAgent>();
                    if (agent == null) agent = police.AddComponent<NavMeshAgent>();
                }
            }
        }

        private static Vector3 GetRandomNavMeshPosition(float radius)
        {
            Vector3 randomDirection = Random.insideUnitSphere * radius;
            randomDirection.y = 0;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomDirection, out hit, radius, 1))
            {
                return hit.position;
            }
            return Vector3.zero;
        }
    }
}
