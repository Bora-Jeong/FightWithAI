﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer hand;

    float time = 0f;
    private Animator animator;
    Hamburger curHamburger;
    private Ingredient curIngredient;
    private bool isWorking;

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

    public void GrabIngredient() // 재료 짚기
    {
        if (curHamburger.ingredients.Count == 0)
        {
            GameManager.instance.ServeHamburger_ai();
            curHamburger = GameManager.instance.GetAiRecipe();
        }

        curIngredient = curHamburger.ingredients.Dequeue();
        hand.sprite = GameManager.instance.GetIngredientSprite(curIngredient, true);
    }

    public void OutIngredient() // 재료 놓기
    {
        Vector3 dest = GameManager.instance.aiHamburger.GetDestination(curIngredient, out GameObject go);
        go.transform.position = hand.transform.position;
        hand.sprite = null;

        LeanTween.moveY(go, dest.y, 0.3f);
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
