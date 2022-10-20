using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CosmosGarden
{
    public class FoodDrag : MonoBehaviour
    {
        [SerializeField] LayerMask planeMask;
        Food currentFood = null;

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                currentFood = GetFood();
            }

            if (Input.GetMouseButtonUp(0))
                currentFood = null;

            if (currentFood == null)
                return;

            Vector3 pos = GetPlanePoint();

            if (pos != Vector3.zero)
                currentFood.transform.position = pos;
        }

        private Food GetFood()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.TryGetComponent<Food>(out var food))
                    return food;
            }

            return null;
        }

        private Vector3 GetPlanePoint()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, planeMask))
            {
                print(hit.collider.name);
                return hit.point;
            }

            return Vector3.zero;
        }
    }
}
