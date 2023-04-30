using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PlayerPoints : MonoBehaviour
{
    
    public TextMeshProUGUI uiText;
    private void Awake()
    {
        Truck.OnUpdateTotalScore += UpdateScore;
    }

    private void UpdateScore(int score, int i)
    {
        uiText.text =score.ToString() + "Kg";
    }

}
