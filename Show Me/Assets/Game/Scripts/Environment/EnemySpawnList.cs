using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gunbloem
{
    [CreateAssetMenu(fileName = "Enemies", menuName = "Gunbloem/Enemy Spawn List")]
    public partial class EnemySpawnList : ScriptableObject
    {
        public EnemySpawn[] enemies;
    }
}