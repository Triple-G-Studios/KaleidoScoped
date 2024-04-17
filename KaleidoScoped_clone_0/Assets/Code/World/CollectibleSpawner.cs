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
        public GameObject currentCollectible1, currentCollectible2, currentCollectible3;
        public int index1 = -1, index2 = -1, index3 = -1;

        // Update is called once per frame
        void Update()
        {
            prefabIndex = Random.Range(0, prefabs.Length);
            spawnIndex = Random.Range(0, spawnPoints.Length);
            bool thisOne = false;


            if (!(spawnIndex == index1 || spawnIndex == index2 || spawnIndex == index3))
            {
                if (currentCollectible1 == null && !thisOne)
                {
                    thisOne = true;
                    index1 = spawnIndex;
                    currentCollectible1 = Instantiate(prefabs[prefabIndex], spawnPoints[spawnIndex].transform.position, Quaternion.identity);
                }
                if (currentCollectible2 == null && !thisOne)
                {
                    thisOne = true;
                    index2 = spawnIndex;
                    currentCollectible2 = Instantiate(prefabs[prefabIndex], spawnPoints[spawnIndex].transform.position, Quaternion.identity);
                }
                if (currentCollectible3 == null && !thisOne)
                {
                    thisOne = true;
                    index3 = spawnIndex;
                    currentCollectible3 = Instantiate(prefabs[prefabIndex], spawnPoints[spawnIndex].transform.position, Quaternion.identity);
                }
            }
        }
    }
}