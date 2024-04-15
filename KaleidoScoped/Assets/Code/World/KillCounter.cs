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
        public Text blueCounterText;
        public Text redCounterText;
        int kills;
        int blueKills;
        int redKills;

        void Start()
        {
            kills = 0;
            blueKills = 0;
            redKills = 0;
            DisplayKills();
        }

        public void IncrementTeamKills(int team)
        {
            if (team == 1)
            {
                blueKills++;
                print("Blue Kills: " + blueKills);
            }
            else
            {
                redKills++;
                print("Red Kills: " + redKills);
            }
            DisplayKills();
        }

        public void IncrementKills()
        {
            kills++;
            /*if (kills >= 10)
            {
                SceneManager.LoadScene("Victory");
            }*/
            print("Kills: " + kills);
            DisplayKills();
        }

        public int DetermineWinner()
        {
            if(blueKills > redKills)
            {
                if(blueKills >= 15)
                {
                    return 1;
                } else
                {
                    return 0;
                }
            } else
            {
                if(redKills >= 15)
                {
                    return 2;
                } else
                {
                    return 0;
                }
            }
        }

        public int GetKills()
        {
            return kills;
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
            if (blueCounterText == null)
            {
                blueCounterText = GameObject.FindWithTag("BlueKillCounter").GetComponent<Text>();
            }
            else
            {
                blueCounterText.text = blueKills.ToString();
            }
            if (redCounterText == null)
            {
                redCounterText = GameObject.FindWithTag("RedKillCounter").GetComponent<Text>();
            }
            else
            {
                redCounterText.text = redKills.ToString();
            }
        }
    }
}
