using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Gunbloem
{
    public class Harvest : MonoBehaviour
    {
        [SerializeField] private float plantRange;
        [SerializeField] private BreedBox breedBox;
        [SerializeField] private float collectRange = 2f;
        private PlayerInventory inv;
        [HideInInspector] public List<GunPart> breedList;

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

        public void SelectBreed(GunPart part)
        {
            breedList.Add(part);
            
            if (breedList.Count == 2)
            {
                Breed(breedList);
                inv.parts.Remove(breedList[0]);
                inv.parts.Remove(breedList[1]);
                breedList.Clear();
                inv.InteractInventory();
            }
        }

        public void DeselectBreed(GunPart part)
        {
            breedList.Remove(part);
        }

        public void Breed(List<GunPart> parts)
        {
            GunPart p0 = parts[0];
            GunPart p1 = parts[1];

            float time = Mathf.Pow((p0.power + p1.power + p0.fireRate + p1.fireRate + p0.impact + p1.impact) / 3f, .85f);

            Quaternion randomRot = Quaternion.Euler(0f, Random.Range(0f, 360f), 0f);
            BreedBox box = Instantiate(breedBox, GetPlantingPosition() - Vector3.up * .5f, randomRot);
            box.SetBreedTime(time);
            box.Breed(parts);
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
                        inv.AddSeed(s);
                        s.Collect();
                    }
                }

                if (coll.TryGetComponent<BreedBox>(out var b))
                {
                    GunSeed seed = b.HarvestGunSeed(out var g, out var o);
                    if (o)
                    {
                        inv.seeds.Add(seed);
                        inv.parts.Add(g[0]);
                        inv.parts.Add(g[1]);
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
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, collectRange);
        }
    }
}