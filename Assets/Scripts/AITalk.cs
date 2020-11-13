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

    [SerializeField]
    private AudioClip pause_sound;

    AudioSource audio;

    private bool isShowing = false;
    private int contentIndex = 0;

    private void Start()
    {
        audio = GetComponent<AudioSource>();
    }
    public void PauseShow()
    {
        StartCoroutine(PauseTalking());

    }
    public void Show()
    {
        if (isShowing) return;
        StartCoroutine(StartTalking());
    }
    public void SuccessShow()
    {
        if (isShowing) return;
        StartCoroutine(SuccessTalking());
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

    IEnumerator SuccessTalking()
    {
        isShowing = true;
        talk.SetActive(true);
        int random = Random.Range(0, success.Length);
        text.text = success[random];
        audio.clip = success_sounds[random];
        audio.Play();
        yield return new WaitForSeconds(2f);
        talk.SetActive(false);
        isShowing = false;
    }
    IEnumerator PauseTalking()
    {
        isShowing = true;
        talk.SetActive(true);
        
        text.text = "삐리릭. 시스템 이상.";
        audio.clip = pause_sound;
        audio.Play();
        yield return new WaitForSeconds(3f);
        talk.SetActive(false);
        isShowing = false;
    }



}
