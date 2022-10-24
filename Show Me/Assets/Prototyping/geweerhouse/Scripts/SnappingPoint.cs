using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace GeweerhousePrototype
{
    public class SnappingPoint : MonoBehaviour
    {
        public Side side;
        private Side oppSide;
        private List<SnappingPoint> snapSiblings = new List<SnappingPoint>();
        private Transform partRoot;
        private Vector3 offset;

        [SerializeField] private float snapDistance = 2f;
        [HideInInspector] public bool snapped = false;
        SnappingPoint snappedToPoint = null;

        public bool snappingEnabled = true;

        public enum Side
        {
            Left,
            Right,
            Top,
            Bottom
        }

        private void Start()
        {
            partRoot = transform.parent.parent;
            offset = partRoot.position - transform.position;
            snapSiblings = transform.parent.GetComponentsInChildren<SnappingPoint>().ToList();
            oppSide = GetOppositeSide();
        }

        private Side GetOppositeSide()
        {
            switch (side)
            {
                case Side.Left:
                    return Side.Right;
                case Side.Right:
                    return Side.Left;
                case Side.Top:
                    return Side.Bottom;
                case Side.Bottom:
                    return Side.Left;
                default:
                    throw new NotImplementedException("No Side Specified, somehow");
            }
        }

        private void Update()
        {
            if (!snappingEnabled)
                return;

            if (snapped)
            {
                partRoot.position = snappedToPoint.transform.position + offset;
            }

            foreach (SnappingPoint ap in FindObjectsOfType<SnappingPoint>())
            {
                if (ap.side != oppSide)
                    continue;
                if (snapSiblings.Contains(ap))
                    continue;

                float dis = (transform.position - ap.transform.position).magnitude;
                print(dis);

                if (dis < snapDistance)
                    Snap(ap);
            }
        }

        private void Snap(SnappingPoint ap)
        {
            snappedToPoint = ap;
            snapped = true;
            Debug.LogWarning("Snapped!");
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(transform.position, .2f);
            Gizmos.DrawWireSphere(transform.position, snapDistance);
        }
    }
}
