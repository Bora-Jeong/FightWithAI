using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    float time = 0f;
    private Animator animator;
    Hamburger curHamburger;
    private bool isWorking = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        curHamburger = GameManager.instance.GetAiRecipe();
    }

    public void StartWork()
    {
        isWorking = true;
        animator.SetBool("Working", true);
    }

    public void StopWork()
    {
        isWorking = false;
        curHamburger = null;
        animator.SetBool("Working", false);
    }

    public void GrabIngredient()
    {

    }

    public void OutIngredient()
    {

    }

    void Update()
    {
        if (!isWorking) return;

        time += Time.deltaTime;

        if(curHamburger == null) curHamburger = GameManager.instance.GetAiRecipe();

        if (time > GameManager.instance.aiSpeed)
        {
            time = 0;
            if (curHamburger.ingredients.Count == 0)
            {
                GameManager.instance.ServeHamburger_ai();
                curHamburger = GameManager.instance.GetAiRecipe();
            }
            else
            {
                GameManager.instance.aiHamburger.StackIngredient(curHamburger.ingredients.Dequeue());
            }          
     
        }
    }

}
