using UnityEngine;
using TMPro;
using System.Collections;

namespace Kaleidoscoped
{
    public class RespawnMessageController : MonoBehaviour
    {
        public TextMeshProUGUI deathMessage;
        public TextMeshProUGUI respawnCountdown;
        public float countdownTime = 5f;

        private void Start()
        {
            deathMessage.gameObject.SetActive(false);
            respawnCountdown.gameObject.SetActive(false);
        }

        public void ShowDeathMessage()
        {
            deathMessage.gameObject.SetActive(true);
            respawnCountdown.gameObject.SetActive(true);
            StartCoroutine(CountdownToRespawn());
        }

        private IEnumerator CountdownToRespawn()
        {
            float currentTime = countdownTime;
            while (currentTime > 0)
            {
                respawnCountdown.text = "Respawning in " + currentTime.ToString("0");
                currentTime -= Time.deltaTime;
                yield return null;
            }

            deathMessage.gameObject.SetActive(false);
            respawnCountdown.gameObject.SetActive(false);
        }
    }
}

