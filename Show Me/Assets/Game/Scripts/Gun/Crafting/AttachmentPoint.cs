using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Gunbloem
{
    public class AttachmentPoint : MonoBehaviour
    {
        public Side side;
        private Side oppSide;
        public int id = 0;
        private Attachment attacher;
        private Image attachImage;
        private Vector2 offset;
        [HideInInspector] public AttachmentPoint snappedTo = null;
        [HideInInspector] public RectTransform rect;
        [HideInInspector] public bool snapped = false;
        private RectTransform par;

        [Header("Don't Change These!")]
        [SerializeField] private Sprite attachmentCircle;
        [SerializeField] private Sprite attachmentInset;

        private void Awake()
        {
            par = transform.parent.GetComponent<RectTransform>();
            rect = GetComponent<RectTransform>();
            attacher = GetComponentInParent<Attachment>();
            attachImage = GetComponent<Image>();
        }
        
        private void Start()
        {
            oppSide = GetOppositeSide();
            offset = par.position - rect.position;
            SetUpAttachImage();
            //test
            Color c = attachImage.color;
            attachImage.color = new Color(c.r, c.g, c.b, 1f);
        }

        private void SetUpAttachImage()
        {
            switch (side)
            {
                case Side.Left:
                    attachImage.sprite = attachmentCircle;
                    break;
                case Side.Right:
                    rect.localEulerAngles = Vector3.forward * 90f;
                    attachImage.sprite = attachmentInset;
                    break;
                case Side.Top:
                    attachImage.sprite = attachmentCircle;
                    break;
                case Side.Bottom:
                    attachImage.sprite = attachmentInset;
                    break;
                default:
                    throw new NotImplementedException("No side specified, somehow");
            }
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
            snappedTo = snapPoint;
            snapped = true;

            par.SetParent(snapPoint.transform, false);
            par.position = (Vector2)snapPoint.rect.position + offset;
            snapPoint.snappedTo = this;
            snapPoint.snapped = true;
        }

        public void UnSnap()
        {
            if (snapped)
            {
                snappedTo.snappedTo = null;
                snappedTo.snapped = false;

                snappedTo = null;
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
                    {
                        if (ap.snappedTo == null)
                            aps.Add(ap);
                    }
                }
            }

            AttachmentPoint closestAp = null;
            closestDistance = Mathf.Infinity;

            foreach (AttachmentPoint ap in aps)
            {
                float dis = Vector3.Distance(rect.position, ap.rect.position);
                if (dis < closestDistance)
                {
                    closestDistance = dis;
                    closestAp = ap;
                }
            }

            return closestAp;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(transform.position, 10f);
        }
    }
}