using CriminalDrugLordCity.CDL.Core;
using CriminalDrugLordCity.CDL.Inventory;
using CriminalDrugLordCity.CDL.Vehicles;
using UnityEngine;

namespace CriminalDrugLordCity.CDL.Core
{
    [RequireComponent(typeof(CharacterController))]
    public class CDL_PlayerController : MonoBehaviour, ICDLDamageable
    {
        public float walkSpeed = 4f;
        public float sprintSpeed = 7f;
        public float crouchSpeed = 2f;
        public float gravity = -20f;
        public float lookSensitivity = 1.5f;
        public float interactDistance = 3.5f;
        public Transform firstPersonCameraPivot;
        public Transform thirdPersonCameraPivot;
        public Camera playerCamera;
        public CDL_InventoryComponent inventory;
        public float health = 100f;

        private CharacterController _controller;
        private Vector3 _velocity;
        private float _pitch;
        private bool _thirdPerson;
        private CDL_VehicleController _currentVehicle;

        public bool IsDriving => _currentVehicle != null;

        private void Awake()
        {
            _controller = GetComponent<CharacterController>();
        }

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            if (CDL_GameManager.Instance != null)
            {
                CDL_GameManager.Instance.playerInventory = inventory;
            }
            UpdateCameraMode();
        }

        private void Update()
        {
            if (IsDriving)
            {
                if (Input.GetKeyDown(KeyCode.F))
                {
                    ExitVehicle();
                }
                return;
            }

            HandleLook();
            HandleMove();
            HandleInteraction();

            if (Input.GetKeyDown(KeyCode.V))
            {
                _thirdPerson = !_thirdPerson;
                UpdateCameraMode();
            }
        }

        private void HandleLook()
        {
            float mouseX = Input.GetAxis("Mouse X") * lookSensitivity;
            float mouseY = Input.GetAxis("Mouse Y") * lookSensitivity;

            transform.Rotate(Vector3.up * mouseX);
            _pitch = Mathf.Clamp(_pitch - mouseY, -75f, 75f);
            firstPersonCameraPivot.localRotation = Quaternion.Euler(_pitch, 0f, 0f);
            thirdPersonCameraPivot.localRotation = Quaternion.Euler(_pitch * 0.5f, 0f, 0f);
        }

        private void HandleMove()
        {
            float speed = Input.GetKey(KeyCode.LeftControl) ? crouchSpeed : (Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : walkSpeed);
            Vector3 move = (transform.forward * Input.GetAxisRaw("Vertical") + transform.right * Input.GetAxisRaw("Horizontal")).normalized;

            if (_controller.isGrounded && _velocity.y < 0f)
            {
                _velocity.y = -2f;
            }

            _controller.Move(move * speed * Time.deltaTime);
            _velocity.y += gravity * Time.deltaTime;
            _controller.Move(_velocity * Time.deltaTime);

            _controller.height = Input.GetKey(KeyCode.LeftControl) ? 1.1f : 1.8f;
        }

        private void HandleInteraction()
        {
            CDL_GameManager.Instance.currentPrompt = string.Empty;

            if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out RaycastHit hit, interactDistance))
            {
                var interactable = hit.collider.GetComponentInParent<ICDLInteractable>();
                if (interactable != null)
                {
                    CDL_GameManager.Instance.currentPrompt = interactable.GetInteractionLabel();
                    if (Input.GetKeyDown(KeyCode.F))
                    {
                        interactable.Interact(this);
                    }
                }
            }
        }

        private void UpdateCameraMode()
        {
            firstPersonCameraPivot.gameObject.SetActive(!_thirdPerson);
            thirdPersonCameraPivot.gameObject.SetActive(_thirdPerson);
            playerCamera.transform.SetParent(_thirdPerson ? thirdPersonCameraPivot : firstPersonCameraPivot, false);
            playerCamera.transform.localPosition = Vector3.zero;
            playerCamera.transform.localRotation = Quaternion.identity;
        }

        public void EnterVehicle(CDL_VehicleController vehicle)
        {
            _currentVehicle = vehicle;
            gameObject.SetActive(false);
        }

        public void ExitVehicle()
        {
            transform.position = _currentVehicle.transform.position + _currentVehicle.transform.right * 2f;
            gameObject.SetActive(true);
            _currentVehicle.ExitDriver(this);
            _currentVehicle = null;
        }

        public void ApplyDamage(float amount, GameObject source)
        {
            health -= amount;
            CDL_GameManager.Instance.PushMessage($"Took {amount:0} damage.");
            if (health <= 0f)
            {
                health = 100f;
                transform.position = Vector3.zero + Vector3.up * 2f;
                CDL_GameManager.Instance.AddHeat(-10f, "Hospital reset");
            }
        }
    }
}
