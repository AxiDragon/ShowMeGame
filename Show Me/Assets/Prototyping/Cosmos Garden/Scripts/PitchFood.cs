using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CosmosGarden
{
    public class PitchFood : Food
    {
        [SerializeField] private float pitchOffset = 0f;

        public override void Consumed(Planet consumer)
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
    }
}