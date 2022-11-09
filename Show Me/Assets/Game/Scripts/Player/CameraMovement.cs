using System;
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
        [SerializeField] float minZoom = 1f;
        [SerializeField] float maxZoom = 35f;
        [SerializeField] float topClamp = -90f;
        [SerializeField] float bottomClamp = 15f;
        [HideInInspector] public Transform cameraTarget;
        float xRotationTarget = 0f;
        private bool controllable = true;
        private bool colliding = false;
        Vector3 offset;

        LayerMask mask;

        void Awake()
        {
            cameraTarget = cameraXPivot.GetChild(0);
            scrollTarget = cameraTarget.localPosition.z;
            Cursor.lockState = CursorLockMode.Locked;
            offset = cameraYPivot.position - transform.position;
            cameraYPivot.parent = null;
            mask = LayerMask.GetMask("Terrain");
        }

        public void RotateCamera(InputAction.CallbackContext callback)
        {
            if (!controllable)
                return; 

            Vector2 rotation = callback.ReadValue<Vector2>() * (rotateSensitivity / 10f) * Time.deltaTime;

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

            CheckCollision();

            if (colliding)
                return;

            float scrollDiff = scrollTarget - cameraTarget.localPosition.z;
            cameraTarget.localPosition += new Vector3(0f, 0f, scrollDiff * zoomAdjustSpeed / 100f);
        }

        private void CheckCollision()
        {
            Vector3 coll = GetCollisionPoint(out bool c);
            colliding = c;

            if (colliding)
                cameraTarget.transform.position = coll;
        }

        public Vector3 GetCollisionPoint(out bool collHit)
        {
            collHit = false;

            RaycastHit hit;
            Ray ray = new Ray(cameraXPivot.position, -cameraXPivot.forward);

            Debug.DrawRay(ray.origin, ray.direction * scrollTarget, Color.red);

            if (Physics.Raycast(ray, out hit, Mathf.Abs(scrollTarget), mask))
            {
                collHit = true;

                return ray.origin + ray.direction * (hit.distance - .3f);
            }

            return Vector3.zero;
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