using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gunbloem
{
    public class BreedBox : MonoBehaviour
    {
        public List<GunPart> breedingParts = new List<GunPart>();
        public GunSeed seed;
        public float breedingBonus = 1.3f; 
        [HideInInspector] public bool harvestable = false; 
        [SerializeField] private Transform displayTransform;
        private Action destroyAction;
        private Animator anim;

        private void Awake()
        {
            destroyAction = () => { Destroy(gameObject); };
            anim = GetComponent<Animator>();
        }

        public void SetBreedTime(float time)
        {
            //Regular animation time is 1
            anim.speed = 1f / time;
        }

        public GunSeed HarvestGunSeed(out List<GunPart> usedParts, out bool success)
        {
            success = false;

            usedParts = new List<GunPart>();
            usedParts.Add(breedingParts[0]);
            usedParts.Add(breedingParts[1]);

            if (harvestable)
            {
                success = true;
                harvestable = false;
                transform.LeanScale(Vector3.one / 1000f, .5f).setEaseInCubic().setOnComplete(destroyAction);
                return seed;
            }

            usedParts.Clear();

            return null;
        }

        public void Breed(List<GunPart> bp)
        {
            breedingParts = new List<GunPart>(bp);
            int randomPart = UnityEngine.Random.Range(0, breedingParts.Count);
            seed.resultPart = Instantiate(breedingParts[randomPart], Vector3.zero, Quaternion.identity);
            seed.collected = true;
            SetSeedStats();
        }

        private void SetSeedStats()
        {
            GunPart p0 = breedingParts[0];
            GunPart p1 = breedingParts[1];
            seed.minPower = (int)(Mathf.Min(p0.power, p1.power) * breedingBonus);
            seed.maxPower = (int)(Mathf.Max(p0.power, p1.power) * breedingBonus);
            seed.minImpact = (int)(Mathf.Min(p0.impact, p1.impact) * breedingBonus);
            seed.maxImpact = (int)(Mathf.Max(p0.impact, p1.impact) * breedingBonus);
            seed.minFireRate = (int)(Mathf.Min(p0.fireRate, p1.fireRate) * breedingBonus);
            seed.maxFireRate = (int)(Mathf.Max(p0.fireRate, p1.fireRate) * breedingBonus);
            seed.minSpeed = (int)(Mathf.Min(p0.speed, p1.speed) * breedingBonus);
            seed.maxSpeed = (int)(Mathf.Max(p0.speed, p1.speed) * breedingBonus);
        }

        //Animation trigger
        public void HarvestTrigger()
        {
            harvestable = true;
            PlayerParticleManager.PoofEffect(displayTransform.position);

            //Display harvestable seed
            GunSeed result = Instantiate(seed, displayTransform);
            result.GetComponent<Rigidbody>().isKinematic = true;
        }
    }
}