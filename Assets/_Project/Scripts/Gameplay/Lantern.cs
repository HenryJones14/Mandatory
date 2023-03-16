using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PuzleGame.Gameplay
{
    public class Lantern : MonoBehaviour
    {
        private static List<Lantern> _lanternList = new List<Lantern>();
        private static int _activeLanterns = 0;

        [Header("Referances")]
        [SerializeField] private GameObject _light;

        private bool _active = false;

        public void OnEnable()
        {
            _active = false;
            _lanternList.Add(this);
        }

        public void OnDisable()
        {
            DeactivateLantern();
            _lanternList.Remove(this);
        }

        public void ActivateLantern()
        {
            _light.SetActive(true);

            if (!_active)
            {
                _activeLanterns += 1;
                _active = true;
            }
        }

        public void DeactivateLantern()
        {
            _light.SetActive(false);

            if (_active)
            {
                _activeLanterns -= 1;
                _active = false;
            }
        }

        public static bool AllLanternsActive()
        {
            return _activeLanterns == _lanternList.Count;
        }
    }
}
