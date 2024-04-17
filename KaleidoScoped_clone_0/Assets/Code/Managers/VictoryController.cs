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
                textMeshPro.text = PlayerPrefs.GetString("winner") + " has won with " + PlayerPrefs.GetInt("winnerk") + " kills!";

                if (PlayerPrefs.GetString("winner") == "Blue team") panel.GetComponent<Image>().color = new Color32(2, 164, 211, 255);
                else if (PlayerPrefs.GetString("winner") == "Red team") panel.GetComponent<Image>().color = new Color32(255, 105, 97, 255);

            } else {
                textMeshPro.text = "Game Over!";
            }
            PlayerPrefs.DeleteAll();
        }
    }
}