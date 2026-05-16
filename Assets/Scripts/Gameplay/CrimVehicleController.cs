using UnityEngine;

namespace CriminalDrugLordCity.Gameplay
{
    public sealed class CrimVehicleController : MonoBehaviour
    {
        public float moveSpeed = 25f;
        public float turnSpeed = 100f;
        public bool isDriving = false;

        private Rigidbody _rb;

        private void Start()
        {
            _rb = GetComponent<Rigidbody>();
            if (_rb == null)
            {
                _rb = gameObject.AddComponent<Rigidbody>();
                _rb.mass = 1500f;
                _rb.linearDamping = 1f;
                _rb.angularDamping = 2f;
            }
        }

        private void Update()
        {
            if (!isDriving) return;

            float move = Input.GetAxis("Vertical");
            float turn = Input.GetAxis("Horizontal");

            if (Mathf.Abs(move) > 0.1f)
            {
                transform.Translate(Vector3.forward * move * moveSpeed * Time.deltaTime);
                transform.Rotate(Vector3.up * turn * turnSpeed * Time.deltaTime * Mathf.Sign(move));
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                ExitVehicle();
            }
        }

        public void EnterVehicle(GameObject player)
        {
            isDriving = true;
            player.SetActive(false);
            player.transform.SetParent(transform);
            
            // Re-enable camera if it was on player
            var cam = Camera.main;
            if (cam != null)
            {
                cam.transform.SetParent(transform);
                cam.transform.localPosition = new Vector3(0, 5, -10);
                cam.transform.localRotation = Quaternion.Euler(20, 0, 0);
            }
        }

        public void ExitVehicle()
        {
            isDriving = false;
            Transform playerTransform = transform.Find("PlayerCharacter") ?? transform.Find("PlayerBlockout");
            if (playerTransform != null)
            {
                playerTransform.SetParent(null);
                playerTransform.gameObject.SetActive(true);
                playerTransform.position = transform.position + transform.right * 3f + Vector3.up * 1f;
                
                var cam = Camera.main;
                if (cam != null)
                {
                    cam.transform.SetParent(null);
                }
            }
        }
    }
}
