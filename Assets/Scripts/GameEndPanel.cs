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

    public GameObject success;
    public GameObject fail;
    
    public void SetText(int day, int playerScore,int aiScore) 
    {
        if(playerScore >= aiScore)
        {
            ClearFailText.text = "<Clear>";
            nextRoundButton.gameObject.SetActive(true);
            success.SetActive(true);
            fail.SetActive(fail);
        }
        else
        {
            ClearFailText.text = "<Fail>";
            nextRoundButton.gameObject.SetActive(false);
            success.SetActive(false);
            fail.SetActive(true);
        }

        dayText.text =  day.ToString() + "일차";
        resultText.text = $"player : {playerScore} \n Ai : {aiScore}";
        
    }
    
}
