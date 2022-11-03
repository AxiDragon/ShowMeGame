using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Gunbloem
{
    public class GunPart : MonoBehaviour
    {
        [Header("Stats")]
        public float power = 1;
        public float impact = 1;
        public float fireRate = 5f;
        public float speed = -0.1f;
        [Header("Other")]
        public Sprite sprite;
    }
}