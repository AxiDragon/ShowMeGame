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
        [SerializeField] private float collectRange = 2f;
        private PlayerInventory inv;

        private void Awake()
        {
            inv = GetComponent<PlayerInventory>();    
        }

        public void Plant(GunSeed seed)
        {
            Vector3 pos = GetPlantingPosition();
            pos.y -= 0.5f;
            seed.PlantSeed(pos);
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

        public void HarvestAction()
        {
            foreach (Collider coll in Physics.OverlapSphere(transform.position, collectRange))
            {
                if (coll.TryGetComponent<GunPlant>(out var p))
                {
                    GunPart gp = p.HarvestGunPart();
                    if (gp != null)
                        inv.parts.Add(gp);
                }

                if (coll.TryGetComponent<GunSeed>(out var s))
                {
                    if (!s.collected)
                    {
                        inv.seeds.Add(s);
                        s.Collect();
                    }
                }
            }
        }

        private Vector3 GetClosestPointOutOfRange(Vector3 position)
        {
            Vector3 dir = transform.position - position;
            dir.y = 0f;
            dir.Normalize();

            return position + dir * plantRange;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, plantRange);
        }
    }
}