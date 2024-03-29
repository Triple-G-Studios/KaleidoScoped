using UnityEngine;
using Mirror;
using System.Collections;

namespace Kaleidoscoped
{
    public class RespawnManager : NetworkBehaviour
    {
        public float respawnTime = 5f;
        public Transform respawnPoint;

        [Server]
        public void RespawnPlayer(GameObject player)
        {
            StartCoroutine(RespawnCoroutine(player));
        }

        private IEnumerator RespawnCoroutine(GameObject player)
        {
            // Inform all clients to deactivate this player
            RpcDeactivatePlayer(player);

            yield return new WaitForSeconds(respawnTime);

            Vector3 spawnPoint = GetSpawnPoint();
            RpcSetPlayerPosition(player, spawnPoint);


            // Inform all clients to reactivate the player and reset health
            RpcReactivatePlayer(player);
        }

        [ClientRpc]
        void RpcSetPlayerPosition(GameObject player, Vector3 position)
        {
            player.transform.position = position;
        }

        // Deactivate the player on all clients
        [ClientRpc]
        void RpcDeactivatePlayer(GameObject player)
        {
            player.SetActive(false);
        }

        // Reactivate the player and reset health on all clients
        [ClientRpc]
        void RpcReactivatePlayer(GameObject player)
        {
            player.SetActive(true);
            var playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.health = 100f;
            }
        }

        private Vector3 GetSpawnPoint()
        {
            return respawnPoint.position;
        }
    }
}
