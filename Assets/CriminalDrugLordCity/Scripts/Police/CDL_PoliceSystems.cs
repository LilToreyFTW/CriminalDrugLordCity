using CriminalDrugLordCity.CDL.Core;
using CriminalDrugLordCity.CDL.Properties;
using UnityEngine;

namespace CriminalDrugLordCity.CDL.Police
{
    public class CDL_PolicePatrol : MonoBehaviour, ICDLDamageable
    {
        public Transform[] patrolPoints;
        public float speed = 2.5f;
        public float suspicionDistance = 10f;
        public float health = 50f;

        private int _targetIndex;
        private CDL_PlayerController _player;

        private void Start()
        {
            _player = FindAnyObjectByType<CDL_PlayerController>();
        }

        private void Update()
        {
            if (_player == null)
            {
                return;
            }

            if (patrolPoints != null && patrolPoints.Length > 0)
            {
                Transform target = patrolPoints[_targetIndex];
                transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
                if (Vector3.Distance(transform.position, target.position) < 0.5f)
                {
                    _targetIndex = (_targetIndex + 1) % patrolPoints.Length;
                }
            }

            float distance = Vector3.Distance(transform.position, _player.transform.position);
            if (distance < suspicionDistance && CDL_GameManager.Instance.globalHeat > 10f)
            {
                transform.position = Vector3.MoveTowards(transform.position, _player.transform.position, speed * 1.3f * Time.deltaTime);
                CDL_GameManager.Instance.PushMessage("Police suspicious.");
            }
        }

        public void ApplyDamage(float amount, GameObject source)
        {
            health -= amount;
            CDL_GameManager.Instance.AddHeat(10f, "Attacked police");
            if (health <= 0f)
            {
                Destroy(gameObject);
            }
        }
    }

    public class CDL_RaidEventSystem : MonoBehaviour
    {
        public float raidCheckInterval = 30f;
        private float _timer;

        private void Update()
        {
            _timer -= Time.deltaTime;
            if (_timer > 0f)
            {
                return;
            }

            _timer = raidCheckInterval;
            if (CDL_GameManager.Instance.globalHeat < 25f || CDL_GameManager.Instance.ownedProperties.Count == 0)
            {
                return;
            }

            var target = CDL_GameManager.Instance.ownedProperties[Random.Range(0, CDL_GameManager.Instance.ownedProperties.Count)];
            CDL_GameManager.Instance.PushMessage($"Raid warning at {target.definition.propertyName}.");
            CDL_GameManager.Instance.AddHeat(-5f, "Raid response");
        }
    }
}
