using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PuzleGame.Gameplay
{
    public class Door : MonoBehaviour
    {
        [Header("Referances")]
        [SerializeField] private Animator _animator;

        private bool _canLock = false;

        public void Update()
        {
            if (Lantern.AllLanternsActive())
            {
                UnlockDoor();
                _canLock = true;
            }
            else if (_canLock)
            {
                LockDoor();
            }
        }

        public void LockDoor()
        {
            _animator.SetBool("Open", false);
        }

        public void UnlockDoor()
        {
            _animator.SetBool("Open", true);
        }
    }
}
