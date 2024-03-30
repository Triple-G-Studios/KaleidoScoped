using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Mirror;


namespace Kaleidoscoped
{
    public class Timer : NetworkBehaviour
    {
        [SyncVar]
        public float currTime;

        [SyncVar]
        public bool gameStarted = false;

        public float startTime;

        [SerializeField] Text timerText;

        void Start()
        {
            currTime = startTime;
        }

        public override void OnStartServer()
        {
            base.OnStartServer();
            currTime = startTime;
            gameStarted = true;
        }

        void Update()
        {
            if (isServer) // Only the server updates the time
            {
                currTime -= Time.deltaTime;
                // if (currTime < 60)
                // {
                //     currTime = startTime;
                // }

                if (currTime <= 0)
                {
                    currTime = 0;
                    // Logic for when time runs out
                }
            }

            if (isClient) // Both server and clients update the display
            {
                var ts = TimeSpan.FromSeconds(currTime);
                timerText.text = string.Format("{0:00}:{1:00}", (int)ts.TotalMinutes, (int)ts.Seconds);
            }
        }

        public bool IsTimeUp()
        {

            return (currTime <= 0 && gameStarted);
        }

    }
}