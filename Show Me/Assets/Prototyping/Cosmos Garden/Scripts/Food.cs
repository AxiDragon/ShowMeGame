using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CosmosGarden
{
    public class Food : MonoBehaviour
    {
        public virtual void Consumed(Planet consumer)
        {
            print("omnomnom");
        }
    }
}