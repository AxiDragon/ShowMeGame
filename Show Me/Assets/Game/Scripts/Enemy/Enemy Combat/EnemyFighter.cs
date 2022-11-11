using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Gunbloem
{
    public class EnemyFighter : MonoBehaviour
    {
        [SerializeField] float attackCooldown = 1f;
        [SerializeField] AudioSource attackAudio;
        private bool canPlaySound = true;
        public float range = 5f;
        Animator animator;
        float timeSinceLastAttack = Mathf.Infinity;
        IAttack attack;
        EnemyController controller;

        private void Awake()
        {
            controller = GetComponent<EnemyController>();
            animator = GetComponent<Animator>();
            attack = GetComponent<IAttack>();
        }

        void Update()
        {
            if (timeSinceLastAttack > attackCooldown && CheckIsInRange())
            {
                animator.SetTrigger("Attack");
                timeSinceLastAttack = 0f;
                if (canPlaySound)
                {
                    attackAudio.pitch = Random.Range(2f, 2.5f);
                    attackAudio.Play();
                    canPlaySound = false;
                }
                transform.LookAt(controller.target.transform.position);
            }

            UpdateTimers();
        }

        private void UpdateTimers()
        {
            timeSinceLastAttack += Time.deltaTime;
        }

        private bool CheckIsInRange()
        {
            float distance = Vector3.Distance(controller.target.transform.position, transform.position);

            if (distance > range)
                return false;
            else
                return true;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, range);
        }

        //Animation Event
        private void AttackTrigger()
        {
            canPlaySound = true;
            attack.Attack(controller.target);
            timeSinceLastAttack = 0f;
        }
    }
}