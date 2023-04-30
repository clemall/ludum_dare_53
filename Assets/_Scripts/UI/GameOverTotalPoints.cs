using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GameOverTotalPoints : MonoBehaviour
{
    
    public TextMeshProUGUI uiText;
    

    private void Update()
    {
        string bossComment = "";
        int score = GameManager.instance.playerScore;
       
        if (score > 200)
        {
            bossComment  = $"You are a good employee. You managed to deliver an amazing amount of {score.ToString()}Kg of parcels. " +
                               $"Happy to see you back tomorrow.";
        }
        else if (score > 100)
        {
            bossComment  = $"You are not the best employee of the day but you did good enough. You managed to deliver {score.ToString()}Kg of parcels. " +
                               $"Try to do better tomorrow <br><br>Note: Try to be more gentle with the parcels. Customers don't like broken goods.";
        }
        else if (score < 100)
        {
            bossComment  = $"Your performance of today was hard to watch. You managed to only deliver {score.ToString()}Kg of parcels. " +
                               $"Hopefully you will do better tomorrow <br><br>Note:Do not forget to be gentle with the parcels. Customers don't like broken goods.";
        }
        uiText.text = bossComment;
        
    }

}
