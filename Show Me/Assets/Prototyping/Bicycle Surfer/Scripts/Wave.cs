using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BicycleSurfer
{
    public class Wave : MonoBehaviour
    {
        [SerializeField] float speed = 50f;

        private void Start()
        {
            Destroy(gameObject, 20f);
        }

        private void FixedUpdate()
        {
            transform.Translate(transform.forward * speed * Time.deltaTime);
        }
    }
}