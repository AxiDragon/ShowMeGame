using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CosmosGarden
{
    public class SoundFood : Food
    {
        [SerializeField] AudioClip clip;
        [SerializeField] private float volume;

        public override void Consumed(Planet consumer)
        {
            consumer.sound.clip = clip;
            consumer.sound.volume = volume;
            Destroy(gameObject);
        }
    }
}