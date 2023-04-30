using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class TruckPoints : MonoBehaviour
{
    
    public TextMeshProUGUI uiText;
    public Color badColor;
    public Color goodColor;

    public GameObject arrowBad;
    public GameObject arrowMore;

    public RectTransform timer;
    private void Awake()
    {
        Truck.OnUpdateCurrentTruckScore += UpdateScore;
        Truck.OnDoorOpen += DoorOpen;
    }

    private void DoorOpen(float duration)
    {
        if (timer == null)
        {
            return;
            
        }
        timer.localScale = new Vector3(1, 1, 1);
        timer.DOScaleX(0, duration).SetEase(Ease.Linear).SetId("toBePause");
    }

    private void UpdateScore(int score, int min, int max)
    {
        if (uiText == null || arrowMore == null|| arrowBad == null)
        {
            return;
        }
        if (score < min || score > max)
        {
            uiText.color = badColor;
        }
        else if (score == 0)
        {
            uiText.color = Color.white;
        }
        else
        {
            uiText.color = goodColor;
        }
        
        if (score < min)
        {
            arrowMore.SetActive(true);
            arrowBad.SetActive(false);
        }
        else if (score > max)
        {
            arrowMore.SetActive(false);
            arrowBad.SetActive(true);
        }
        else
        {
            arrowMore.SetActive(false);
            arrowBad.SetActive(false);
        }
        
        uiText.text = score.ToString() + "Kg";
    }

}
