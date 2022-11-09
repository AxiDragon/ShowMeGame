using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Gunbloem
{
    public class GunPart : MonoBehaviour
    {
        [Header("Unadjustable, just for info")]
        public int power;
        public int impact;
        public int fireRate;
        public int speed;
        [HideInInspector] public Sprite sprite;
        public Attachment attachment;

        internal Sprite GetSprite()
        {
            sprite = attachment.GetComponent<Image>().sprite;
            return sprite;
        }

        public GunPart Clone()
        {
            return MemberwiseClone() as GunPart;
        }
    }
}