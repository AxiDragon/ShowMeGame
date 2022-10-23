using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int numberOfCoins { get; private set;}

    public void PickedUpCoin()
    {
        numberOfCoins++;
    }
}
