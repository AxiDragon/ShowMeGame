using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gunbloem
{
    public class EnemyFighter : MonoBehaviour
    {
        [SerializeField] float attackCooldown = 1f;
        [SerializeField] AudioSource attackAudio;
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
                transform.LookAt(controller.target.transform.position);
                LaunchAttack();
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
        private void LaunchAttack()
        {
            //attackAudio.Play();
            attack.Attack(controller.target);
            timeSinceLastAttack = 0f;
        }
    }
}