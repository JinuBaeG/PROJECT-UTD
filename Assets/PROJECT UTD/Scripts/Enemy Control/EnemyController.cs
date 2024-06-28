using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace UTD
{
    public class EnemyController : MonoBehaviour
    {
        public float moveSpeed = 10f;

        public Transform way1, way2, way3, way4;

        NavMeshAgent nmAgent;

        private Transform target;
        

        private void Start()
        {
            nmAgent = GetComponent<NavMeshAgent>();
            target = way2.transform;
            nmAgent.speed = moveSpeed;
        }

        private void Update()
        {
            nmAgent.SetDestination(target.position);
        }

        private void OnTriggerEnter(Collider collider)
        {
            switch (collider.gameObject.name)
            {
                case "Point1":
                    target = way2.transform;
                    break;
                case "Point2":
                    target = way3.transform;
                    break;
                case "Point3":
                    target = way4.transform;
                    break;
                case "Point4":
                    target = way1.transform;
                    break;
            }
        }
    }
}
