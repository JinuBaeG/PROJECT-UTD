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

        private Transform camera;

        private void Start()
        {
            linkedEnemy.onDamageCallback += RefreshHpBar;
            camera = Camera.main.transform;
        }

        private void Update()
        {
            transform.LookAt(transform.position + camera.rotation * Vector3.forward, camera.rotation * Vector3.up);
        }

        public void RefreshHpBar(float healthPoint, float maxHealthPoint)
        {
            Debug.Log(healthPoint + " / " + maxHealthPoint);
            hpBar.fillAmount = healthPoint / maxHealthPoint;
        }
    }
}
