using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gunbloem
{
    public class FaceCamera : MonoBehaviour
    {
        private Transform cam;

        private void Awake()
        {
            cam = Camera.main.transform;
        }

        void LateUpdate()
        {
            transform.LookAt(cam.position);
        }
    }
}