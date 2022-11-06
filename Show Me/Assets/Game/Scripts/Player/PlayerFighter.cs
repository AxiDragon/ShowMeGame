using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Gunbloem
{
    public class PlayerFighter : MonoBehaviour
    {
        [HideInInspector] private Gun gun;
        [SerializeField] private Transform gunHolder;
        private PlayerMovement movement;
        private bool canShoot = true;

        private void Awake()
        {
            movement = GetComponent<PlayerMovement>();
            gun = GetComponentInChildren<Gun>();
        }

        private void Update()
        {
            //dirty, but not sure how to do this in the input system atm
            if (Input.GetMouseButton(0))
            {
                if (canShoot)
                {
                    gun.Shoot();
                }
            }
        }

        public void Shoot(InputAction.CallbackContext c)
        {
            //print(c.ReadValue<int>());
            //if (canShoot)
            //{
            //    gun.Shoot();
            //}
        }

        public void UpdateGun(Gun newGun)
        {
            gun = newGun;
            movement.UpdateSpeed(gun.speed);
        }

        public void CanShoot(bool canShoot)
        {
            this.canShoot = canShoot;
        }
    }
}