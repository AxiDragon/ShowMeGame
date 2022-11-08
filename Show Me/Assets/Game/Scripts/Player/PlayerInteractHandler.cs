using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Gunbloem
{
    public class PlayerInteractHandler : MonoBehaviour
    {
        PlayerInventory inventory;
        Harvest playerHarvest;

        private void Awake()
        {
            inventory = GetComponent<PlayerInventory>();
            playerHarvest = GetComponent<Harvest>();
        }

        public void InteractZ(InputAction.CallbackContext inp)
        {
            //Code for when the player presses Z
            if (inp.performed)
            {
                inventory.InteractInventory();
            }
        }

        public void InteractX(InputAction.CallbackContext inp)
        {
            //Code for when the player presses X
            if (inp.performed)
            {
                if (!inventory.inventoryUI.activeInHierarchy)
                    playerHarvest.HarvestAction();
            }
        }
    }
}