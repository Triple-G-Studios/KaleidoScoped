using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kaleidoscoped
{
    public class SFXManager : MonoBehaviour
    {
        public static SFXManager instance;

        // Outlets
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

    }
}
