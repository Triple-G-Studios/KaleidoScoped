using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Kaleidoscoped
{
    public class EnemyRemainingCounter : MonoBehaviour
    {
        // Outlets
        public Text remainingText;
        int enemies;

        void Start()
        {
            enemies = 12;
            DisplayEnemies();
        }

        public void DecrementEnemies()
        {
            enemies--;
            /*if (kills >= 10)
            {
                SceneManager.LoadScene("Victory");
            }*/
            DisplayEnemies();
        }

        public int GetEnemies()
        {
            return enemies;
        }

        private void DisplayEnemies()
        {
            if (remainingText == null)
            {
                remainingText = GameObject.FindWithTag("EnemyRemaining").GetComponent<Text>();
            }
            else
            {
                remainingText.text = enemies.ToString();
            }
        }
    }
}
