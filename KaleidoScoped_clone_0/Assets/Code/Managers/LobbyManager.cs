using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Kaleidoscoped
{
    public class LobbyManager : MonoBehaviour
    {
        //state tracking
        public string weapon = "rifle";
        public string color = "blue";
        public int team = 1;

        //outlets
        public TextMeshProUGUI currentWeapon;
        public TextMeshProUGUI currentColor;
        public TextMeshProUGUI currentTeam;

        public void playPressed()
        {
            PlayerPrefs.SetString("color", color);
            PlayerPrefs.SetString("weapon", weapon);
            PlayerPrefs.SetInt("team", team);
            SceneManager.LoadScene("Instructions");
        }

        public void riflePress()
        {
            weapon = "rifle";
            currentWeapon.text = "Current Weapon: Rifle";
        }

        public void shotPress()
        {
            weapon = "shotgun";
            currentWeapon.text = "Current Weapon: Shotgun";
        }

        public void bluPress()
        {
            color = "blue";
            currentColor.text = "Current Color: Blue";
        }

        public void redPress()
        {
            color = "red";
            currentColor.text = "Current Color: Red";
        }

        public void purPress()
        {
            color = "purple";
            currentColor.text = "Current Color: Purple";
        }

        public void grePress()
        {
            color = "green";
            currentColor.text = "Current Color: Green";
        }

        public void t1Press()
        {
            team = 1;
            currentTeam.text = "Currently Team 1";
        }

        public void t2Press()
        {
            team = 2;
            currentTeam.text = "Currently Team 2";
        }
    }
}