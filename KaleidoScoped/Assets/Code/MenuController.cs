using UnityEngine;

namespace Kaleidoscoped
{
    public class MenuController : MonoBehaviour
    {
        public static MenuController instance;

        public GameObject pauseMenuUI;
        public static bool isPaused = false;

        void Awake()
        {
            instance = this;
        }

        public void Continue()
        {
            pauseMenuUI.SetActive(false);
            Time.timeScale = 1f;
            isPaused = false;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
