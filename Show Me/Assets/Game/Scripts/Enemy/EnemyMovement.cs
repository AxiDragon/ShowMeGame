using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Gunbloem
{
    public class EnemyMovement : MonoBehaviour
    {
        NavMeshAgent agent;
        EnemyController controller;

        void Awake()
        {
            controller = GetComponent<EnemyController>();
            agent = GetComponent<NavMeshAgent>();
        }

        void Update()
        {
            agent.SetDestination(controller.target.transform.position);
        }
    }
}