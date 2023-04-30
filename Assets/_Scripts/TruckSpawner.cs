using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TruckSpawner : MonoBehaviour
{
    public List<GameObject> TruckList;
    public Vector3 spawningArea;

    public bool isFirstTruck = true;
    
    private static TruckSpawner _instance;
    public static TruckSpawner instance
    {
        get
        {
            if(_instance == null)
                _instance = GameObject.FindObjectOfType<TruckSpawner>();
            return _instance;
        }
    }


    private void Start()
    {
        
        StartCoroutine("FirstTruck");
    }
    
    private IEnumerator FirstTruck()
    {
        yield return new WaitForSeconds(3f);

        SpawnTruck();
    }


    public void SpawnTruck()
    {
        GameObject p = TruckList[Random.Range(0, TruckList.Count)];
        if (isFirstTruck)
        {
            p = TruckList[0];
            isFirstTruck = false;
        }
        Instantiate(p, spawningArea, Quaternion.identity);
    }

   
}