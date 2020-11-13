using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameEndPanel : MonoBehaviour
{
    public Text dayText;
    public Text resultText;
    
    public void SetText(int day, int playerScore,int aiScore) 
    {
        dayText.text = day.ToString();
        resultText.text = $"player : {playerScore}";
        
    }
    
}
