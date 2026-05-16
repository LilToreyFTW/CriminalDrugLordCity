using UnityEngine;
using UnityEngine.AI;

namespace CriminalDrugLordCity.Gameplay
{
    public static class WantedSystem
    {
        public static int WantedLevel = 0;
        public static float LastCrimeTime;

        public static void CommitCrime(int intensity)
        {
            WantedLevel = Mathf.Clamp(WantedLevel + intensity, 0, 5);
            LastCrimeTime = Time.time;
            Debug.Log("Crime committed! Wanted Level: " + WantedLevel);
        }
    }

    [RequireComponent(typeof(NavMeshAgent))]
    public class PoliceAi : MonoBehaviour
    {
        private NavMeshAgent _agent;
        private GameObject _player;

        private void Start()
        {
            _agent = GetComponent<NavMeshAgent>();
            _player = GameObject.FindGameObjectWithTag("Player");
            _agent.speed = 18f;
        }

        private void Update()
        {
            if (WantedSystem.WantedLevel > 0 && _player != null)
            {
                _agent.SetDestination(_player.transform.position);
            }
            else if (_agent.remainingDistance < 1f)
            {
                // Patrol or idle
            }
        }
    }
}
