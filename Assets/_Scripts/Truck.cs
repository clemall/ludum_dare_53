using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using DG.Tweening;
using UnityEngine.PlayerLoop;

public class Truck : MonoBehaviour
{

    public float finalPosition;
    public float startPosition = -50f;

    public int scoreGoalMinimum;
    public int scoreGoalMaximum;
    public int currentScore = 0;
    
    public float durationAnimationTruck = 2.0f;

    public float durationDoorOpen = 5.0f;

    public float durationAnimationDoor = 1.0f;

    public Transform door;
    private float _doorScaleX;

    public  TruckColliderPoints truckCollider;
    public  SpriteRenderer hidingLayerSprite;

    public GameObject driverAskedLoad;

    public static event Action<int, int, int> OnUpdateTruckLoad;
    public static event Action<float> OnDoorOpen;
    public static event Action<int, int, int> OnUpdateCurrentTruckScore;
    public static event Action<int, int> OnUpdateTotalScore;

    public AudioSource truckStart;
    
    void Awake()
    {
        _doorScaleX = door.transform.localScale.x;
    }
    void Start()
    {
        driverAskedLoad.SetActive(false);
        transform.DOMoveX(finalPosition, durationAnimationTruck).SetEase(Ease.OutQuad).OnComplete(OpenDoor).SetId("toBePause");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddPoints(int points)
    {
        currentScore += points;
        OnUpdateCurrentTruckScore ?.Invoke(currentScore, scoreGoalMinimum, scoreGoalMaximum);
    }

    public void RemovePoints(int points)
    {
        currentScore -= points;
        OnUpdateCurrentTruckScore ?.Invoke(currentScore, scoreGoalMinimum, scoreGoalMaximum);
    }

    
    [Button("Leave Truck")]
    public void LeaveTruck()
    {
        transform.DOMoveX(startPosition - 5f, durationAnimationTruck + 1f).SetEase(Ease.InCirc);
        truckStart.Play();
        CleanParcels();
        CheckLost();
        StartCoroutine("InitializeNewTruck");
    }
    
    private IEnumerator InitializeNewTruck()
    {
        yield return new WaitForSeconds(durationAnimationTruck + 2f);

        if (gameObject.activeSelf && GameManager.instance.isGameover == false)
        {
            TruckSpawner.instance.SpawnTruck();
            // Destroy(gameObject);
            gameObject.SetActive(false);
        }
    }
 

    private void CleanParcels()
    {
        truckCollider.acceptParcel = false;
        truckCollider.gameObject.SetActive(false);
        for (int i = 0; i < truckCollider.listParcels.Count; i++) 
        {
            truckCollider.listParcels[i].SetActive(false);
            ParcelSpawner.instance.parcelsInGame.Remove(truckCollider.listParcels[i]);
        }
    }

    private void CheckLost()
    {
        GameManager.instance.playerScore += currentScore;
        OnUpdateTotalScore ?.Invoke(GameManager.instance.playerScore, currentScore);
        
        if (currentScore < scoreGoalMinimum || currentScore > scoreGoalMaximum )
        {
            GameManager.instance.GameOver();
            return;
        }
        
        // reset
        OnUpdateCurrentTruckScore ?.Invoke(0, 0,0);
    }

    [Button("Open")]
    public void OpenDoor()
    {
        OnUpdateTruckLoad ?.Invoke(scoreGoalMinimum, scoreGoalMaximum, currentScore);
        OnDoorOpen ?.Invoke(durationDoorOpen + durationAnimationDoor);
        driverAskedLoad.SetActive(true);
        hidingLayerSprite.DOFade(0f, durationAnimationDoor).SetId("toBePause");
        CanvasGroup childrenHidingLayer = hidingLayerSprite.GetComponentInChildren<CanvasGroup>();
        childrenHidingLayer.DOFade(0f, durationAnimationDoor).SetId("toBePause");
        door.DOScaleX(0.01f, durationAnimationDoor).SetEase(Ease.OutQuad).OnComplete(CloseDoorAfterDelay).SetId("toBePause");
    } 
    
    [Button("Close")]
    public void CloseDoorAfterDelay()
    {
        
        
        hidingLayerSprite.DOFade(1f, durationAnimationDoor).SetDelay(durationDoorOpen).SetId("toBePause");
        CanvasGroup childrenHidingLayer = hidingLayerSprite.GetComponentInChildren<CanvasGroup>();
        childrenHidingLayer.DOFade(1f, durationAnimationDoor).SetDelay(durationDoorOpen).SetId("toBePause");
        door.DOScaleX(_doorScaleX, durationAnimationDoor).SetEase(Ease.OutQuad).SetDelay(durationDoorOpen).OnComplete(LeaveTruck).SetId("toBePause");

    }

    [Button("Debug Open")]
    public void openDoorDebug()
    {
        door.DOScaleX(0.01f, durationAnimationDoor).SetId("toBePause");
    } 
    
    [Button("debugClose")]
    public void closeDoorAfterDelayDebug()
    {
        door.DOScaleX(_doorScaleX, durationAnimationDoor).SetId("toBePause");
    } 
}
