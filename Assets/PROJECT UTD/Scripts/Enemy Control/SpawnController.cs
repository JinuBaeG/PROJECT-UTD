using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace UTD
{
    public class SpawnController : MonoBehaviour
    {
        [SerializeField]
        private EnemyDatabaseSO enemyDB;

        private GameObject enemyPrefab;

        private int spawnCount = 0;
        private int maxSpawnCount = 25;
        private float spawnTime = 2.0f;

        private float tempTime = 0f;

        private void SpawnInit(int enemyIndex)
        {
            GameObject enemyObj = null;
            enemyPrefab = enemyDB.objectsData[enemyIndex].Prefab;
            enemyObj = Instantiate(enemyPrefab);
            enemyObj.transform.position = transform.position;
        }

        private void Update()
        {
            tempTime += Time.deltaTime;
            
            if (tempTime >= spawnTime && spawnCount < maxSpawnCount)
            {
                SpawnInit(0);
                tempTime = 0f;
                spawnCount++;
                Debug.Log(spawnCount);
            }
        }
    }
}
