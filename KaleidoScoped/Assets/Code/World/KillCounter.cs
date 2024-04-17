using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Kaleidoscoped
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

        public void IncrementKills(CharacterSelection shooter)
        {
            shooter.playerKills++;
            DisplayKills();
        }

        public int GetKills()
        {
            return StaticVariables.playerKills;
        }

        private void DisplayKills()
        {
            if (counterText == null)
            {
                counterText = GameObject.FindWithTag("KillCounter").GetComponent<Text>();
            }
            else
            {
                counterText.text = kills.ToString();
            }
        }
    }
}
