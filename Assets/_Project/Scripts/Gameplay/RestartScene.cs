using UnityEngine.SceneManagement;
using UnityEngine;

namespace PuzleGame.Gameplay
{
    public class RestartScene : MonoBehaviour
    {
        public void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == 3)
            {
                SceneManager.LoadScene(0);
            }
        }
    }
}
