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
                PlayerPrefs.SetFloat("mouseSensitivity", 300f);
                Load();
            }
            else
            {
                Load();
            }
        }

        public void ChangeSensitivity()
        {
            FPSController.mouseSensitivity = sensitivitySlider.value * 300f;
            Save();
        }

        private void Load()
        {
            Debug.Log("Hello " + sensitivitySlider.value);

            sensitivitySlider.value = PlayerPrefs.GetFloat("mouseSensitivity");
        }

        private void Save()
        {
            PlayerPrefs.SetFloat("mouseSensitivity", sensitivitySlider.value * 300f);
        }
    }
}
