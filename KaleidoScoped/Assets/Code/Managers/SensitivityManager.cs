using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Kaleidoscoped
{
    // Took from our previous semester project
    public class SensitivityManager : MonoBehaviour
    {
        [SerializeField] Slider sensitivitySlider;

        void Start()
        {
            if (!PlayerPrefs.HasKey("mouseSensitivity"))
            {
                PlayerPrefs.SetFloat("mouseSensitivity", 1);
                Load();
            }

            else
            {
                Load();
            }
        }

        public void ChangeSensitivity()
        {
            FPSController.mouseSensitivity = sensitivitySlider.value;
            Save();
        }

        private void Load()
        {
            sensitivitySlider.value = PlayerPrefs.GetFloat("mouseSensitivity");
        }

        private void Save()
        {
            PlayerPrefs.SetFloat("mouseSensitivity", sensitivitySlider.value);
        }
    }
}
