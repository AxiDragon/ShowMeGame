using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CosmosGarden
{
    public class SolarSystem : MonoBehaviour
    {
        List<Planet> planets = new List<Planet>();
        [SerializeField] private float rotationSpeed = .2f;

        void Start()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                planets.Add(transform.GetChild(i).GetComponent<Planet>());
            }
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartCoroutine(PlaySystem());
            }
        }

        private void FixedUpdate()
        {
            transform.rotation = Quaternion.Euler(transform.eulerAngles + Vector3.up * rotationSpeed);
        }

        private IEnumerator PlaySystem()
        {
            for (int i = 0; i < planets.Count; i++)
            {
                planets[i].sound.Play();
                yield return new WaitForSecondsRealtime(1f);
            }
        }
    }
}