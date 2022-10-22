using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Groenhuis
{
    public class CameraFollow : MonoBehaviour
    {
        Vector3 offset;
        Transform par;

        void Start()
        {
            par = transform.parent;
            offset = par.position - transform.position;
            transform.parent = null;
        }

        void LateUpdate()
        {
            transform.position = par.position - offset;
        }
    }
}