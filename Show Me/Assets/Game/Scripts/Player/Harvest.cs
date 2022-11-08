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
        private GunSeed plantedSeed;
        private GunPart part;
        private bool canHarvest;
        public bool inRange = false;


        public void Plant(GunSeed seed)
        {
            Vector3 pos = GetPlantingPosition();
            pos.y -= 0.5f;
            plantedSeed = Instantiate(seed, pos, Quaternion.identity);
            StartCoroutine(GrowSeed());
           
        }
        private IEnumerator GrowSeed()
        {
            float timer = 0f;

            while (timer < growTime)
            {
                timer += Time.deltaTime;
                plantedSeed.transform.Translate(Vector3.up * Time.deltaTime / 2f);
                yield return null;
            }
            if (timer >= growTime) 
            {
                canHarvest = true;
                part = plantedSeed.resultPart;
            }
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
            Vector3 dir = transform.position - position;
            dir.y = 0f;
            dir.Normalize();

            return position + dir * plantRange;
        }

        public GunPart HarvestGunSeed() 
        {
            if (canHarvest && inRange) 
            {
                Destroy(gameObject);
                return part;
            }

            return null;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, plantRange);
        }
    }
}