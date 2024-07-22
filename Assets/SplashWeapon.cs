using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UTD
{
    public class SplashWeapon : MonoBehaviour
    {
        public float speed = 100.0f;
        public GameObject bombMuzzle;

        private Transform target;
        private EnemyController enemy;

        private int damage;
        private int damageRange;

        public void Init(int damage, int damageRange, EnemyController enemy)
        {
            this.damage = damage;
            this.damageRange = damageRange;
            this.enemy = enemy;
            target = enemy.transform;
        }

        private void FixedUpdate()
        {
            Vector3 distance = target.position - transform.position;
            
            distance.Normalize();
            Debug.Log("Target : " + target.position + " / Current : " + transform.position + " / distance : " + distance);

            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }

        public void OnCollisionEnter(Collision collision)
        {
            Explode(damage, damageRange, enemy);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(target.position, damageRange);
        }

        public void Explode(int damage, int damageRange, EnemyController enemy)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, damageRange);

            foreach(Collider col in colliders)
            {
                if(col.tag == "Enemy")
                {
                    enemy.Damage(damage);
                }
            }

            Destroy(gameObject);
        }
    }
}
