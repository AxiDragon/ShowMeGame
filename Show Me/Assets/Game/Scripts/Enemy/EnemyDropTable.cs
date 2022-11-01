using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gunbloem
{
    [CreateAssetMenu(fileName = "Enemy Drop Table", menuName = "Gunbloem/Enemy Drop Table")]
    public partial class EnemyDropTable : ScriptableObject
    {
        public float overallDropChance = .2f;
        public DropList[] drops;
    }
}