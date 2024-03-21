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

        //outlets
        public TextMeshProUGUI currentWeapon;
        public TextMeshProUGUI currentColor;

        public void playPressed()
        {
            SceneManager.LoadScene("JoeyScene");
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
    }
}