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
                respawnManager.RespawnPlayer(gameObject);
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

