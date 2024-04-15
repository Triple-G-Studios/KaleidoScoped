using UnityEngine;
using UnityEngine.SceneManagement;

namespace Kaleidoscoped
{
    public class SceneLoader : MonoBehaviour
    {
        public void ReloadGame()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            SceneManager.LoadScene("JoeyScene");
        }

        public void VictoryScreen(string winner)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            PlayerPrefs.SetString("winner", winner);
            SceneManager.LoadScene("Victory");
        }

        public void GameOverScreen()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            SceneManager.LoadScene("GameOver");
        }
    }
}