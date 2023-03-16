using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

namespace PuzleGame.Player
{
    public class PlayerController : MonoBehaviour
    {
        [Header("Referances")]
        [SerializeField] private CharacterController _controller;

        [Header("Settings")]
        [SerializeField, Min(0)] private float _movementSpeed = 5f;
        [SerializeField, Min(1)] private float _movementSmoothness = 7f;
        [Space]
        [SerializeField, Min(0)] private float _jumpForce = 1.25f;
        [SerializeField, Min(0)] private float _gravityStrenght = 2.5f;
        [SerializeField] private LayerMask _stepDownMask = 55;

        // Local variables
        private Vector2 _smoothInput = Vector2.zero;
        private Vector3 _gravity = Vector3.zero;
        private bool _grounded = true;

        public void Update()
        {
            // If CharacterController referance is missing almost nothing can be done so we just return
            if (_controller == null) { return; }



            // Check if we should step down
            if (_grounded != _controller.isGrounded && _controller.isGrounded == false && _gravity.y <= 0)
            {
                if (Physics.Raycast(transform.position + (_controller.center - new Vector3(0, _controller.height * 0.5f, 0)), Vector3.down, out RaycastHit hit, _controller.stepOffset + _controller.skinWidth * 2, _stepDownMask))
                {
                    _controller.Move(Vector3.down * hit.distance);
                }
            }
            _grounded = _controller.isGrounded;



            // Update gravity and apply jump
            if (_controller.isGrounded)
            {
                if (Input.GetButtonDown("Jump"))
                {
                    _gravity = -Physics.gravity * _jumpForce;
                }
                else
                {
                    _gravity = Vector3.zero;
                }
            }
            _gravity += Physics.gravity * _gravityStrenght * Time.deltaTime;



            // Move player with input
            _smoothInput = Vector2.MoveTowards(_smoothInput, new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized, Time.deltaTime * _movementSmoothness);
            Vector3 movedirection = _SquashVector(transform.right) * _smoothInput.x + _SquashVector(transform.forward) * _smoothInput.y;
            _controller.Move((movedirection * _movementSpeed + _gravity) * Time.deltaTime);
        }

        public void OnDrawGizmos()
        {
            Gizmos.DrawSphere(transform.position + (_controller.center - new Vector3(0, _controller.height * 0.5f, 0)), 0.1f);
        }

        private static Vector3 _SquashVector(Vector3 Vector)
        {
            Vector.y = 0;
            return Vector.normalized;
        }
    }
}