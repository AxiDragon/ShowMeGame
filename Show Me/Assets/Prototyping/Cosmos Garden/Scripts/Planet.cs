using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CosmosGarden
{
    public class Planet : MonoBehaviour
    {
        [HideInInspector] public AudioSource sound;
        private Renderer rend;
        [SerializeField] Material lowPitch;
        [SerializeField] Material highPitch;
        [SerializeField] private float maxPitch;
        [SerializeField] private float minPitch;
        private float pitch;

        public float Pitch { get => pitch; set
            {
                pitch = Mathf.Clamp(value, minPitch, maxPitch);
                UpdatePitch();
            }
        }

        void Start()
        {
            sound = GetComponent<AudioSource>();
            rend = GetComponent<Renderer>();
            Pitch = sound.pitch;
        }

        void UpdatePitch()
        {
            sound.pitch = Pitch;
            rend.material.Lerp(lowPitch, highPitch, GetPitchFraction());
        }

        private float GetPitchFraction()
        {
            float m = maxPitch - minPitch;
            float p = pitch - minPitch;
            return (p / m);
        }

        void DecreasePitch()
        {
            Pitch -= .2f;
        }

        void IncreasePitch()
        {
            Pitch += .2f;
        }

        private void OnMouseDown()
        {
            sound.Play();
        }

        private void OnGUI()
        {
            if (GUI.Button(new Rect(25,0,150,50), "Increase Pitch"))
            {
                IncreasePitch();
            }
            if (GUI.Button(new Rect(25, 100, 150, 50), "Decrease Pitch"))
            {
                DecreasePitch();
            }
        }
    }
}