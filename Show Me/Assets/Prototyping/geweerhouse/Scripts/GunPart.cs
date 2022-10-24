using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace GeweerhousePrototype
{
    public class GunPart : MonoBehaviour
    {
        List<SnappingPoint> snappingPoints = new List<SnappingPoint>();
        [SerializeField] Transform snapParent;
        public GunStats stats;

        void Start()
        {
            snappingPoints = snapParent.GetComponentsInChildren<SnappingPoint>().ToList();
        }

        public void SetSnapping(bool snap)
        {
            foreach (SnappingPoint snapping in snappingPoints)
            {
                snapping.snappingEnabled = snap;
            }
        }
    }
}

