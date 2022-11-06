using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gunbloem
{
    public class Harvest : MonoBehaviour
    {
        [SerializeField] private float plantRange = 5f;
        [SerializeField] private GameObject potToPlant;

        [SerializeField] float growTime = 2f;

        [SerializeField] Transform stalk;

        Vector3 stalkStartPos;

        public void Plant(GunSeed seed)
        {
            Vector3 pos = GetPlantingPosition();
            GunSeed plantedSeed = Instantiate(seed, pos, Quaternion.identity);
            GameObject pot = Instantiate(potToPlant, pos, Quaternion.identity);

            stalk.position = pot.transform.position;
            stalkStartPos = stalk.position;

            StartCoroutine(GrowSeed());
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

        private IEnumerator GrowSeed()
        {
            float timer = 0f;

            while (timer < growTime)
            {
                timer += Time.deltaTime;
                stalk.Translate(Vector3.up * Time.deltaTime / 2f);
                yield return null;
            }

            stalk.position = stalkStartPos;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, plantRange);
        }
    }
}