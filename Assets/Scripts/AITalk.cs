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
    private AudioClip[] sounds;
    [SerializeField]
    private string[] success;
    [SerializeField]
    private AudioClip[] success_sounds;

    AudioSource audio;

    private bool isShowing = false;
    private int contentIndex = 0;

    private void Start()
    {
        audio = GetComponent<AudioSource>();
    }
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
        audio.clip = sounds[contentIndex];
        audio.Play();
        contentIndex = (contentIndex + 1) % contents.Length;
        yield return new WaitForSeconds(2f);
        talk.SetActive(false);
        isShowing = false;
    }

}
