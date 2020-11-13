using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{

    float speed = 3f; // 쌓는 속도


    float time = 0f;

    // Update is called once per frame
    Hamburger curHamburger;


    private void Start()
    {
        curHamburger = GameManager.instance.GetAiRecipe();
  
    }

    void Update()
    {
        time += Time.deltaTime;

 
        if(time > speed)
        {
            time = 0;
            if (curHamburger.ingredients.Count == 0)
            {
                GameManager.instance.ServeHamburger_ai();
                curHamburger = GameManager.instance.GetAiRecipe();
                print(curHamburger.ingredients.Count);
            }
            else
            {
                GameManager.instance.aiHamburger.StackIngredient(curHamburger.ingredients.Dequeue());
            }
           
     
        }
    }

}
