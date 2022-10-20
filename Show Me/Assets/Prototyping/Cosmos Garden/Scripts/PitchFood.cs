using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CosmosGarden
{
    public class PitchFood : MonoBehaviour
    {
        [SerializeField] private float pitchOffset = 0f;

        void Start()
        {

        }

        void Update()
        {
            //Planet closestPlanet = GetClosestPlanet(out float distance);
            //if (distance < consumeDistance && closestPlanet != null)
            //{
            //    closestPlanet.Pitch += pitchOffset;
            //    closestPlanet.transform.localScale
            //    Destroy(gameObject);
            //}
        }

        public void Consumed(Planet consumer)
        {
            consumer.Pitch += pitchOffset;
            consumer.transform.localScale *= 1.1f;
            Destroy(gameObject);
        }

        private Planet GetClosestPlanet()
        {
            float dis = Mathf.Infinity;
            Planet cl = null;
            foreach (Planet p in FindObjectsOfType<Planet>())
            {
                float d = (transform.position - p.transform.position).magnitude;
                if (d < dis)
                {
                    dis = d;
                    cl = p;
                }
            }

            return cl;
        }

        private Planet GetClosestPlanet(out float distance)
        {
            distance = Mathf.Infinity;
            Planet cl = null;
            foreach (Planet p in FindObjectsOfType<Planet>())
            {
                float d = (transform.position - p.transform.position).magnitude;
                if (d < distance)
                {
                    distance = d;
                    cl = p;
                }
            }

            return cl;
        }

        private void OnDrawGizmos()
        {
            //Gizmos.color = Color.yellow;
            //Gizmos.DrawWireSphere(transform.position, consumeDistance);
        }
    }
}