using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Kaleidoscoped
{
    public class BGMManager : MonoBehaviour
    {
        // Outlets
        [SerializeField] Slider bgmSlider;
        AudioSource AudioSource;
        public AudioClip[] BGMClips;

        void Start()
        {
            if (!PlayerPrefs.HasKey("bgmVolume"))
            {
                PlayerPrefs.SetFloat("bgmVolume", 1);
                Load();
            } else
            {
                Load();
            }

            AudioSource = GetComponent<AudioSource>();
            System.Random random = new();
            int i = random.Next(0, 5);
            AudioClip current = BGMClips[i];
            AudioSource.clip = current;
            AudioSource.Play();
        }

        public void ChangeVolume()
        {
            AudioListener.volume = bgmSlider.value;
            Save();
        }

        private void Load()
        {
            bgmSlider.value = PlayerPrefs.GetFloat("bgmVolume");
        }

        private void Save()
        {
            PlayerPrefs.SetFloat("bgmVolume", bgmSlider.value);
        }
    }
}