using UnityEngine;
using UnityEngine.AI;

namespace CriminalDrugLordCity.Gameplay
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class NpcCarAi : MonoBehaviour
    {
        public float wanderRadius = 200f;
        private NavMeshAgent _agent;

        private void Start()
        {
            _agent = GetComponent<NavMeshAgent>();
            SetNewDestination();
        }

        private void Update()
        {
            if (!_agent.pathPending && _agent.remainingDistance < 2f)
            {
                SetNewDestination();
            }
        }

        private void SetNewDestination()
        {
            Vector3 randomDirection = Random.insideUnitSphere * wanderRadius;
            randomDirection += transform.position;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomDirection, out hit, wanderRadius, 1))
            {
                _agent.SetDestination(hit.position);
            }
        }
    }

    [RequireComponent(typeof(NavMeshAgent))]
    public class NpcPedestrianAi : MonoBehaviour
    {
        public float wanderRadius = 50f;
        private NavMeshAgent _agent;

        private void Start()
        {
            _agent = GetComponent<NavMeshAgent>();
            _agent.speed = 3.5f;
            SetNewDestination();
        }

        private void Update()
        {
            if (!_agent.pathPending && _agent.remainingDistance < 0.5f)
            {
                SetNewDestination();
            }
        }

        private void SetNewDestination()
        {
            Vector3 randomDirection = Random.insideUnitSphere * wanderRadius;
            randomDirection += transform.position;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomDirection, out hit, wanderRadius, 1))
            {
                _agent.SetDestination(hit.position);
            }
        }
    }

    [RequireComponent(typeof(NavMeshAgent))]
    public class ThugAi : MonoBehaviour
    {
        public float detectRange = 20f;
        public float attackRange = 10f;
        private NavMeshAgent _agent;
        private Transform _player;

        private void Start()
        {
            _agent = GetComponent<NavMeshAgent>();
            var playerObj = GameObject.FindWithTag("Player");
            if (playerObj != null) _player = playerObj.transform;
        }

        private void Update()
        {
            if (_player == null) return;

            float dist = Vector3.Distance(transform.position, _player.position);
            if (dist < detectRange)
            {
                _agent.SetDestination(_player.position);
            }
        }
    }
}
