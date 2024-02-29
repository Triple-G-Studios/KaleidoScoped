using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Kaleidoscope
{
    public class KillCounter : MonoBehaviour
    {
        // Outlets
        public Text counterText;
        int kills;

        void Start()
        {
            kills = 0;
            DisplayKills();
        }

        public void IncrementKills()
        {
            kills++;
            if (kills >= 10)
            {
                SceneManager.LoadScene("Victory");
            }
            DisplayKills();
        }

        public int GetKills()
        {
            return kills;
        }

        private void DisplayKills()
        {
            if (counterText == null)
            {
                counterText = GameObject.FindWithTag("KillCounterText").GetComponent<Text>();
            }
            else
            {
                counterText.text = kills.ToString();
            }
        }
    }
}
