using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace UTD
{

    // 전체 게임 시간 컨트롤 : 라운드 시간, 전체 게임 시간, 스폰 시간
    // 

    public class GameManager : SingletonBase<GameManager>
    {
        //bool isPlay = true;

        [Header("# Game Control")]
        public bool gameIsLive;

        public float gameTime;
        public float maxGametime;

        public int maxSpawnEnemy;
        public int round;
        public int maxRound;
        public float enemySpawnTime;
        public float roundTime;
        public float pauseTime;

        [Header("# Player Info")]

        public int kill;
        public int resource;

        [SerializeField]
        private TMP_Text killCount;
        [SerializeField]
        private TMP_Text resourceCount;
        [SerializeField]
        private Button buyButton;

        public void killEnemy()
        {
            kill++;
            resource++;
        }

        public void buyRandomTurret()
        {
            resource -= 10;
        }

        public void sellTurret(int sellPrice)
        {
            resource += sellPrice;
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

            if(resource < 10)
            {
                buyButton.interactable = false;
            } else
            {
                buyButton.interactable = true;
            }

            gameTime += Time.deltaTime;

            killCount.SetText(kill.ToString());
            resourceCount.SetText(resource.ToString());
        }
    }
}
