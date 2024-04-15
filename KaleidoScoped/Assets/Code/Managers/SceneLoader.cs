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

        public void VictoryScreen(string winner, int winnerKills)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            PlayerPrefs.SetString("winner", winner);
            PlayerPrefs.SetInt("winnerk", winnerKills);
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