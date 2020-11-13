using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recipe : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Hide()
    {
        StartCoroutine(StartHide());
    }

    private IEnumerator StartHide()
    {
        Hamburger hamburger = GetComponentInChildren<Hamburger>();
        hamburger.gameObject.SetActive(false);
        animator.SetBool("Out", true);

        yield return new WaitForSeconds(3.5f);

        animator.SetBool("Out", false);
        hamburger.gameObject.SetActive(true);
    }
}
