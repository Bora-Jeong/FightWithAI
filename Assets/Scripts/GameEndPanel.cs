using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameEndPanel : MonoBehaviour
{
    public Text ClearFailText;
    public Button retryButton;
    public Button nextRoundButton;
    public Text dayText;
    public Text resultText;
   
    
    public void SetText(int day, int playerScore,int aiScore) 
    {
        if(playerScore >= aiScore)
        {
            ClearFailText.text = "<Clear>";
            nextRoundButton.interactable = true;
        }
        else
        {
            ClearFailText.text = "<Fail>";
            nextRoundButton.interactable = false;
        }
      
        dayText.text =  day.ToString() + "일차";
        resultText.text = $"player : {playerScore} \n Ai : {aiScore}";

        
    }
    
}
