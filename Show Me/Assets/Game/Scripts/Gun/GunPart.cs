using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Gunbloem
{
    public class GunPart : MonoBehaviour
    {
        [HideInInspector] public float power = 1;
        [HideInInspector] public float impact = 1;
        [HideInInspector] public float fireRate = 5f;
        [HideInInspector] public float speed = -0.1f;
        [HideInInspector] public Sprite sprite;
        public Attachment attachment;

        internal Sprite GetSprite()
        {
            sprite = attachment.GetComponent<Image>().sprite;
            return sprite;
        }
    }
}