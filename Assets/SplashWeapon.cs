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

        private int damage;
        private int damageRange;

        public void Init(int damage, int damageRange, EnemyController enemy)
        {
            this.damage = damage;
            this.damageRange = damageRange;
            target = enemy.transform;
        }

        private void FixedUpdate()
        {
            if(!target)
            {
                Destroy(gameObject);
                return;
            }

            float dist = Vector3.Distance(target.position, transform.position);
            
            if(dist < 1.0f)
            {
                Explode(damage, damageRange);
            }

            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, damageRange);
        }

        private void Explode(int damage, int damageRange)
        {
            var newMuzzle = Instantiate(bombMuzzle);
            newMuzzle.transform.SetPositionAndRotation(target.position, target.rotation);
            Destroy(newMuzzle, 1f);

            Collider[] colliders = Physics.OverlapSphere(transform.position, damageRange);

            foreach(Collider col in colliders)
            {
                if(col.tag == "Enemy")
                {
                    col.GetComponent<EnemyController>().Damage(damage);
                }
            }

            Destroy(gameObject);
        }

        private void Thrower(int damage, int damageRage)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, damageRange);


        }

        private void Lazor()
        {
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit[] hitObjects = Physics.SphereCastAll(ray, 0f, 0f);
            if (hitObjects != null)
            {
            }
        }
    }
}
