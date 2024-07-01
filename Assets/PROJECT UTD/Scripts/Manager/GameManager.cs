using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UTD
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;
        //bool isPlay = true;

        [Header("# Game Control")]
        public float gameTime;
        public float maxGametime;
        public float roundTime;
        public bool gameIsLive;

        [Header("# Player Info")]
        public float health;
        public float maxHealth;
        public int round;
        public int maxRound;
        public int kill;
        public int resource;

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            gameIsLive = true;
        }

        private void Update()
        {
            if(!gameIsLive)
            {
                return;
            }

            gameTime += Time.deltaTime;
        }
    }
}
