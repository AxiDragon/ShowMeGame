using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace Gunbloem
{
    [RequireComponent(typeof(TwoBoneIKConstraint))]
    public class ProceduralIK : MonoBehaviour
    {
        TwoBoneIKConstraint constraint;
        Transform target;
        Transform bone;
        Vector3 targetPos;
        [SerializeField] private float moveDistance = 1f;
        [SerializeField] bool keepOnY = true;

        void Awake()
        {
            constraint = GetComponent<TwoBoneIKConstraint>();
            target = constraint.data.target;
            bone = constraint.data.tip;
            UpdateTargetPosition();
        }

        void Update()
        {
            float targetDistance = Vector3.Distance(target.position, targetPos);
            target.position = Vector3.MoveTowards(target.position, targetPos, targetDistance * Time.deltaTime * 20f);

            float boneDistance = Vector3.Distance(target.position, bone.position);
            if (boneDistance > moveDistance)
                UpdateTargetPosition();
        }

        private void UpdateTargetPosition()
        {
            Vector3 dir = bone.position - target.position;
            Vector3 tarPos = bone.position + dir * 1.8f;

            if (keepOnY)
            {
                tarPos.y = transform.position.y;
            }

            targetPos = tarPos;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(GetComponent<TwoBoneIKConstraint>().data.tip.position, moveDistance);
            Gizmos.DrawSphere(GetComponent<TwoBoneIKConstraint>().data.target.position, .1f);
        }
    }
}