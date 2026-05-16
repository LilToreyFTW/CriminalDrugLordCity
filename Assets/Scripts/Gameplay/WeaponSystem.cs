using UnityEngine;

namespace CriminalDrugLordCity.Gameplay
{
    public class Projectile : MonoBehaviour
    {
        public float speed = 100f;
        public float damage = 20f;
        public float lifetime = 3f;

        private void Start()
        {
            Destroy(gameObject, lifetime);
        }

        private void Update()
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player") || other.CompareTag("Untagged")) return;
            
            // Handle damage to NPCs
            var npc = other.GetComponent<NpcPedestrianAi>();
            if (npc != null)
            {
                Debug.Log("Hit NPC!");
                // Could add health system to NPCs here
            }
            
            Destroy(gameObject);
        }
    }

    public static class WeaponEffects
    {
        public static void CreateMuzzleFlash(Transform muzzle, GameObject flashPrefab)
        {
            if (muzzle == null || flashPrefab == null) return;
            GameObject flash = Object.Instantiate(flashPrefab, muzzle.position, muzzle.rotation, muzzle);
            Object.Destroy(flash, 0.05f);
        }
    }
}
