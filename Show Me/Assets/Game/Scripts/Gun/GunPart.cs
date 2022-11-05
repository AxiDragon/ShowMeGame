using System;
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
        [HideInInspector] public Sprite sprite;
        public Attachment attachment;

        internal Sprite GetSprite()
        {
            sprite = attachment.GetComponent<Image>().sprite;
            return sprite;
        }
    }
}