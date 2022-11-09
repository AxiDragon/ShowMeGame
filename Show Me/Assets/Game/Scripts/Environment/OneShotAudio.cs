using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gunbloem
{
    public class OneShotAudio : MonoBehaviour
    {
        void Start()
        {
            AudioSource audioSource = GetComponent<AudioSource>();
            Destroy(gameObject, audioSource.clip.length);
        }
    }
}