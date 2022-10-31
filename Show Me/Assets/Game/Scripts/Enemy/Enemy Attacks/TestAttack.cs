using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gunbloem
{
    public class TestAttack : MonoBehaviour, IAttack
    {
        public void Attack(Health target)
        {
            target.TakeDamage(1);
        }
    }
}