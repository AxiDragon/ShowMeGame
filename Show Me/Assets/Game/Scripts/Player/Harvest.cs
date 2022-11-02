using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Harvest : MonoBehaviour
{
    [SerializeField] private GameObject plantPrefab;
    public static bool inRangeOfPlant;

    // Start is called before the first frame update
    void Start()
    {
        inRangeOfPlant = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && inRangeOfPlant == false)
        {
            GameObject plantedPot = Instantiate(plantPrefab, gameObject.transform.position - new Vector3(0,1.5f,0), Quaternion.Euler(90,0,0));
            plantedPot.GetComponent<GeweerhousePrototype.SeedPot>().plantedSeed();
        }
    }
}
