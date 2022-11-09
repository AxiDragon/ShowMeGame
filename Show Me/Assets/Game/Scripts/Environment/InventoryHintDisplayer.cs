using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gunbloem
{
    [RequireComponent(typeof(Collider))]
    public class InventoryHintDisplayer : MonoBehaviour
    {
        [SerializeField] Transform buttonParent;
        [SerializeField] float scaleInTime = 0.5f;
        [SerializeField] float displayAfterTime = 3f;
        public bool canDisplay = false;
        private float timer = 0f;
        Vector3 baseScale;
        Vector3 offset;

        void Awake()
        {
            baseScale = buttonParent.localScale;
            offset = transform.position - buttonParent.position;
        }

        void Update()
        {
            timer += canDisplay ? Time.deltaTime : -Time.deltaTime;
            timer = Mathf.Clamp(timer, 0f, scaleInTime + displayAfterTime);

            buttonParent.localScale = Vector3.Lerp(Vector3.zero, baseScale, GetLerpValue());
        }

        private void LateUpdate()
        {
            buttonParent.position = transform.position - offset;
        }

        private float GetLerpValue()
        {
            return 1 - Mathf.Pow(1 - Mathf.Max(timer - displayAfterTime, 0f), 4);
        }
    }
}