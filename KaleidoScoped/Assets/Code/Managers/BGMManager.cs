using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kaleidoscoped
{
    public class BGMManager : MonoBehaviour
    {
        // Outlets
        AudioSource AudioSource;
        public AudioClip[] BGMClips;

        void Start()
        {
            AudioSource = GetComponent<AudioSource>();
            System.Random random = new();
            int i = random.Next(0, 5);
            AudioClip current = BGMClips[i];
            AudioSource.clip = current;
            AudioSource.Play();
        }
    }
}