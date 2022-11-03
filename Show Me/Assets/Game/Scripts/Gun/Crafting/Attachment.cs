using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gunbloem
{
    public class Attachment : MonoBehaviour
    {
        [HideInInspector] public List<AttachmentPoint> aps = new List<AttachmentPoint>();
        [HideInInspector] public bool benched = false;

        void Awake()
        {
            aps = GetComponentsInChildren<AttachmentPoint>().ToList();
        }
        
        public void TrySnap()
        {
            print("Trying,,,");
            AttachmentPoint closestSnap = null;
            AttachmentPoint snappingPoint = null;
            float closestDistance = Mathf.Infinity;

            foreach(AttachmentPoint point in aps)
            {
                point.UnSnap();

                AttachmentPoint ap = point.GetClosestOpposite(out float dis);
                if (ap != null && dis < closestDistance)
                {
                    print("got one?");
                    closestSnap = point;
                    snappingPoint = ap;
                    closestDistance = dis;
                }
            }

            if (closestSnap != null)
                closestSnap.Snap(snappingPoint);
        }
    }
}