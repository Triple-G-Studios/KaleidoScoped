using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Kaleidoscoped
{
    public class VictoryController : MonoBehaviour
    {
        // Outlets
        public TextMeshProUGUI textMeshPro;
        public GameObject panel;

        void Start()
        {
            if (PlayerPrefs.GetString("winner") != null)
            {
                textMeshPro.text = PlayerPrefs.GetString("winner") + " has won!";

                if (PlayerPrefs.GetString("winner") == "Blue team") panel.GetComponent<Image>().color = new Color(2, 164, 211);
                else if (PlayerPrefs.GetString("winner") == "Red team") panel.GetComponent<Image>().color = new Color(255, 105, 97);

            } else {
                textMeshPro.text = "Game Over!";
            }
            PlayerPrefs.DeleteAll();
        }
    }
}