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
        private float spawnTime = 0f;
        private float pauseTime = 0f;
        private float roundTime = 0;

        private void Update()
        {
            GameManager.Singleton.gameTime += Time.deltaTime;
            spawnTime += Time.deltaTime;
            roundTime += Time.deltaTime;


            if (spawnTime >= GameManager.Singleton.enemySpawnTime && spawnCount < maxSpawnCount)
            {
                SpawnInit(enemyIndex);
                spawnTime = 0f;
                spawnCount++;
            }

            // 쉬는 시간 표시 하고 라운드 타임 안보이게
            if(spawnCount == maxSpawnCount && GameManager.Singleton.roundTime <= roundTime)
            {
                pauseTime += Time.deltaTime;

                // 쉬는 시간 표시 없애고 라운드 카운트 증가 및 스폰 수 초기화, 라운드 타임 표시 및 초기화
                if (pauseTime >= GameManager.Singleton.pauseTime)
                {
                    GameManager.Singleton.round++;
                    enemyIndex = GameManager.Singleton.round;
                    spawnCount = 0;
                    pauseTime = 0f;
                    GameManager.Singleton.roundTime = 0f;
                }
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
