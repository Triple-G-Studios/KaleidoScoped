using UnityEngine;
using Mirror;
using System.Collections;
using static Kaleidoscoped.MyNetworkManager;
using UnityEngine.InputSystem;

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

            Transform spawnPoint = GetSpawnPoint(isBlueTeam);
            RpcSetPlayerPosition(player, spawnPoint.position, spawnPoint.rotation);


            // Inform all clients to reactivate the player and reset health
            RpcReactivatePlayer(player);
            player.GetComponent<PlayerInput>().enabled = true;
        }

        [ClientRpc]
        void RpcSetPlayerPosition(GameObject player, Vector3 position, Quaternion rotation)
        {
            if (player != null && position != null && rotation != null)
            {
                player.GetComponent<PlayerInput>().enabled = false;
                player.transform.position = position;
                player.transform.rotation = rotation;
            }
            else
            {
                Debug.LogError("Either player/vector3/rotation are null in RpcSetPlayerPosition.");
            }
        }

        // Deactivate the player on all clients
        [ClientRpc]
        void RpcDeactivatePlayer(GameObject player)
        {
            player.SetActive(false);
            // Destroy(player);
        }

        // Reactivate the player and reset health on all clients
        [ClientRpc]
        void RpcReactivatePlayer(GameObject player)
        {
            /*if (true)
            {
                // you can send the message here, or wherever else you want
                CreateCharacterMessage characterMessage = new CreateCharacterMessage
                {
                    playerName = StaticVariables.playerName,
                    characterNumber = StaticVariables.characterNumber,
                    characterColor = StaticVariables.characterColor,
                    teamId = StaticVariables.teamId
                };

                print("REAL TEAM " + characterMessage.teamId);
                NetworkClient.Send(characterMessage);
            }*/

            player.SetActive(true);
            var playerHealth = player.GetComponent<PlayerHealth>();
            playerHealth.Invulnerable();
            if (playerHealth != null)
            {
                playerHealth.health = 100f;
            }
        }

        public Transform GetSpawnPoint(bool isBlueTeam)
        {
            if (isBlueTeam)
            {
                return blueSpawnPoints[Random.Range(0, blueSpawnPoints.Length)].transform;
            } else
            {
                return redSpawnPoints[Random.Range(0, redSpawnPoints.Length)].transform;
            }
        }
    }
}
