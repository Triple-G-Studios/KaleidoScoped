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

            GameObject blueSpawnPointsInstance = Instantiate(blueSpawnPointsPrefab);
            GameObject redSpawnPointsInstance = Instantiate(redSpawnPointsPrefab);

            Transform[] blueSpawnPoints = blueSpawnPointsInstance.GetComponentsInChildren<Transform>();
            Transform[] redSpawnPoints = redSpawnPointsInstance.GetComponentsInChildren<Transform>();

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
