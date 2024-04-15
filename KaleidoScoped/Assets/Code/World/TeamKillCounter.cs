using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


namespace Kaleidoscoped
{
    public class TeamKillCounter : NetworkBehaviour
    {
        [SyncVar(hook = nameof(OnBlueKillsChanged))]
        public int blueKills;

        [SyncVar(hook = nameof(OnRedKillsChanged))]
        public int redKills;

        public Text blueCounterText;
        public Text redCounterText;

        void Start()
        {
            blueKills = 0;
            redKills = 0;
            DisplayKills();
        }

        // Called when blueKills value changes on server
        private void OnBlueKillsChanged(int oldValue, int newValue)
        {
            blueKills = newValue;
            DisplayKills();
        }

        // Called when redKills value changes on server
        private void OnRedKillsChanged(int oldValue, int newValue)
        {
            redKills = newValue;
            DisplayKills();
        }

        public void IncrementTeamKills(int team)
        {
            if (!isServer)
                return;

            if (team == 1)
            {
                blueKills++;
            }
            else
            {
                redKills++;
            }
        }

        public int DetermineWinner()
        {
            if (blueKills > redKills && blueKills >= 15)
            {
                return 1;
            }
            else if (redKills > blueKills && redKills >= 15)
            {
                return 2;
            }
            else
            {
                return 0;
            }
        }

        public int DetermineWinnerEnd()
        {
            if (blueKills > redKills)
            {
                return 1;
            }
            else
            {
                return 2;
            }
        }

        public int GetWinnerKills()
        {
            if (blueKills > redKills) return blueKills;
            else return redKills;
        }

        private void DisplayKills()
        {
            if (blueCounterText != null)
            {
                blueCounterText.text = blueKills.ToString();
            }
            if (redCounterText != null)
            {
                redCounterText.text = redKills.ToString();
            }
        }
    }
}
