using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PuzleGame.Player
{
    public class CameraDolly : MonoBehaviour
    {
        [Header("Referances")]
        [SerializeField] private Camera _playerCamera;
        [SerializeField] private Transform _cameraControll;
        [SerializeField] private Transform _cameraTarget;

        private Vector3 _originalPosition;
        private Quaternion _originalRotation;

        public void OnEnable()
        {
            if (_playerCamera != null)
            {
                _originalPosition = _playerCamera.transform.localPosition;
                _originalRotation = _playerCamera.transform.localRotation;
            }
        }

        public void OnDisable()
        {
            if (_playerCamera != null)
            {
                _playerCamera.transform.localPosition = _originalPosition;
                _playerCamera.transform.localRotation = _originalRotation;
            }
        }

        public void Update()
        {
            if (_cameraControll != null)
            {
                _playerCamera.transform.position = _cameraControll.position;

                if (_cameraTarget == null)
                {
                    _playerCamera.transform.rotation = _cameraControll.rotation;
                }
                else
                {
                    _playerCamera.transform.LookAt(_cameraTarget);
                }
            }
        }
    }
}
