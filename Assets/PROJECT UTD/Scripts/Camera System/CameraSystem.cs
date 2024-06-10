using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SCU
{
    public class CameraSystem : MonoBehaviour
    {
        public static CameraSystem Instance { get; private set; } = null;

        // public bool IsTpsMode { get { return isTPSMode; } }
        public bool IsTPSMode => isTPSMode;

        public float TargerFOV { get; set; } = 60.0f;

        public Cinemachine.CinemachineVirtualCamera tpsCamera;
        public Cinemachine.CinemachineVirtualCamera fpsCamera;

        public float zoomSpeed = 5.0f;

        private bool isTPSMode = true;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            isTPSMode = tpsCamera.gameObject.activeSelf;
        }

        private void OnDestroy()
        {
            Instance = null;
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.T))
            {
                isTPSMode = !isTPSMode;

                tpsCamera.gameObject.SetActive(isTPSMode);
                fpsCamera.gameObject.SetActive(!isTPSMode);
            }
        }

        private void LateUpdate()
        {
            tpsCamera.m_Lens.FieldOfView = Mathf.Lerp(tpsCamera.m_Lens.FieldOfView, TargerFOV, zoomSpeed * Time.deltaTime);
            fpsCamera.transform.forward = fpsCamera.Follow.transform.forward;
        }
    }
}
