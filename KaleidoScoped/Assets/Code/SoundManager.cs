using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kaleidoscope
{
    public class SoundManager : MonoBehaviour
    {
        public static SoundManager instance;

        // Outlet
        AudioSource audioSource;
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
            audioSource = GetComponent<AudioSource>();
        }

        public void PlaySoundSplat()
        {
            audioSource.PlayOneShot(splatSound);
        }

        public void PlaySoundShoot()
        {
            audioSource.PlayOneShot(shootSound);
        }

        public void PlayGrassySound()
        {
            audioSource.PlayOneShot(shootSound);
        }

        public void PlayRockySound()
        {
            audioSource.PlayOneShot(shootSound);
        }

        public void PlayGameOverSound()
        {
            audioSource.PlayOneShot(shootSound);
        }

        public void PlayVictorySound()
        {
            audioSource.PlayOneShot(shootSound);
        }

    }
}
