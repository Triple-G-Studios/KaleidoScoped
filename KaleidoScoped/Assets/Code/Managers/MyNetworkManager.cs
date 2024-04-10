using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

namespace Kaleidoscoped
{
    public class MyNetworkManager : NetworkManager
    {
        public RespawnManager respawnManager;

        public override void OnStartServer()
        {
            base.OnStartServer();

            Debug.Log("Server started");

            GameObject[] blueSpawnPointObjects = GameObject.FindGameObjectsWithTag("BlueSpawn");
            GameObject[] redSpawnPointObjects = GameObject.FindGameObjectsWithTag("RedSpawn");

            // Convert GameObject arrays to Transform arrays
            Transform[] blueSpawnPoints = new Transform[blueSpawnPointObjects.Length];
            for (int i = 0; i < blueSpawnPointObjects.Length; i++)
            {
                blueSpawnPoints[i] = blueSpawnPointObjects[i].transform;
            }

            Transform[] redSpawnPoints = new Transform[redSpawnPointObjects.Length];
            for (int i = 0; i < redSpawnPointObjects.Length; i++)
            {
                redSpawnPoints[i] = redSpawnPointObjects[i].transform;
            }

            respawnManager.blueSpawnPoints = blueSpawnPoints;
            respawnManager.redSpawnPoints = redSpawnPoints;
        }

        public override void OnServerAddPlayer(NetworkConnectionToClient conn)
        {
            base.OnServerAddPlayer(conn);

            Debug.Log("Adding player");

            int playerTeam = PlayerPrefs.GetInt("team", 1);
            Debug.Log("Player team: " + playerTeam);
            bool isBlueTeam = (playerTeam == 1);
            Vector3 spawnPoint = respawnManager.GetSpawnPoint(isBlueTeam);
            Debug.Log("Spawn point: " + spawnPoint);

            GameObject player = Instantiate(playerPrefab, spawnPoint, Quaternion.identity);
            NetworkServer.AddPlayerForConnection(conn, player);
        }
    }
}
