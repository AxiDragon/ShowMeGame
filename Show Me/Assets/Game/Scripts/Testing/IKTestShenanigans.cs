using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKTestShenanigans : MonoBehaviour
{
    [SerializeField] Transform target;

    void Update()
    {
        if (Vector3.Distance(transform.position, target.position) > 5f)
            transform.position = target.position + (target.position - transform.position) * .8f;
    }
}
