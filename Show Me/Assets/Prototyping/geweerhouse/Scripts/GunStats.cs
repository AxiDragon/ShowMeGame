using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GeweerhousePrototype
{
    [CreateAssetMenu(fileName = "Gun Stats", menuName = "GeweerhouseProto/Gun Stats")]
    public class GunStats : ScriptableObject
    {
        public float fireRate = 1f;
        public float explosionRadius = 1f;
        public float explosionForce = 5f;
        public float firingSpeed = 20f;
        public float recoil = 1f;

        public Functioning function = Functioning.Manual;

        public float emotionalStability = 1f;
    }
}