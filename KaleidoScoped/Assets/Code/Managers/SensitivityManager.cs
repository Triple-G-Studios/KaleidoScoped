using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Kaleidoscoped
{
    public class SensitivityManager : MonoBehaviour
    {
        [SerializeField] Slider sensitivitySlider;

        void Start()
        {
            if (!PlayerPrefs.HasKey("mouseSensitivity"))
            {
                PlayerPrefs.SetFloat("mouseSensitivity", 5f);
                Load();
            }
            else
            {
                Load();
            }
        }

        public void ChangeSensitivity()
        {
            LukePlayerMovement.mouseSensitivity = sensitivitySlider.value * 5f;
            Save();
        }

        private void Load()
        {
            sensitivitySlider.value = PlayerPrefs.GetFloat("mouseSensitivity");
        }

        private void Save()
        {
            PlayerPrefs.SetFloat("mouseSensitivity", sensitivitySlider.value * 5f);
        }
    }
}
