using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UTD
{
    public class TurretController : MonoBehaviour
    {
        public Scanner scanner;
        private Animator anim;

        public GameObject muzzlePrefab;
        public Transform firePosition;
        public Transform target;
        private EnemyController enemy;

        private float lastShootTime = 0f;
        public float attackSpeed = 0f;
        public int damage;
        public bool isSplash;
        public int sellPrice;

        private void Awake()
        {
            scanner = GetComponent<Scanner>();
            anim = GetComponent<Animator>();
        }

        private void Update()
        {
            if (!scanner.nearestTarget)
            {
                Idle();
            }
            else
            {
                Scan();
            }
            
        }

        public void Scan()
        {
            target = scanner.nearestTarget;

            Vector3 targetPos = target.position;

            transform.LookAt(targetPos);

            anim.SetBool("isDetect", true);

            Attack();
        }

        public void Idle()
        {
            transform.Rotate(new Vector3(0, 30.0f * Time.deltaTime, 0));
            anim.SetBool("isDetect", false);
        }

        public void Attack()
        {
            if (Time.time > lastShootTime + attackSpeed)
            {
                // Possible Shoot
                lastShootTime = Time.time;

                enemy = target.GetComponent<EnemyController>();
                enemy.Damage(damage);

                var newMuzzle = Instantiate(muzzlePrefab);
                newMuzzle.transform.SetPositionAndRotation(firePosition.position, firePosition.rotation);
                Destroy(newMuzzle, 1f);
            }

        }
    }
}
