using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Kaleidoscoped
{
    public class SFXManager : MonoBehaviour
    {
        public static SFXManager instance;

        // Outlets
        [SerializeField] Slider sfxSlider;
        AudioSource AudioSource;
        public AudioClip splatSound;
        public AudioClip shootSound;
        public AudioClip grassySound;
        public AudioClip rockySound;
        public AudioClip gameOverSound;
        public AudioClip victorySound;

        void Awake()
        {
            instance = this;
        }

        void Start()
        {
            AudioSource = GetComponent<AudioSource>();

            if (!PlayerPrefs.HasKey("sfxVolume"))
            {
                PlayerPrefs.SetFloat("sfxVolume", 1);
                Load();
            } else
            {
                Load();
            }
        }

        public void PlaySoundSplat()
        {
            AudioSource.PlayOneShot(splatSound);
        }

        public void PlaySoundShoot()
        {
            AudioSource.PlayOneShot(shootSound);
        }

        public void PlayGrassySound()
        {
            AudioSource.PlayOneShot(shootSound);
        }

        public void PlayRockySound()
        {
            AudioSource.PlayOneShot(shootSound);
        }

        public void PlayGameOverSound()
        {
            AudioSource.PlayOneShot(shootSound);
        }

        public void PlayVictorySound()
        {
            AudioSource.PlayOneShot(shootSound);
        }

        public void ChangeVolume()
        {
            AudioSource.volume = sfxSlider.value;
            Save();
        }

        private void Load()
        {
            sfxSlider.value = PlayerPrefs.GetFloat("sfxVolume");
        }

        private void Save()
        {
            PlayerPrefs.SetFloat("sfxVolume", sfxSlider.value);
        }
    }
}
