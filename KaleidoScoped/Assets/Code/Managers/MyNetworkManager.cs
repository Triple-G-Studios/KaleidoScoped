using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

namespace Kaleidoscoped
{
    public class MyNetworkManager : NetworkManager
    {
        [SerializeField]
        public RespawnManager respawnManager;

        public bool SpawnAsCharacter = true;

        public static new MyNetworkManager singleton => (MyNetworkManager)NetworkManager.singleton;
        private CharacterData characterData;

        public override void Awake()
        {
            characterData = CharacterData.characterDataSingleton;
            if (characterData == null)
            {
                Debug.Log("Add CharacterData prefab singleton into the scene.");
                return;
            }
            base.Awake();
        }

        public struct CreateCharacterMessage : NetworkMessage
        {
            public string playerName;
            public int characterNumber;
            public Color characterColor;
            public int teamId;
        }

        public struct ReplaceCharacterMessage : NetworkMessage
        {
            public CreateCharacterMessage createCharacterMessage;
        }

        public override void OnStartServer()
        {
            base.OnStartServer();

            if (respawnManager == null)
            {
                Debug.LogError("RespawnManager not assigned. Please assign it in the Unity Editor.");
                return;
            }

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

            NetworkServer.RegisterHandler<CreateCharacterMessage>(OnCreateCharacter);
            NetworkServer.RegisterHandler<ReplaceCharacterMessage>(OnReplaceCharacterMessage);
        }

        public override void OnClientConnect()
        {
            base.OnClientConnect();

            if (SpawnAsCharacter)
            {
                // you can send the message here, or wherever else you want
                CreateCharacterMessage characterMessage = new CreateCharacterMessage
                {
                    playerName = StaticVariables.playerName,
                    characterNumber = StaticVariables.characterNumber,
                    characterColor = StaticVariables.characterColor,
                    teamId = StaticVariables.teamId
                };

                NetworkClient.Send(characterMessage);
            }
        }

        void OnCreateCharacter(NetworkConnectionToClient conn, CreateCharacterMessage msg)
        {
            if (respawnManager == null)
            {
                Debug.LogWarning("RespawnManager is not assigned. Using default spawn point.");
                // Handle the case where respawnManager is not assigned, e.g., provide a default spawn point
                return;
            }

            // int team = PlayerPrefs.GetInt("team", 1);
            int team = msg.teamId;

            print("Team: " + team);

            bool isBlueTeam = (msg.teamId == 1);

            print("Is blue team: " + isBlueTeam);
            Debug.Log("Team " + team);

            Transform startPos = respawnManager.GetSpawnPoint(isBlueTeam);
            if (startPos == null)
            {
                Debug.LogWarning("Start position is not set. Using default spawn position.");
                // Handle the case where startPos is not assigned, e.g., provide a default spawn point
                return;
            }

            // check if the save data has been pre-set
            if (msg.playerName == "")
            {
                Debug.Log("OnCreateCharacter name invalid or not set, use random.");
                msg.playerName = "Player: " + UnityEngine.Random.Range(100, 1000);
            }

            // check if the save data has been pre-set
            if (msg.characterColor == new Color(0, 0, 0, 0))
            {
                Debug.Log("OnCreateCharacter colour invalid or not set, use random.");
                msg.characterColor = Random.ColorHSV(0f, 1f, 1f, 1f, 0f, 1f);
            }

            GameObject playerObject = startPos != null
                ? Instantiate(characterData.characterPrefabs[msg.characterNumber], startPos.position, startPos.rotation)
                : Instantiate(characterData.characterPrefabs[msg.characterNumber]);

            // Apply data from the message however appropriate for your game
            // Typically Player would be a component you write with syncvars or properties
            CharacterSelection characterSelection = playerObject.GetComponent<CharacterSelection>();
            characterSelection.playerName = msg.playerName;
            characterSelection.characterNumber = msg.characterNumber;
            characterSelection.characterColor = msg.characterColor;

            // call this to use this gameobject as the primary controller
            NetworkServer.AddPlayerForConnection(conn, playerObject);
        }

        void OnReplaceCharacterMessage(NetworkConnectionToClient conn, ReplaceCharacterMessage message)
        {
            // Cache a reference to the current player object
            GameObject oldPlayer = conn.identity.gameObject;

            GameObject playerObject = Instantiate(characterData.characterPrefabs[message.createCharacterMessage.characterNumber], oldPlayer.transform.position, oldPlayer.transform.rotation);

            // Instantiate the new player object and broadcast to clients
            // Include true for keepAuthority paramater to prevent ownership change
            NetworkServer.ReplacePlayerForConnection(conn, playerObject, true);

            // Apply data from the message however appropriate for your game
            // Typically Player would be a component you write with syncvars or properties
            CharacterSelection characterSelection = playerObject.GetComponent<CharacterSelection>();
            characterSelection.playerName = message.createCharacterMessage.playerName;
            characterSelection.characterNumber = message.createCharacterMessage.characterNumber;
            characterSelection.characterColor = message.createCharacterMessage.characterColor;

            // Remove the previous player object that's now been replaced
            // Delay is required to allow replacement to complete.
            Destroy(oldPlayer, 0.1f);
        }

        public void ReplaceCharacter(ReplaceCharacterMessage message)
        {
            NetworkClient.Send(message);
        }

        /*public override void OnServerAddPlayer(NetworkConnectionToClient conn)
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
        }*/
    }
}
