using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

namespace PuzleGame.Gameplay
{
    public class Button : MonoBehaviour
    {
        [Header("Referances")]
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private Transform _button;

        [Header("Settings")]
        [SerializeField] private float _pushbackForce;
        [SerializeField] private Vector2 _positionClamp;

        [Header("Events")]
        [SerializeField] private UnityEvent _buttonPressed;
        [SerializeField] private UnityEvent _buttonReleased;

        private bool _lastState = false;

        public void Update()
        {
            if (_button.localPosition.y < _positionClamp.x)
            {
                _button.localPosition = new Vector3(_button.localPosition.x, _positionClamp.x, _button.localPosition.z);
                _rigidbody.velocity = _rigidbody.velocity * -0.1f;
            }

            if (_button.localPosition.y > _positionClamp.y)
            {
                _button.localPosition = new Vector3(_button.localPosition.x, _positionClamp.y, _button.localPosition.z);
                _rigidbody.velocity = _rigidbody.velocity * -0.1f;
            }

            if (_button.localPosition.y > (_positionClamp.x + _positionClamp.y) * 0.5f)
            {
                if (!_lastState)
                {
                    _buttonReleased?.Invoke();
                    _lastState = true;
                }
            }
            else
            {
                if (_lastState)
                {
                    _buttonPressed?.Invoke();
                    _lastState = false;
                }
            }
        }

        public void FixedUpdate()
        {
            _rigidbody.AddForce(Vector3.up * _pushbackForce, ForceMode.Impulse);
        }
    }
}
