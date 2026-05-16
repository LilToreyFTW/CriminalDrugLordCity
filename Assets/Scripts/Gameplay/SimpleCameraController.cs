using UnityEngine;

namespace CriminalDrugLordCity.Gameplay
{
    public sealed class SimpleCameraController : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private Vector3 thirdPersonOffset = new Vector3(0f, 5f, -10f);
        [SerializeField] private Vector3 firstPersonOffset = new Vector3(0f, 2f, 0f);
        [SerializeField] private float lookSpeed = 120f;
        [SerializeField] private float moveSpeed = 18f;

        private bool _thirdPerson = true;
        private float _yaw;
        private float _pitch = 12f;

        private void Update()
        {
            if (target == null)
            {
                return;
            }

            if (Input.GetKeyDown(KeyCode.V))
            {
                _thirdPerson = !_thirdPerson;
            }

            _yaw += Input.GetAxisRaw("Mouse X") * lookSpeed * Time.deltaTime;
            _pitch = Mathf.Clamp(_pitch - Input.GetAxisRaw("Mouse Y") * lookSpeed * Time.deltaTime, -65f, 70f);

            Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
            Vector3 move = Quaternion.Euler(0f, _yaw, 0f) * input.normalized;
            target.position += move * moveSpeed * Time.deltaTime;
            target.rotation = Quaternion.Euler(0f, _yaw, 0f);

            Vector3 offset = _thirdPerson ? thirdPersonOffset : firstPersonOffset;
            transform.position = target.position + Quaternion.Euler(_pitch, _yaw, 0f) * offset;
            transform.LookAt(target.position + new Vector3(0f, 2f, 0f));
        }
    }
}
