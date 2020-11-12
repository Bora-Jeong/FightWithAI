using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Dialogue
{
    public int bgIndex; // 배경 사진 몇번째
    public string content;
}

public enum Ingredient
{
    TopBread,
    Tomato,
    Lettuce,
    Cheese,
    Patty,
    BottomBread
}

public class GameManager : SingleTon<GameManager>
{
    [SerializeField] DialoguePanel _dialoguePanel;
    [SerializeField] GameObject _gamePanel;

    [Header("Prologue")]
    [SerializeField] Sprite[] _prologueBG;
    [SerializeField] Dialogue[] _dialogues;

    [Header("Game")]
    [SerializeField] Text _dayText;
    [SerializeField] Slider _timeSlider;
    [SerializeField] Text _timeText;
    [SerializeField] Text _playerScoreText;
    [SerializeField] Text _aiScoreText;

    private int _day;
    private float _totalTime;
    private float _remainTime;
    private int _playerScore;
    private int _aiScore;

    public bool isPlaying { get; private set; } // 게임 중?
    public int playerScore
    {
        get => _playerScore;
        set
        {
            _playerScore = value;
            _playerScoreText.text = $"Score {_playerScore}";
        }
    }

    public int aiScore
    {
        get => _aiScore;
        set
        {
            _aiScore = value;
            _aiScoreText.text = $"Score {_aiScore}";
        }
    }

    public float remainTime
    {
        get => _remainTime;
        set
        {
            _remainTime = value;
            _timeText.text = $"{_remainTime}초";
            _timeSlider.value = _remainTime / _totalTime;
        }
    }

    private void Awake()
    {
        StartCoroutine(StartPrologue());
    }

    IEnumerator StartPrologue()  // 프롤로그 시작
    {
        _gamePanel.gameObject.SetActive(false);
        _dialoguePanel.gameObject.SetActive(true);
        for (int i = 0; i < _dialogues.Length; i++)
        {
            _dialoguePanel.Set(_prologueBG[_dialogues[i].bgIndex], _dialogues[i].content);
            yield return new WaitForSeconds(2f);
        }

        _dialoguePanel.gameObject.SetActive(false);
        _gamePanel.gameObject.SetActive(true);
        RoundStart(1, 60f);
    }

    private void RoundStart(int day, float time) // 라운드 시작
    {
        _day = day;
        _dayText.text = $"{_day}일차";
        _totalTime = time;
        remainTime = _totalTime;
        playerScore = 0;
        aiScore = 0;

        isPlaying = true;
        StartCoroutine(GameSchedulling());
    }

    IEnumerator GameSchedulling()
    {
        while(remainTime > 0)
        {
            remainTime -= Time.deltaTime;
            yield return null;
        }

        GameOver();
    }

    private void GameOver() // 게임 오버
    {
        isPlaying = false;
    }
}
