using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GeweerhousePrototype
{
    public class PartDrag : MonoBehaviour
    {
        [SerializeField] LayerMask planeMask;
        GunPart currentPart = null;

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                currentPart = GetPart();
            }

            if (Input.GetMouseButtonUp(0))
                currentPart = null;

            if (currentPart == null)
                return;

            Vector3 pos = GetPlanePoint();

            if (pos != Vector3.zero)
                currentPart.transform.position = pos;
        }

        private GunPart GetPart()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.TryGetComponent<GunPart>(out var part))
                    return part;
            }

            return null;
        }

        private Vector3 GetPlanePoint()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, planeMask))
                return hit.point;

            return Vector3.zero;
        }
    }
}
