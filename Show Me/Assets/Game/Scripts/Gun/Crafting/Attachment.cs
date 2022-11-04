using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gunbloem
{
    public class Attachment : MonoBehaviour
    {
        [HideInInspector] public List<AttachmentPoint> aps = new List<AttachmentPoint>();
        [HideInInspector] public Transform bench;
        [HideInInspector] public RectTransform rect;
        [HideInInspector] public bool benched = false;

        void Awake()
        {
            aps = GetComponentsInChildren<AttachmentPoint>().ToList();
            bench = FindObjectOfType<Workbench>().transform;
            rect = GetComponent<RectTransform>();
        }

        public void TrySnap()
        {
            AttachmentPoint closestSnap = null;
            AttachmentPoint snapToPoint = null;
            float closestDistance = Mathf.Infinity;

            foreach (AttachmentPoint point in aps)
            {
                UnSnap();

                AttachmentPoint ap = point.GetClosestOpposite(out float dis);
                if (dis < closestDistance && ap != null)
                {
                    closestSnap = ap;
                    snapToPoint = point;
                    closestDistance = dis;
                }
            }

            if (closestSnap != null)
            {
                snapToPoint.Snap(closestSnap);
            }
        }

        public void UnSnap()
        {
            foreach (AttachmentPoint point in aps)
            {
                point.UnSnap();
            }

            Vector2 pos = rect.position;
            transform.SetParent(bench.transform, false);
            rect.position = pos;
        }
    }
}