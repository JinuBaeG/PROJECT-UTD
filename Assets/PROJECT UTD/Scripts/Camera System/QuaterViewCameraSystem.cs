using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SCU
{
    public class QuaterViewCameraSystem : MonoBehaviour
    {
        [SerializeField]
        public float cameraRotateSpeed = 50f;
        [SerializeField]
        public float cameraMoveSpeed = 50f;
        [SerializeField]
        public float zoomSensitvity = 10f;

        [SerializeField]
        public float horizontalZone = 7.0f;
        public float verticalZone = 7.0f;

        private Vector3 beginCampos = Vector3.zero;

        public Camera mainCamera;
        public Transform cameraPivot;
        public Cinemachine.CinemachineVirtualCamera quaterViewCamera;

        public Cinemachine.CinemachineComponentBase componentBase;

        float cameraDistance;
        private float cameraMaxDistance = 80.0f;
        private float cameraMinDistance = 5.0f;

        public float cameraBorderThickness;

        private void Awake()
        {
            cameraBorderThickness = 0.001f;
        }

        private void Update()
        {
            float rotateDirection = 0f;

            // Camera Left Rotation 
            if (Input.GetKey(KeyCode.Q))
            {
                rotateDirection = -1;
                Vector3 originalRot = quaterViewCamera.transform.localRotation.eulerAngles;
                quaterViewCamera.transform.localRotation = Quaternion.Euler(originalRot +
                    (Vector3.up * rotateDirection * cameraRotateSpeed * Time.deltaTime));
            }
            // Camera Right Rotation 
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
            // Camera Zoom In/Out
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

            // Mouse Screen Border Check
            Vector3 mousePosition = Input.mousePosition;
            Vector3 viewPortMousePosition = mainCamera.ScreenToViewportPoint(mousePosition);

            // Left
            if (viewPortMousePosition.x < cameraBorderThickness)
            {
                // To do : Camera Move to Left
                cameraPivot.Translate(cameraRight * cameraMoveSpeed * (-1) * Time.deltaTime, Space.World);

                // Left Top
                if (viewPortMousePosition.y > (1 - cameraBorderThickness))
                {
                    cameraPivot.Translate(cameraForward * cameraMoveSpeed * Time.deltaTime, Space.World);
                }
                // Left Bottom
                else if (viewPortMousePosition.y < cameraBorderThickness)
                {
                    cameraPivot.Translate(cameraForward * cameraMoveSpeed * (-1) * Time.deltaTime, Space.World);
                }

            }
            // Right
            else if (viewPortMousePosition.x > (1 - cameraBorderThickness))
            {
                // To do : Camera Move to Right
                cameraPivot.Translate(cameraRight * cameraMoveSpeed * Time.deltaTime, Space.World);

                // Right Top
                if (viewPortMousePosition.y > (1 - cameraBorderThickness))
                {
                    cameraPivot.Translate(cameraForward * cameraMoveSpeed * Time.deltaTime, Space.World);
                }
                // Right Bottom
                else if (viewPortMousePosition.y < cameraBorderThickness)
                {
                    cameraPivot.Translate(cameraForward * cameraMoveSpeed * (-1) * Time.deltaTime, Space.World);
                }
            }
            // Top
            else if (viewPortMousePosition.y > (1 - cameraBorderThickness))
            {
                // To do : Camera Move to Top
                cameraPivot.Translate(cameraForward * cameraMoveSpeed * Time.deltaTime, Space.World);

                if (viewPortMousePosition.x < cameraBorderThickness)
                {
                    cameraPivot.Translate(cameraRight * cameraMoveSpeed * (-1) * Time.deltaTime, Space.World);
                }
                else if (viewPortMousePosition.x > (1 - cameraBorderThickness))
                {
                    cameraPivot.Translate(cameraRight * cameraMoveSpeed * Time.deltaTime, Space.World);
                }
            }
            // Bottom
            else if (viewPortMousePosition.y < cameraBorderThickness)
            {
                // To do : Camera Move to Bottom
                cameraPivot.Translate(cameraForward * cameraMoveSpeed * (-1) * Time.deltaTime, Space.World);

                if (viewPortMousePosition.x < cameraBorderThickness)
                {
                    cameraPivot.Translate(cameraRight * cameraMoveSpeed * (-1) * Time.deltaTime, Space.World);
                }
                else if (viewPortMousePosition.x > (1 - cameraBorderThickness))
                {
                    cameraPivot.Translate(cameraRight * cameraMoveSpeed * Time.deltaTime, Space.World);
                }
            }

            // 카메라 이동 제한
            beginCampos = cameraPivot.position;
            beginCampos.x = Mathf.Clamp(cameraPivot.position.x, -verticalZone, verticalZone);
            beginCampos.z = Mathf.Clamp(cameraPivot.position.z, -horizontalZone, horizontalZone);
            cameraPivot.position = beginCampos;

        }
    }
}
