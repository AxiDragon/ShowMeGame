using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace Gunbloem
{
    [RequireComponent(typeof(TwoBoneIKConstraint))]
    public class IKGunAim : MonoBehaviour
    {
        private TwoBoneIKConstraint constraint;
        [HideInInspector] public Gun gun;

        private void Awake()
        {
            constraint = GetComponent<TwoBoneIKConstraint>();
        }

        void Update()
        {
            if (gun)
                constraint.data.target.position = gun.GetShootTarget();
        }
    }
}