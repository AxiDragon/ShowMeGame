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

        }

        private void OnMouseDown()
        {
            ModifyPlanet();
        }

        private void ModifyPlanet()
        {
            Planet closestPlanet = GetClosestPlanet();
            if (closestPlanet != null)
            {
                closestPlanet.sound.pitch += pitchOffset;
                Destroy(gameObject);
            }
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
    }
}