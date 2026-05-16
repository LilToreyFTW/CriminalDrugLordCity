using UnityEngine;
using UnityEngine.InputSystem;

namespace CriminalDrugLordCity.Gameplay
{
    [RequireComponent(typeof(CharacterController))]
    public sealed class CrimPlayerController : MonoBehaviour
    {
        [Header("Movement")]
        public float moveSpeed = 6f;
        public float rotationSpeed = 15f;
        public float lookSensitivity = 0.5f;

        [Header("Camera")]
        public Transform cameraTransform;
        public Vector3 fpsOffset = new Vector3(0, 4.5f, 0.5f);
        public Vector3 tpsOffset = new Vector3(0, 6f, -12f);
        public bool isFirstPerson = true;

        private CharacterController _controller;
        private Vector2 _lookRotation;
        private Vector3 _velocity;
        private float _gravity = -9.81f;

        private void Start()
        {
            _controller = GetComponent<CharacterController>();
            if (cameraTransform == null && Camera.main != null)
            {
                cameraTransform = Camera.main.transform;
            }
            Cursor.lockState = CursorLockMode.Locked;
        }

        [Header("Weapon")]
        public GameObject bulletPrefab;
        public GameObject muzzleFlashPrefab;
        public Transform muzzleTransform;
        public float fireRate = 0.15f;
        private float _nextFireTime;

        private void Update()
        {
            HandleMovement();
            HandleRotation();
            HandleInteraction();
            HandleShooting();
            UpdateCamera();

            if (Input.GetKeyDown(KeyCode.V))
            {
                isFirstPerson = !isFirstPerson;
            }

            if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Tab))
            {
                if (PhoneManager.Instance != null) PhoneManager.Instance.TogglePhone();
            }
}

        private void HandleShooting()
        {
            if (RuntimeGameState.PlayerState == null || string.IsNullOrEmpty(RuntimeGameState.PlayerState.EquippedWeapon))
                return;

            if (Input.GetMouseButton(0) && Time.time > _nextFireTime)
            {
                _nextFireTime = Time.time + fireRate;
                Shoot();
            }
        }

        private void Shoot()
        {
            if (bulletPrefab != null && cameraTransform != null)
            {
                Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);
                Vector3 targetPoint = ray.GetPoint(100);
                
                Vector3 spawnPos = muzzleTransform != null ? muzzleTransform.position : cameraTransform.position + cameraTransform.forward * 2f;
                Quaternion spawnRot = Quaternion.LookRotation(targetPoint - spawnPos);

                Object.Instantiate(bulletPrefab, spawnPos, spawnRot);
                WeaponEffects.CreateMuzzleFlash(muzzleTransform, muzzleFlashPrefab);
            }
        }

        private void HandleMovement()
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            Vector3 move = transform.right * horizontal + transform.forward * vertical;
            _controller.Move(move * moveSpeed * Time.deltaTime);

            if (_controller.isGrounded && _velocity.y < 0)
            {
                _velocity.y = -2f;
            }

            _velocity.y += _gravity * Time.deltaTime;
            _controller.Move(_velocity * Time.deltaTime);
        }

        private void HandleRotation()
        {
            float mouseX = Input.GetAxis("Mouse X") * lookSensitivity;
            float mouseY = Input.GetAxis("Mouse Y") * lookSensitivity;

            _lookRotation.x -= mouseY;
            _lookRotation.x = Mathf.Clamp(_lookRotation.x, -80f, 80f);
            _lookRotation.y += mouseX;

            transform.rotation = Quaternion.Euler(0, _lookRotation.y, 0);
        }

        private void HandleInteraction()
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                // Interaction logic handled by interactable components via triggers,
                // but we could also raycast here for precision.
            }
        }

        private void UpdateCamera()
        {
            if (cameraTransform == null) return;

            if (isFirstPerson)
            {
                cameraTransform.position = transform.position + transform.rotation * fpsOffset;
                cameraTransform.localRotation = Quaternion.Euler(_lookRotation.x, 0, 0);
            }
            else
            {
                cameraTransform.position = transform.position + transform.rotation * tpsOffset;
                cameraTransform.LookAt(transform.position + Vector3.up * 4f);
            }
        }
    }
}