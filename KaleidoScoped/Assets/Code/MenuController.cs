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
            Debug.Log("Sup");
            pauseMenuUI.SetActive(false);
            //Time.timeScale = 1f;
            isPaused = false;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        public void Pause()
        {
            isPaused = true;
            pauseMenuUI.SetActive(true);
            //Time.timeScale = 0f;
            isPaused = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
