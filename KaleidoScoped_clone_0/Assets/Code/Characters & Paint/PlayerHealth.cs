using UnityEngine;
using Mirror;
using System.Collections;

namespace Kaleidoscoped
{
    public class PlayerHealth : NetworkBehaviour
    {
        public float health = 100f;
        public RespawnManager respawnManager;
        public GameObject forceField;

        private bool canTakeDmg = true;

        public RespawnMessageController respawnMessageController;

        [Server]
        public void TakeDamage(float damage, GameObject shooter)
        {
            if (!canTakeDmg)
            {
                Debug.Log("Player cannot be damaged currently");
                return;
            }
            
            health -= damage;

            // Add a point to the team of the shooter
            CharacterSelection shooterCharacterSelection = shooter.GetComponent<CharacterSelection>();
            TeamKillCounter teamKillCounter = GameObject.FindWithTag("TeamKillCounter").GetComponent<TeamKillCounter>();
            if (teamKillCounter != null)
            {
                teamKillCounter.IncrementTeamKills(shooterCharacterSelection.teamId);
            }
            else
            {
                Debug.Log("No team kill counter found");
            }

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
            forceField.SetActive(true);
            yield return new WaitForSeconds(period);
            forceField.SetActive(false);
            canTakeDmg = true;
        }

        public void Invulnerable()
        {
            StartCoroutine(DisableDmgForPeriod(3f));
        }
    }
}

