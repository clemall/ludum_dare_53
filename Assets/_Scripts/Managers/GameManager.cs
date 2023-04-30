using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool isPause = false;
    public bool isGameover = false;

    public int playerScore = 0;


    public GameObject GameOverPopup;


    private static GameManager _instance;
    public static GameManager instance
    {
        get
        {
            if(_instance == null)
                _instance = GameObject.FindObjectOfType<GameManager>();
            return _instance;
        }
    }

    private void Awake()
    {
        DOTween.Init();
    }

    public void Pause(){
        print("Game is paused");
        isPause = true;
        
        DOTween.Pause("toBePause");
    }

    public void UnPause(){
        isPause = false;
        DOTween.PlayAll();
    }


    public void GameOver(){
        isPause = true;
        isGameover = true;
        
        DOTween.Pause("toBePause");
        
        GameOverPopup.SetActive(true);
    }


}
