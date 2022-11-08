using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Gunbloem
{
    public class PlayerInventory : MonoBehaviour
    {
        [Header("Testing")]
        [SerializeField] public List<GunSeed> seeds = new List<GunSeed>();
        [SerializeField] public List<GunPart> parts = new List<GunPart>();
        [Header("UI GameObjects")]
        [SerializeField] private GameObject inventoryUI;
        [SerializeField] private GameObject plantMenuContent;
        [SerializeField] private GameObject breedMenuContent;
        [SerializeField] private GameObject craftMenuContent;
        [Header("Item Prefabs")]
        [SerializeField] private SeedItemUI seedUI;
        [SerializeField] private PartItemUI partUI;
        [Header("Events")]
        [SerializeField] private UnityEvent inventoryOpen;
        [SerializeField] private UnityEvent inventoryClosed;

        public void InteractInventory()
        {
            inventoryUI.SetActive(!inventoryUI.activeInHierarchy);
            Time.timeScale = inventoryUI.activeInHierarchy ? 0.0f : 1.0f;
            Cursor.lockState = inventoryUI.activeInHierarchy ? CursorLockMode.None : CursorLockMode.Locked;

            if (inventoryUI.activeInHierarchy)
            {
                //if possible, gradual time slowdown and inventory opening for sparkle
                inventoryOpen?.Invoke();
            }
            else
            {
                inventoryClosed?.Invoke();
            }
        }

        public void GenerateInventory()
        {
            GenerateMenuContent(plantMenuContent, seeds);
            GenerateMenuContent(breedMenuContent, parts);
            GenerateCraftMenuContent(craftMenuContent, parts);
        }

        private void GenerateCraftMenuContent(GameObject menuContent, List<GunPart> parts)
        {
            ClearChildren(menuContent);
            List<PartItemUI> content = GenerateCraftContent(parts);
            PlaceContent(menuContent, content);
        }

        private void GenerateMenuContent(GameObject menuContent, List<GunPart> parts)
        {
            ClearChildren(menuContent);
            List<PartItemUI> content = GenerateContent(parts);
            PlaceContent(menuContent, content);
        }

        private void PlaceContent(GameObject menuContent, List<PartItemUI> content)
        {
            for (int i = 0; i < content.Count; i++)
            {
                content[i].transform.SetParent(menuContent.transform, false);
            }
        }

        private List<PartItemUI> GenerateCraftContent(List<GunPart> parts)
        {
            List<PartItemUI> items = new List<PartItemUI>();
            for (int i = 0; i < parts.Count; i++)
            {
                PartItemUI item = Instantiate(partUI, transform);
                item.part = parts[i];
                
                ClearChildren(item.gameObject);
                Attachment att = Instantiate(parts[i].attachment, item.transform);
                att.part = parts[i];
                item.GetComponent<Button>().enabled = false;
                items.Add(item);
            }

            return items;
        }

        private List<PartItemUI> GenerateContent(List<GunPart> parts)
        {
            List<PartItemUI> items = new List<PartItemUI>();
            for (int i = 0; i < parts.Count; i++)
            {
                PartItemUI item = Instantiate(partUI, transform);
                item.part = parts[i];
                items.Add(item);
            }

            return items;
        }

        private void GenerateMenuContent(GameObject menuContent, List<GunSeed> seeds)
        {
            ClearChildren(menuContent);
            List<SeedItemUI> content = GenerateContent(seeds);
            PlaceContent(menuContent, content);
        }

        private void PlaceContent(GameObject menuContent, List<SeedItemUI> content)
        {
            for (int i = 0; i < content.Count; i++)
            {
                content[i].transform.SetParent(menuContent.transform, false);
            }
        }

        private List<SeedItemUI> GenerateContent(List<GunSeed> seeds)
        {
            List<SeedItemUI> items = new List<SeedItemUI>();
            for (int i = 0; i < seeds.Count; i++)
            {
                SeedItemUI item = Instantiate(seedUI, transform);
                item.seed = seeds[i];
                items.Add(item);
            }

            return items;
        }

        private void ClearChildren(GameObject go)
        {
            for (int i = go.transform.childCount - 1; i >= 0; i--)
            {
                Destroy(go.transform.GetChild(i).gameObject);
            }
        }
        public void AddGunPart(GunPart part) 
        {
            if (part != null) 
            {
                parts.Add(part);
            }
        }

        public void PlantGun() 
        {
            Harvest harvest = GetComponent<Harvest>();
            if(seeds.Count > 0)
            {
                harvest.Plant(seeds[0]);
            }
        
        }
    }
}