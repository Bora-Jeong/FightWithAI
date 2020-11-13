using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recipe : MonoBehaviour
{
    [SerializeField]
    private Sprite normal;
    [SerializeField]
    private Sprite broken;

    private SpriteRenderer sr;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    public void Hide()
    {
        StartCoroutine(StartHide());
    }

    private IEnumerator StartHide()
    {
        sr.sprite = broken;

        yield return new WaitForSeconds(5f);

        sr.sprite = normal;
    }
}
