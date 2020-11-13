using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{

    float speed = 1f; // 쌓는 속도


    float time = 0f;

    // Update is called once per frame
    Hamburger curHamburger;
   

    void Update()
    {
        time += Time.deltaTime;

        if(curHamburger == null)
        {
            curHamburger = GameManager.instance.GetAiRecipe();
        }
 
        if(time > speed)
        {
            time = 0;
            if (curHamburger.ingredients.Count == 0)
            {
                curHamburger = null;
                GameManager.instance.ServeHamburger_ai();
            }
            else
            {
                GameManager.instance.aiHamburger.StackIngredient(curHamburger.ingredients.Dequeue());
            }
           
     
        }
    }

}
