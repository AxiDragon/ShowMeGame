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
            ScoreKeeper.IncreaseScore((int)(maxHealth / 10f));
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