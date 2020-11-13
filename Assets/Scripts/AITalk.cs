using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AITalk : MonoBehaviour
{
    [SerializeField]
    private GameObject talk;
    [SerializeField]
    private Text text;
    [SerializeField]
    private string[] contents;
    [SerializeField]
    private string[] success;

    private bool isShowing = false;
    private int contentIndex = 0;

    public void Show()
    {
        if (isShowing) return;
        StartCoroutine(StartTalking());
    }

    IEnumerator StartTalking()
    {
        isShowing = true;
        talk.SetActive(true);
        text.text = contents[contentIndex];
        contentIndex = (contentIndex + 1) % contents.Length;
        yield return new WaitForSeconds(2f);
        talk.SetActive(false);
        isShowing = false;
    }

}
