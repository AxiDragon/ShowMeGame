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
        [SerializeField] public GameObject inventoryUI;
        [SerializeField] private GameObject plantMenuContent;
        [SerializeField] private GameObject breedMenuContent;
        [SerializeField] private GameObject craftMenuContent;
        [Header("Item Prefabs")]
        [SerializeField] private SeedItemUI seedUI;
        [SerializeField] private PartItemUI partUI;
        [Header("Events")]
        [SerializeField] private UnityEvent inventoryOpen;
        [SerializeField] private UnityEvent inventoryClosing;
        [SerializeField] private UnityEvent inventoryClosed;
        [SerializeField] private float swipeOffset = 4000f;
        [Header("Children")]
        [SerializeField] private Transform bar;
        [SerializeField] private Transform foldouts;
        [SerializeField] private float swipeTime = .2f;
        private float barIn;
        private float barOut;
        private float foldoutsIn;
        private float foldoutsOut;
        private Camera cam;

        private InventoryHintDisplayer hint;
        private bool inventoryTriggered = false;

        [HideInInspector] public bool sliding = false;
        [HideInInspector] public bool slidIn = false;

        private void Awake()
        {
            hint = GetComponent<InventoryHintDisplayer>();
            cam = Camera.main;
        }

        private void Start()
        {
            barIn = bar.localPosition.x;
            barOut = barIn + swipeOffset;
            foldoutsIn = foldouts.localPosition.x;
            foldoutsOut = foldoutsOut + swipeOffset;

            bar.localPosition = new Vector3(barOut, bar.localPosition.y, bar.localPosition.z);
            foldouts.localPosition = new Vector3(foldoutsOut, foldouts.localPosition.y, foldouts.localPosition.z);
        }

        public void InteractInventory()
        {
            if (sliding)
                return;

            inventoryTriggered = true;
            hint.canDisplay = false;

            if (!inventoryUI.activeInHierarchy)
            {
                inventoryUI.SetActive(true);
            }

            Cursor.lockState = slidIn ? CursorLockMode.Locked : CursorLockMode.None;

            if (slidIn)
                inventoryClosing?.Invoke();

            SlideInventory(!slidIn);
        }

        private void SlideInventory(bool swipeIn)
        {
            sliding = true;
            slidIn = swipeIn;

            StartCoroutine(ControlTime(swipeIn));
            bar.LeanMoveLocalX(swipeIn ? barIn : barOut, swipeTime).setEaseInOutCubic().setIgnoreTimeScale(true);
            foldouts.LeanMoveLocalX(swipeIn ? foldoutsIn : foldoutsOut, swipeTime).setEaseInOutCubic().setIgnoreTimeScale(true);
        }

        private IEnumerator ControlTime(bool swipeIn)
        {
            float end = swipeIn ? 0f : 1f;
            float start = swipeIn ? 1f : 0f;
            float camStart = swipeIn ? 60f : 30f;
            float camEnd = swipeIn ? 30f : 60f;
            float timer = 0f;

            while (timer < swipeTime)
            {
                float t = EaseInOutCubic(timer / swipeTime);
                Time.timeScale = Mathf.Lerp(start, end, t);
                cam.fieldOfView = Mathf.Lerp(camStart, camEnd, t);
                timer += Time.unscaledDeltaTime;
                yield return null;
            }

            Time.timeScale = end;
            cam.fieldOfView = camEnd;

            if (swipeIn)
            {
                inventoryOpen?.Invoke();
            }
            else
            {
                inventoryClosed?.Invoke();
                inventoryUI.SetActive(false);
            }

            sliding = false;
        }

        private float EaseInOutCubic(float x)
        {
            return x < 0.5f ? 4f * Mathf.Pow(x, 3f) : 1 - Mathf.Pow(-2f * x + 2f, 3f) / 2f;
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

        //only for button hint appearing tbh oops
        public void AddSeed(GunSeed seed)
        {
            seeds.Add(seed);
            if (!inventoryTriggered)
                hint.canDisplay = true;
        }
    }
}