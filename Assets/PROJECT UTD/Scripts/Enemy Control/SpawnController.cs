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

        private EnemyController enemy;

        private int enemyIndex = 0;
        private int spawnCount = 0;
        private int maxSpawnCount = 25;
        private float spawnTime = 2.0f;

        private float tempTime = 0f;

        private void Update()
        {
            tempTime += Time.deltaTime;
            
            if (tempTime >= spawnTime && spawnCount < maxSpawnCount)
            {
                SpawnInit(enemyIndex);
                tempTime = 0f;
                spawnCount++;
            }
        }

        private void SpawnInit(int enemyIndex)
        {
            GameObject enemyObj = null;

            // Get Enemy DataBase
            enemyPrefab = enemyDB.objectsData[enemyIndex].Prefab;
            enemy = enemyPrefab.GetComponent<EnemyController>();
            // Init Enemy Data
            enemy.Init(enemyDB.objectsData[enemyIndex].MoveSpeed, enemyDB.objectsData[enemyIndex].HealthPoint);
            // Spawn Enemy
            enemyObj = Instantiate(enemyPrefab);
            enemyObj.transform.position = transform.position;
            
        }
    }
}
