using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UTD
{
    public class TurretController : MonoBehaviour
    {
        public Scanner scanner;
        public Projector projector;
        public Animator anim;

        public GameObject muzzlePrefab;
        public GameObject bulletPrefab;
        public Transform firePosition;
        public Transform target;
        private SplashWeapon splashWeapon;
        private EnemyController enemy;
        

        private float lastShootTime = 0f;
        public float attackSpeed = 0f;
        public int damage;
        public int damageRange;
        public bool isSplash;
        public int sellPrice;

        public bool isPreview;

        private void Awake()
        {
            projector.enabled = true;
            isPreview = true;
        }

        private void Update()
        {
            if(isPreview)
            {
                return;
            }

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

            Attack();
        }

        public void Idle()
        {
            transform.Rotate(new Vector3(0, 30.0f * Time.deltaTime, 0));

            if (anim)
            {
                anim.SetBool("isDetect", false);
            }

        }

        public void Attack()
        {
            if (Time.time > lastShootTime + attackSpeed)
            {
                // Possible Shoot
                lastShootTime = Time.time;

                enemy = target.GetComponent<EnemyController>();


                if (anim)
                {
                    anim.SetBool("isDetect", true);
                }

                var newMuzzle = Instantiate(muzzlePrefab);
                newMuzzle.transform.SetPositionAndRotation(firePosition.position, firePosition.rotation);

                if(transform.CompareTag("Uncommon"))
                {
                    Destroy(newMuzzle, 0.1f);
                }
                else
                {
                    Destroy(newMuzzle, 1f);
                }


                if (!isSplash)
                {
                    enemy.Damage(damage);
                }
                else
                {
                    var bullet = Instantiate(bulletPrefab, firePosition.position, firePosition.rotation);
                    splashWeapon = bullet.GetComponent<SplashWeapon>();
                    splashWeapon.Init(damage, damageRange, enemy);
                }

            }
        }
    }
}
