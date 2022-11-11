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
        bool stunned = false;
        float speed;
        float angularSpeed;

        void Awake()
        {
            controller = GetComponent<EnemyController>();
            agent = GetComponent<NavMeshAgent>();
            speed = agent.speed;
            angularSpeed = agent.angularSpeed;
        }

        void Update()
        {
            if (stunned)
                return;

            if (controller.target)
                agent.SetDestination(controller.target.transform.position);
        }

        public void Stun(float time)
        {
            StartCoroutine(StunCoroutine(time));
        }

        private IEnumerator StunCoroutine(float time)
        {
            stunned = true;
            agent.speed = 0f;
            agent.angularSpeed = 0f;
            yield return new WaitForSeconds(time);

            for (int i = 0; i < 30; i++)
            {
                NavMeshHit hit;

                if (NavMesh.SamplePosition(transform.position, out hit, 150f, NavMesh.AllAreas))
                {
                    transform.position = hit.position;
                    break;
                }
            }

            stunned = false;
            agent.speed = speed;
            agent.angularSpeed = angularSpeed;
        }
    }
}