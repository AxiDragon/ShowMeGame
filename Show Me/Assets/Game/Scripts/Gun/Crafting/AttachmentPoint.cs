using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gunbloem
{
    public class AttachmentPoint : MonoBehaviour
    {
        public Side side;
        private Side oppSide;
        private Attachment attacher;
        private Vector3 offset;
        [HideInInspector] public RectTransform rect;
        [HideInInspector] public AttachmentPoint snappedTo = null;
        public RectTransform snappedToRect = null;
        [HideInInspector] public bool snapped = false;
        private RectTransform parentRect;

        private void Awake()
        {
            rect = GetComponent<RectTransform>();
            parentRect = GetComponentInParent<RectTransform>();
            offset = rect.position - parentRect.position;

            attacher = GetComponentInParent<Attachment>();
        }

        private void Start()
        {
            oppSide = GetOppositeSide();
        }

        private void Update()
        {
            if (!snapped)
                return;

            parentRect.position = snappedToRect.position + offset;
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
                    return Side.Top;
                default:
                    throw new NotImplementedException("No side specified, somehow");
            }
        }

        public void Snap(AttachmentPoint snapPoint)
        {
            print("snapping so hard right now");
            snappedTo = snapPoint;
            snappedToRect = snapPoint.rect;
            snapped = true;

            snapPoint.snappedTo = this;
            snapPoint.snappedToRect = rect;
            snapPoint.snapped = true;
        }

        public void UnSnap()
        {
            if (snapped)
            {
                snappedTo.snappedTo = null;
                snappedTo.snappedToRect = null;
                snappedTo.snapped = false;

                snappedTo = null;
                snappedToRect = null;
                snapped = false;
            }
        }

        public AttachmentPoint GetClosestOpposite(out float closestDistance)
        {
            List<AttachmentPoint> aps = new List<AttachmentPoint>();

            foreach (Attachment a in FindObjectsOfType<Attachment>())
            {
                if (!a.benched || a == attacher)
                    continue;

                foreach (AttachmentPoint ap in a.aps)
                {
                    if (ap.side == oppSide)
                        aps.Add(ap);
                }
            }

            AttachmentPoint closestAp = null;
            closestDistance = Mathf.Infinity;

            foreach (AttachmentPoint ap in aps)
            {
                float dis = Vector2.Distance(rect.position, ap.rect.position);
                if (dis < closestDistance)
                {
                    closestDistance = dis;
                    closestAp = ap;
                }
            }

            return closestAp;
        }
    }
}