using UnityEngine;
using Mirror;
using System.Collections;

namespace Kaleidoscoped
{
    public class PlayerHealth : NetworkBehaviour
    {
        public float health = 100f;
        public RespawnManager respawnManager;

        private bool canTakeDmg = true;

        public RespawnMessageController respawnMessageController;

        [Server]
        public void TakeDamage(float damage)
        {
            if (!canTakeDmg)
            {
                Debug.Log("Player cannot be damaged currently");
                return;
            }
            
            health -= damage;

            if (health <= 0f)
            {
                health = 0f;
                RpcHandleDeath();

                // Get the CharacterSelection component of the player object
                CharacterSelection characterSelection = GetComponent<CharacterSelection>();
                if (characterSelection != null)
                {
                    bool isBlueTeam = (characterSelection.teamId == 1);
                    respawnManager.RespawnPlayer(gameObject, isBlueTeam);
                }
                else
                {
                    Debug.LogError("CharacterSelection component not found on player object.");
                }
            }
        }

        [ClientRpc]
        void RpcHandleDeath()
        {
            if (isLocalPlayer && respawnMessageController != null)
            {
                respawnMessageController.ShowDeathMessage();
            }
        }

        private IEnumerator DisableDmgForPeriod(float period)
        {
            canTakeDmg = false;
            yield return new WaitForSeconds(period);
            canTakeDmg = true;
        }

        public void Invulnerable()
        {
            StartCoroutine(DisableDmgForPeriod(3f));
        }
    }
}

