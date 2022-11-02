using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Gunbloem
{
    public class PlayerInventory : MonoBehaviour
    {
        [SerializeField] public List<GunSeed> seeds = new List<GunSeed>();
        [SerializeField] public List<GunPart> parts = new List<GunPart>();
        [SerializeField] private GameObject inventoryUI;
        [SerializeField] private UnityEvent inventoryOpen;
        [SerializeField] private UnityEvent inventoryClosed;

        public void InteractInventory()
        {
            inventoryUI.SetActive(!inventoryUI.activeInHierarchy);
            Time.timeScale = inventoryUI.activeInHierarchy ? 0.0f : 1.0f;
            Cursor.lockState = inventoryUI.activeInHierarchy ? CursorLockMode.None : CursorLockMode.Locked;

            if (inventoryUI.activeInHierarchy)
            {
                print("Open!");
                inventoryOpen?.Invoke();
            }
            else
            {
                print("Closed!");
                inventoryClosed?.Invoke();
            }
        }
    }
}