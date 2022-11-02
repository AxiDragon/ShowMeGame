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
        [SerializeField] Transform cameraPivot;
        [HideInInspector] public float scrollTarget;
        [SerializeField] float zoomAdjustSpeed = 5f;
        //[SerializeField] float rotateAdjustSpeed = 5f;
        [SerializeField] float minZoom = 1f;
        [SerializeField] float maxZoom = 35f;
        [SerializeField] float topClamp = -90f;
        [SerializeField] float bottomClamp = 15f;
        Transform cameraTarget;
        float xRotationTarget = 0f;
        //float yRotationTarget = 0f;

        void Awake()
        {
            cameraTarget = cameraPivot.GetChild(0);
            scrollTarget = cameraTarget.localPosition.z;
            Cursor.lockState = CursorLockMode.Locked;
        }

        public void RotateCamera(InputAction.CallbackContext callback)
        {
            Vector2 rotation = callback.ReadValue<Vector2>() * (rotateSensitivity / 1000f);

            xRotationTarget -= rotation.y;
            xRotationTarget = Mathf.Clamp(xRotationTarget, bottomClamp, topClamp);

            transform.Rotate(Vector3.up * rotation.x);
            cameraPivot.localRotation = Quaternion.Euler(xRotationTarget, 0f, 0f);
        }

        public void UpdateOffset(InputAction.CallbackContext callback)
        {
            float value = callback.ReadValue<float>();
            scrollTarget += value * zoomSensitivity / 1000f;
            scrollTarget = Mathf.Clamp(scrollTarget, -maxZoom, -minZoom);
        }

        private void FixedUpdate()
        {
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
    }
}