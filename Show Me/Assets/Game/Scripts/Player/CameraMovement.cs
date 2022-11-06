using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Gunbloem
{
    public class CameraMovement : MonoBehaviour
    {
        [SerializeField] float rotateSensitivity = 100f;
        [SerializeField] float zoomSensitivity = 100f;
        [SerializeField] Transform cameraXPivot;
        [SerializeField] Transform cameraYPivot;
        [HideInInspector] public float scrollTarget;
        [SerializeField] float zoomAdjustSpeed = 5f;
        //[SerializeField] float rotateAdjustSpeed = 5f;
        [SerializeField] float minZoom = 1f;
        [SerializeField] float maxZoom = 35f;
        [SerializeField] float topClamp = -90f;
        [SerializeField] float bottomClamp = 15f;
        [HideInInspector] public Transform cameraTarget;
        float xRotationTarget = 0f;
        private bool controllable = true;
        Vector3 offset;

        //float yRotationTarget = 0f;

        void Awake()
        {
            cameraTarget = cameraXPivot.GetChild(0);
            scrollTarget = cameraTarget.localPosition.z;
            Cursor.lockState = CursorLockMode.Locked;
            offset = cameraYPivot.position - transform.position;
            cameraYPivot.parent = null;
        }

        public void RotateCamera(InputAction.CallbackContext callback)
        {
            if (!controllable)
                return; 
            Vector2 rotation = callback.ReadValue<Vector2>() * (rotateSensitivity / 1000f);

            xRotationTarget -= rotation.y;
            xRotationTarget = Mathf.Clamp(xRotationTarget, bottomClamp, topClamp);

            cameraYPivot.Rotate(Vector3.up * rotation.x);
            cameraXPivot.localRotation = Quaternion.Euler(xRotationTarget, 0f, 0f);
        }

        public void UpdateOffset(InputAction.CallbackContext callback)
        {
            if (!controllable)
                return; 
            
            float value = callback.ReadValue<float>();
            scrollTarget += value * zoomSensitivity / 1000f;
            scrollTarget = Mathf.Clamp(scrollTarget, -maxZoom, -minZoom);
        }

        private void FixedUpdate()
        {
            if (!controllable)
                return;

            float scrollDiff = scrollTarget - cameraTarget.localPosition.z;
            cameraTarget.localPosition += new Vector3(0f, 0f, scrollDiff * zoomAdjustSpeed / 100f);

            //float xDiff = Mathf.DeltaAngle(xRotationTarget, cameraPivot.localEulerAngles.x) / 360f;
            //print(xRotationTarget + "    " + cameraPivot.localEulerAngles.x);
            //print(xDiff);
            //Vector3 pRot = cameraPivot.localEulerAngles;
            //print(pRot);
            //cameraPivot.localRotation = Quaternion.Euler((xDiff * zoomAdjustSpeed / 100f) + pRot.x, 0f, 0f);
            //print(Quaternion.Euler((xDiff * rotateAdjustSpeed / 100f) + pRot.x, 0f, 0f));
        }

        private void LateUpdate()
        {
            cameraYPivot.position = transform.position + offset;
        }

        public void SetEnabled(bool enabled)
        {
            controllable = enabled;
        }
    }
}