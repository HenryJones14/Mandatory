using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PuzleGame.Player
{
    public class CameraController : MonoBehaviour
    {
        [Header("Referances")]
        [SerializeField] private Transform _playerRootTransform;
        [SerializeField] private Transform _cameraRigTransform;
        [Space]
        [SerializeField] private Transform _mainTransform;
        [SerializeField] private Camera _mainCamera;

        [Header("Settings")]
        [SerializeField] private float _rotationSpeed = 0.75f;
        [SerializeField] private Vector2 _rotationClamp = new Vector2(-89, 89);
        [SerializeField] private LayerMask _cameraClippingMask = 55;

        // Local variables
        float _rotation = 0;
        Vector3 _defaultPosition;

        public void Awake()
        {
            _defaultPosition = _mainTransform.localPosition;
        }

        public void Update()
        {
            // Calculate rotation
            _rotation -= Input.GetAxisRaw("Mouse Y") * _rotationSpeed;
            _rotation = Mathf.Clamp(_rotation, _rotationClamp.x, _rotationClamp.y);



            // Apply rotation
            _playerRootTransform.Rotate(Vector3.up * Input.GetAxisRaw("Mouse X") * _rotationSpeed);
            _cameraRigTransform.localRotation = Quaternion.Euler(_rotation, 0, 0);



            // Move camera to stop clipping
            _mainTransform.localPosition = _defaultPosition;
            Vector3 direction = _mainTransform.position - _cameraRigTransform.position;
            if (Physics.SphereCast(_cameraRigTransform.position, _mainCamera.nearClipPlane, direction, out RaycastHit hit, direction.magnitude + _mainCamera.nearClipPlane, _cameraClippingMask))
            {
                _mainTransform.position -= direction.normalized * (direction.magnitude - hit.distance + _mainCamera.nearClipPlane);
            }
        }

        public void OnDrawGizmos()
        {
            Vector3 reset = _mainTransform.position;



            _mainTransform.localPosition = _defaultPosition;
            Vector3 direction = _mainTransform.position - _cameraRigTransform.position;
            Gizmos.DrawRay(_cameraRigTransform.position, direction);



            _mainTransform.position = reset;
        }
    }
}