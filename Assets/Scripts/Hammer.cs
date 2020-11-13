using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Show()
    {
        gameObject.SetActive(true);
        animator.SetTrigger("Gang");
    }

    public void PlaySfx()
    {
        AudioManager.instance.hammerSound();
    }

    public void End()
    {
        gameObject.SetActive(false);
    }
}
