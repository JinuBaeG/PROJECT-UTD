using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UTD
{

    // 전체 게임 시간 컨트롤 : 라운드 시간, 전체 게임 시간, 스폰 시간
    // 

    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;
        //bool isPlay = true;

        [Header("# Game Control")]
        public bool gameIsLive;

        public float gameTime;
        public float maxGametime;
        
        
        public int round;
        public int maxRound;
        public float roundTime;

        [Header("# Player Info")]

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
