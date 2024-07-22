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

        private Transform cam;

        private void Start()
        {
            linkedEnemy.onDamageCallback += RefreshHpBar;
            cam = Camera.main.transform;
        }

        private void Update()
        {
            transform.LookAt(transform.position + cam.rotation * Vector3.forward, cam.rotation * Vector3.up);
        }

        public void RefreshHpBar(float healthPoint, float maxHealthPoint)
        {
            hpBar.fillAmount = healthPoint / maxHealthPoint;
        }
    }
}
