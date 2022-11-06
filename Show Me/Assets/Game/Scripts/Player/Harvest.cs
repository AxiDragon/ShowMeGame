using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gunbloem
{
    public class Harvest : MonoBehaviour
    {
        [SerializeField] private float plantRange;
        [SerializeField] private GameObject potToPlant;

        [SerializeField] float growTime = 2f;

        [SerializeField] Transform stalk;

        Vector3 stalkStartPos;

        public void Plant(GunSeed seed)
        {
            Vector3 pos = GetPlantingPosition();
            pos.y -= 0.5f;


            Debug.Log(pos);
            GunSeed plantedSeed = Instantiate(seed, pos, Quaternion.identity);
            /*GameObject pot = Instantiate(potToPlant, pos, potToPlant.transform.rotation);

            stalk.position = pot.transform.position;
            stalkStartPos = stalk.position;*/

        }

        private Vector3 GetPlantingPosition()
        {
            Debug.Log(transform.position);
            Vector3 plantingPosition = this.transform.position;

            /*foreach (Collider c in Physics.OverlapSphere(transform.position, plantRange))
            {
                if (c.TryGetComponent<ProtectionTarget>(out var _))
                {
                    plantingPosition = GetClosestPointOutOfRange(c.transform.position);
                }
            }*/
            return plantingPosition;
        }

        private Vector3 GetClosestPointOutOfRange(Vector3 position)
        {
            Vector3 dir = (transform.position - position);
            return position + dir * plantRange;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, plantRange);
        }
    }
}