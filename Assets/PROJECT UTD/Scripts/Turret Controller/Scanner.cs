using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UTD
{
    public class Scanner : MonoBehaviour
    {
        public float scanRange;
        public LayerMask targetLayer;
        public Collider[] targets;
        public Transform nearestTarget;

        private void FixedUpdate()
        {
            targets = Physics.OverlapSphere(transform.position, scanRange, targetLayer);
            nearestTarget = GetNearest();
        }

        Transform GetNearest()
        {
            Transform result = null;
            float diff = 10;

            foreach (Collider target in targets)
            {
                Vector3 playerPos = transform.position;
                Vector3 targetPos = target.transform.position;
                float curDiff = Vector3.Distance(playerPos, targetPos);

                if (curDiff < diff)
                {
                    diff = curDiff;
                    result = target.transform;
                }
            }

            return result;
        }
    }
}
