using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gunbloem
{
    public class ModelAttachmentPoint : MonoBehaviour
    {
        public Side side;
        public int id = 0;
        [HideInInspector] public Vector3 offset;

        private void Awake()
        {
            offset = transform.parent.position - transform.position;
        }

        public void Snap(ModelAttachmentPoint snapPoint)
        {
            transform.parent.parent = snapPoint.transform;
            transform.parent.localPosition = snapPoint.transform.position + offset;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(transform.position, .01f);
        }
    }
}