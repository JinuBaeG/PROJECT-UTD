using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UTD
{
    public class InputManager : MonoBehaviour
    {
        [SerializeField]
        private Camera sceneCamera;

        private Vector3 lastPosition;

        [SerializeField]
        private LayerMask placementLayerMask;

        public Vector3 GetSelectedMapPosition()
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = sceneCamera.nearClipPlane;
            Ray ray = sceneCamera.ScreenPointToRay(mousePos);
            
            if (Physics.Raycast(ray, out RaycastHit hit, 100, placementLayerMask))
            {
                lastPosition = hit.point;
            }
            
            return lastPosition;
        }

    }
}
