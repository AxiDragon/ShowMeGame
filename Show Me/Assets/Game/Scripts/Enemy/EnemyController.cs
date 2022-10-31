using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace Gunbloem
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] bool ranged = false;
        [HideInInspector] public Health target;
        Health player;

        private void Awake()
        {
            player = GameObject.FindGameObjectWithTag("Player").transform.GetComponent<Health>();
        }

        void Start()
        {
            target = GetClosestTarget();
        }

        void Update()
        {
            target = GetClosestTarget();
        }

        private Health GetClosestTarget()
        {
            if (ranged)
                return player;

            GameObject target = null;

            float distance = Mathf.Infinity;

            List<GameObject> targets = GameObject.FindGameObjectsWithTag("Protection Target").ToList();
            targets.Add(player.gameObject);

            foreach (GameObject t in targets)
            {
                float dis = Vector3.Distance(transform.position, t.transform.position);
                if (dis < distance)
                {
                    distance = dis;
                    target = t;
                }
            }

            return target.GetComponent<Health>();
        }
    }
}