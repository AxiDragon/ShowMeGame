using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Gunbloem
{
    public class PlayerInteractHandler : MonoBehaviour
    {
        PlayerInventory inventory;
        GunShoot playerGun;
        Harvest playerHarvest;

        private void Awake()
        {
            inventory = GetComponent<PlayerInventory>();
            playerGun = GetComponent<GunShoot>();   
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
                //Check if near harvestable PT (In another script)
            }
        }

        public void PlayerShoot(InputAction.CallbackContext inp)
        {
            if (inp.performed) 
            {
                playerGun.Shoot();
            
            }
        }
    }
}