using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class TruckDriverAskedLoad : MonoBehaviour
{
    
    public TextMeshProUGUI uiText;

    private void Awake()
    {
        Truck.OnUpdateTruckLoad += UpdateScore;
        Truck.OnUpdateTotalScore += SayThanks;
    }

    private void UpdateScore(int scoreGoalMinimum, int scoreGoalMaximum, int currentScore)
    {
        uiText.text = "Hey, I need between " + scoreGoalMinimum.ToString() + "Kg and " + scoreGoalMaximum.ToString() + "Kg. Be fast!";
    }
    
    private void SayThanks(int i, int currentScore)
    {
        int r = Random.Range(0, 4);
        if (r == 0)
        {
            uiText.text =  currentScore.ToString() + "Kg is good enough, see ya!" ;
        }
        else if (r == 1)
        {
             uiText.text = "Ok thanks, " + currentScore.ToString() + "Kg will do." ;
        }
        else if (r == 2)
        {
             uiText.text = "That wasn't to long for " + currentScore.ToString() + "Kg." ;
        }
        else if (r == 3)
        {
             uiText.text = "How can you transport " + currentScore.ToString() + "Kg of parcels so fast? bye." ;
        }
        
    }

}
