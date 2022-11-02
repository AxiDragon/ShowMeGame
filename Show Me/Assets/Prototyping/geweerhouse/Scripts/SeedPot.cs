using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GeweerhousePrototype
{
    public class SeedPot : MonoBehaviour
    {
        public BulletSeed plantedSeed = null;
        public GunPart finishedPart = null;
        [SerializeField] Transform stalk;
        [SerializeField] GameObject toolTip;
        GunPart gunFruit = null;
        Vector3 stalkStartPos;
        [SerializeField] float growTime = 2f;

        void Start()
        {
            stalkStartPos = stalk.position;
        }

        
        public void PlantSeed(BulletSeed seed)
        {
            plantedSeed = seed;
            StartCoroutine(GrowSeed());
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
            finishedPart = plantedSeed.resultPart;
            gunFruit = Instantiate(finishedPart, transform.position + Vector3.up, Quaternion.identity);
            gunFruit.transform.localScale /= 2f;
            gunFruit.SetSnapping(false);
        }

        public GunPart CollectPart()
        {
            StartCoroutine(DeletePart());
            return finishedPart;
        }

        private IEnumerator DeletePart()
        {
            yield return null;
            finishedPart = null;
            plantedSeed = null;
            Destroy(gunFruit.gameObject);
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.GetComponent<Harvest>())
            {
                Debug.Log("in range");
                Harvest.inRangeOfPlant = true;
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.GetComponent<Harvest>())
            {
                Harvest.inRangeOfPlant = false;
            }
        }
    }
}