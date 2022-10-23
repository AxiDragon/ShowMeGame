using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) 
    {
        Inventory playerInventory = other.GetComponent<Inventory>();
        if (playerInventory != null)
        {
            playerInventory.PickedUpCoin();
            gameObject.SetActive(false);
            print("Picked up a coin");
        }
    
    
    }
}
