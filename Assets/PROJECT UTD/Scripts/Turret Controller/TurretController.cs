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
            //Debug.Log(damage + " / " + attackSpeed);
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
            Vector3 targetPos = scanner.nearestTarget.position;

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
            if (Time.time > lastShootTime + 0.5f)
            {
                // Possible Shoot
                lastShootTime = Time.time;

                var newMuzzle = Instantiate(muzzlePrefab);
                newMuzzle.transform.SetPositionAndRotation(firePosition.position, firePosition.rotation);
                Destroy(newMuzzle, 1f);
            }

        }
    }
}
