using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialoguePanel : MonoBehaviour
{
    [SerializeField]
    private Image _background;
    [SerializeField]
    private Text _text;

    public void Set(Sprite sprite, string content)
    {
        _background.sprite = sprite;
        _text.text = content;
    }
}
