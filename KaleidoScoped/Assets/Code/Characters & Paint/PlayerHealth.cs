using UnityEngine;
using Mirror;

namespace Kaleidoscoped
{
    public class PlayerHealth : NetworkBehaviour
    {
        public float health = 100f;
        public RespawnManager respawnManager;

        public RespawnMessageController respawnMessageController;

        [Server]
        public void TakeDamage(float damage)
        {
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
    }
}

