using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ParcelSpawner : MonoBehaviour
{
    private IEnumerator _coroutineSpawnParcel;

    public List<GameObject> parcelList;
    public Collider2D spawningArea;

    public float waitTime = 1f;
    public float difficultyFactor = 10f;
    private float _difficultyFactorTimer = 0f;
    public float burstMode = 0.8f;
    
    public List<GameObject> parcelsInGame = new List<GameObject>();

    private static ParcelSpawner _instance;
    public static ParcelSpawner instance
    {
        get
        {
            if(_instance == null)
                _instance = GameObject.FindObjectOfType<ParcelSpawner>();
            return _instance;
        }
    }
    
    void Start()
    {
        _coroutineSpawnParcel = Spawn();
        
        StartCoroutine("Delay");
        
    }
    
    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(3f);

        StartCoroutine(_coroutineSpawnParcel);
    }

    void Update()
    {
        if (GameManager.instance.isPause || GameManager.instance.isGameover)
        {
            return;
        }

        _difficultyFactorTimer += Time.deltaTime;

        if (_difficultyFactorTimer > difficultyFactor)
        {
            _difficultyFactorTimer = 0;

            waitTime -= 0.1f;
            waitTime = Mathf.Clamp(waitTime, 0.4f, 10f);
        }
    }


    private IEnumerator Spawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);


            if (GameManager.instance.isPause || GameManager.instance.isGameover)
            {
                continue;
            }

            if (parcelsInGame.Count < 15)
            {
              SpawnParcel(); 
            }
            
        }
    }

    void SpawnParcel()
    {
        // burst mode
        if (Random.value > burstMode)
        {
            InstantiateParcel(Random.Range(2,5));
        }
        else
        {
            InstantiateParcel(1);
        }
    }

    void InstantiateParcel(int count)
    {
        for (int i = 0; i < count; i++)
        {

            Vector3 spawnPosition = RandomPointInBounds(spawningArea.bounds);

            GameObject p = parcelList[Random.Range(0, parcelList.Count)];
            GameObject go = Instantiate(p, spawnPosition, Quaternion.identity) as GameObject;

            go.transform.parent = transform;

            parcelsInGame.Add(go);
        }
    }

    public static Vector3 RandomPointInBounds(Bounds bounds)
    {
        return new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            Random.Range(bounds.min.y, bounds.max.y),
            Random.Range(bounds.min.z, bounds.max.z)
        );
    }
}