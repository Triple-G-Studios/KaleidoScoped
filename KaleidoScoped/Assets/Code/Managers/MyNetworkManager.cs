using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

namespace Kaleidoscoped
{
    public class MyNetworkManager : NetworkManager
    {
        public RespawnManager respawnManager;

        public GameObject blueSpawnPointsPrefab;
        public GameObject redSpawnPointsPrefab;

        public override void OnStartServer()
        {
            base.OnStartServer();

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

            int playerTeam = PlayerPrefs.GetInt("team", 1);
            print("TEAM " + playerTeam);
            bool isBlueTeam = (playerTeam == 1);
            Vector3 spawnPoint = respawnManager.GetSpawnPoint(isBlueTeam);

            GameObject player = Instantiate(playerPrefab, spawnPoint, Quaternion.identity);
            NetworkServer.AddPlayerForConnection(conn, player);
        }
    }
}
