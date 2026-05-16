using CriminalDrugLordCity.CDL.Core;
using CriminalDrugLordCity.CDL.Districts;
using UnityEngine;

namespace CriminalDrugLordCity.CDL.Rivals
{
    public class CDL_RivalZone : MonoBehaviour
    {
        public CDL_RivalGangDefinition rivalGang;
        public CDL_DistrictRuntime district;
    }

    public class CDL_RivalNPC : MonoBehaviour, ICDLDamageable
    {
        public CDL_RivalGangDefinition gang;
        public float health = 40f;
        public float attackRange = 4f;
        public float damage = 8f;
        private CDL_PlayerController _player;

        private void Start()
        {
            _player = FindAnyObjectByType<CDL_PlayerController>();
        }

        private void Update()
        {
            if (_player == null) return;
            float dist = Vector3.Distance(transform.position, _player.transform.position);
            if (dist < attackRange)
            {
                _player.ApplyDamage(damage * Time.deltaTime, gameObject);
                transform.LookAt(_player.transform.position);
            }
        }

        public void ApplyDamage(float amount, GameObject source)
        {
            health -= amount;
            if (health <= 0f)
            {
                CDL_GameManager.Instance.AddReputation(1f);
                Destroy(gameObject);
            }
        }
    }
}
