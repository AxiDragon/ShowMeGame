using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gunbloem
{
    public class GunSeed : MonoBehaviour
    {
        [Header("Max Stats")]
        public float minPower = 1f;
        public float minImpact = 1f;
        public float minFireRate = 5f;
        public float minSpeed = -0.1f;
        [Header("Min Stats")]
        public float maxPower = 1f;
        public float maxImpact = 1f;
        public float maxFireRate = 5f;
        public float maxSpeed = -0.1f;
        [Header("Other")]
        public Sprite sprite;
        [HideInInspector] public float plantRange = 5f;
    }
}