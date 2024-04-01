using UnityEngine;
using Mirror;
using System.Collections;

namespace Kaleidoscoped
{
    public class RespawnManager : NetworkBehaviour
    {
        public float respawnTime = 5f;
        public Transform[] blueSpawnPoints;
        public Transform[] redSpawnPoints;

        [Server]
        public void RespawnPlayer(GameObject player, bool isBlueTeam)
        {
            StartCoroutine(RespawnCoroutine(player, isBlueTeam));
        }

        private IEnumerator RespawnCoroutine(GameObject player, bool isBlueTeam)
        {
            // Inform all clients to deactivate this player
            RpcDeactivatePlayer(player);

            yield return new WaitForSeconds(respawnTime);

            Vector3 spawnPoint = GetSpawnPoint(isBlueTeam);
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

        private Vector3 GetSpawnPoint(bool isBlueTeam)
        {
            if (isBlueTeam)
            {
                return blueSpawnPoints[Random.Range(0, blueSpawnPoints.Length)].position;
            } else
            {
                return redSpawnPoints[Random.Range(0, redSpawnPoints.Length)].position;
            }
        }
    }
}
