using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gunbloem
{
    public class Harvest : MonoBehaviour
    {
        [SerializeField] private float plantRange = 5f;

        public void Plant(GunSeed seed)
        {
            Vector3 pos = GetPlantingPosition();
            GunSeed plantedSeed = Instantiate(seed, pos, Quaternion.identity);
        }

        private Vector3 GetPlantingPosition()
        {
            Vector3 plantingPosition = transform.position;

            foreach (Collider c in Physics.OverlapSphere(transform.position, plantRange))
            {
                if (c.TryGetComponent<ProtectionTarget>(out var _))
                {
                    plantingPosition = GetClosestPointOutOfRange(c.transform.position);
                }
            }

            return plantingPosition;
        }

        private Vector3 GetClosestPointOutOfRange(Vector3 position)
        {
            Vector3 dir = (transform.position - position).normalized;
            return position + dir * plantRange;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, plantRange);
        }
    }
}