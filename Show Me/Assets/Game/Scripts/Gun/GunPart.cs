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
        public int power = 1;
        public int impact = 1;
        public int fireRate = 5;
        public int speed = 10;
        [HideInInspector] public Sprite sprite;
        public Attachment attachment;

        internal Sprite GetSprite()
        {
            sprite = attachment.GetComponent<Image>().sprite;
            return sprite;
        }
    }
}