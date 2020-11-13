using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{

 
    float time = 0f;

    // Update is called once per frame
    Hamburger curHamburger;


    private void Start()
    {
        curHamburger = GameManager.instance.GetAiRecipe();
  
    }

    void Update()
    {
        if (!GameManager.instance.isPlaying)
        {
            curHamburger = null;
            return;
        }

        time += Time.deltaTime;

        if(curHamburger == null) curHamburger = GameManager.instance.GetAiRecipe();

        if (time > GameManager.instance.aiSpeed)
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
