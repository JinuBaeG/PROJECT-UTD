using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SCU
{
    public class QuaterViewCameraSystem : MonoBehaviour
    {
        public float cameraRotateSpeed = 50f;
        public float cameraMoveSpeed = 50f;
        public float zoomSensitvity = 10f;

        public Camera mainCamera;
        public Transform cameraPivot;
        public Cinemachine.CinemachineVirtualCamera quaterViewCamera;

        public Cinemachine.CinemachineComponentBase componentBase;

        float cameraDistance;
        private float cameraMaxDistance = 80.0f;
        private float cameraMinDistance = 5.0f;

        private void Update()
        {
            float rotateDirection = 0f;
            if (Input.GetKey(KeyCode.Q))
            {
                rotateDirection = -1;
                Vector3 originalRot = quaterViewCamera.transform.localRotation.eulerAngles;
                quaterViewCamera.transform.localRotation = Quaternion.Euler(originalRot +
                    (Vector3.up * rotateDirection * cameraRotateSpeed * Time.deltaTime));
            }

            if (Input.GetKey(KeyCode.E))
            {
                rotateDirection = 1;
                Vector3 originalRot = quaterViewCamera.transform.localRotation.eulerAngles;
                quaterViewCamera.transform.localRotation = Quaternion.Euler(originalRot +
                    (Vector3.up * rotateDirection * cameraRotateSpeed * Time.deltaTime));
            }

            Vector3 cameraForward = mainCamera.transform.forward;
            Vector3 cameraRight = mainCamera.transform.right;
            cameraForward.y = 0;
            cameraRight.y = 0;

            if (Input.GetKey(KeyCode.W))
            {
                cameraPivot.Translate(cameraForward * cameraMoveSpeed  * Time.deltaTime, Space.World);
            }

            if (Input.GetKey(KeyCode.S))
            {
                cameraPivot.Translate(cameraForward * cameraMoveSpeed * (-1) * Time.deltaTime, Space.World);
            }

            if (Input.GetKey(KeyCode.A))
            {
                cameraPivot.Translate(cameraRight * cameraMoveSpeed * (-1) * Time.deltaTime, Space.World);
            }

            if (Input.GetKey(KeyCode.D))
            {
                cameraPivot.Translate(cameraRight * cameraMoveSpeed  * Time.deltaTime, Space.World);
            }

            if(componentBase == null)
            {
                componentBase = quaterViewCamera.GetCinemachineComponent(Cinemachine.CinemachineCore.Stage.Body);
            }

            if (Input.GetAxis("Mouse ScrollWheel") != 0)
            {
                cameraDistance = Input.GetAxis("Mouse ScrollWheel") * zoomSensitvity;
                if(componentBase is Cinemachine.CinemachineFramingTransposer)
                {
                    (componentBase as Cinemachine.CinemachineFramingTransposer).m_CameraDistance -= cameraDistance;
                    if((componentBase as Cinemachine.CinemachineFramingTransposer).m_CameraDistance > cameraMaxDistance) {
                        (componentBase as Cinemachine.CinemachineFramingTransposer).m_CameraDistance = cameraMaxDistance;
                    } else if ((componentBase as Cinemachine.CinemachineFramingTransposer).m_CameraDistance < cameraMinDistance)
                    {
                        (componentBase as Cinemachine.CinemachineFramingTransposer).m_CameraDistance = cameraMinDistance;
                    }
                            
                }
            }

        }
    }
}
