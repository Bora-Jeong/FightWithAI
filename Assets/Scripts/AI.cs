using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer hand;

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
        curHamburger = GameManager.instance.GetAiRecipe();
        animator.SetBool("Working", true);
    }

    public void StopWork()
    {
        isWorking = false;
        curHamburger = null;
        animator.SetBool("Working", false);
    }

    public void GrabIngredient() // 재료 집기
    {
        if (curHamburger.ingredients.Count == 0)
        {
            GameManager.instance.ServeHamburger_ai();
            curHamburger = GameManager.instance.GetAiRecipe();
        }

        Ingredient ingredient = curHamburger.ingredients.Peek();
        hand.sprite = GameManager.instance.GetIngredientSprite(ingredient, true);
    }

    public void OutIngredient() // 재료 놓기
    {
        hand.sprite = null;
        GameManager.instance.aiHamburger.StackIngredient(curHamburger.ingredients.Dequeue(), true);
    }

    void Update()
    {
        //if (!isWorking) return;

        //time += Time.deltaTime;

        //if(curHamburger == null) curHamburger = GameManager.instance.GetAiRecipe();

        //if (time > GameManager.instance.aiSpeed)
        //{
        //    time = 0;
        //    if (curHamburger.ingredients.Count == 0)
        //    {
        //        GameManager.instance.ServeHamburger_ai();
        //        curHamburger = GameManager.instance.GetAiRecipe();
        //    }
        //    else
        //    {
        //        GameManager.instance.aiHamburger.StackIngredient(curHamburger.ingredients.Dequeue());
        //    }          
     
        //}
    }

}
