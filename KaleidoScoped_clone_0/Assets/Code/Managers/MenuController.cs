using UnityEngine;

namespace Kaleidoscoped
{
    public class MenuController : MonoBehaviour
    {
        public static MenuController instance;

        public GameObject pauseMenuUI;
        public bool isPaused;

        void Awake()
        {
            instance = this;
        }

        public void Continue()
        {
            pauseMenuUI.SetActive(false);
            isPaused = false;
            //Time.timeScale = 1f;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        public void Pause()
        {
            pauseMenuUI.SetActive(true);
            isPaused = true;
            //Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
