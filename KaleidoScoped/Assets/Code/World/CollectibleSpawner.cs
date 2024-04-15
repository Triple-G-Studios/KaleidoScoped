using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kaleidoscoped
{
    public class CollectibleSpawner : MonoBehaviour
    {
        //Outlets
        public GameObject[] spawnPoints;
        public GameObject[] prefabs;

        //State Tracking
        public int spawnIndex = 0;
        public int prefabIndex = 0;
        public GameObject currentCollectible;

        // Update is called once per frame
        void Update()
        {
            prefabIndex = Random.Range(0, prefabs.Length);

            if (currentCollectible == null) { 
                currentCollectible = Instantiate(prefabs[prefabIndex], spawnPoints[spawnIndex].transform.position, Quaternion.identity);
                spawnIndex++;

                if (spawnIndex >= spawnPoints.Length) spawnIndex = 0;
            }
        }
    }
}