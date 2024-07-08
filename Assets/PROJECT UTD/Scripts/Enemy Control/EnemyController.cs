using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace UTD
{
    public class EnemyController : MonoBehaviour
    {
        public static EnemyController instance;

        public float healthPoint;

        public float maxHealthPoint;

        public float moveSpeed = 10f;

        public Transform way1_1, way1_2, way2_1, way2_2, way3_1, way3_2, way4_1, way4_2;

        NavMeshAgent nmAgent;

        private Transform target;

        public delegate void OnDamage(float healthPoint, float maxHealthPoint);
        public OnDamage onDamageCallback;

        public void Damage(float damage)
        {
            healthPoint -= damage;

            onDamageCallback(damage, healthPoint);

            if(healthPoint <= 0)
            {
                Destroy(gameObject);
            }

        }

        [ContextMenu("Damage Debug")]
        public void DamageDebugButton()
        {
            Damage(20);
        }

        private void Awake()
        {
            instance = this;
        }

        private void OnDestroy()
        {
            instance = null;
        }

        private void Start()
        {
            nmAgent = GetComponent<NavMeshAgent>();
            target = way1_2.transform;
            nmAgent.speed = moveSpeed;
        }

        private void Update()
        {
            nmAgent.SetDestination(target.position);
        }

        public void Init(float moveSpeed, float healthPoint)
        {
            this.moveSpeed = moveSpeed;
            this.maxHealthPoint = healthPoint;
            this.healthPoint = healthPoint;
        }

        private void OnTriggerEnter(Collider collider)
        {
            switch (collider.gameObject.name)
            {
                case "Point1-1":
                    target = way1_2.transform;
                    break;
                case "Point1-2":
                    target = way2_1.transform;
                    break;
                case "Point2-1":
                    target = way2_2.transform;
                    break;
                case "Point2-2":
                    target = way3_1.transform;
                    break;
                case "Point3-1":
                    target = way3_2.transform;
                    break;
                case "Point3-2":
                    target = way4_1.transform;
                    break;
                case "Point4-1":
                    target = way4_2.transform;
                    break;
                case "Point4-2":
                    target = way1_1.transform;
                    break;
            }
        }
    }
}
