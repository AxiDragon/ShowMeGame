using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gunbloem
{
    public class EnemyHealth : Health
    {
        EnemyDrops drops;

        private void Awake()
        {
            drops = GetComponent<EnemyDrops>();
        }

        public override void Die()
        {
            base.Die();
            drops.DropItem();
            //Death Animation
            Destroy(gameObject);
        }

        //Triggered by Animation
        public void DieTrigger()
        {
            Destroy(gameObject);
        }
    }
}