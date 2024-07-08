using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UTD
{
    public class EnemyUI : MonoBehaviour
    {
        public Image hpBar;

        public EnemyController linkedEnemy;

        private void Start()
        {
            linkedEnemy.onDamageCallback += RefreshHpBar;
        }

        public void RefreshHpBar(float healthPoint, float maxHealthPoint)
        {
            hpBar.fillAmount = healthPoint / maxHealthPoint;
        }
    }
}
