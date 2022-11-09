using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gunbloem
{
    [RequireComponent(typeof(Collider))]
    public class ButtonHintDisplayer : MonoBehaviour
    {
        [SerializeField] Transform buttonParent;
        [SerializeField] float scaleInTime = 0.5f;
        [SerializeField] float displayAfterTime = 3f;
        public bool canDisplay = false;
        bool inRange = false;
        private float timer = 0f;
        Vector3 baseScale;
        Vector3 offset;

        public bool useDistance = false;
        public float distance = 4f;
        private Transform player;

        void Awake()
        {
            baseScale = buttonParent.localScale;
            offset = transform.position - buttonParent.position;
            player = GameObject.FindWithTag("Player").transform;
        }

        void Update()
        {
            if (useDistance)
                inRange = Vector3.Distance(player.transform.position, transform.position) < distance;

            timer += inRange ? Time.deltaTime : -Time.deltaTime;
            timer = Mathf.Clamp(timer, 0f, scaleInTime + displayAfterTime);

            if (!canDisplay)
                timer = 0f;

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

        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Player"))
                inRange = true;
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
                inRange = false;
        }

        private void OnDrawGizmosSelected()
        {
            if (!useDistance)
                return;

            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, distance);
        }
    }
}