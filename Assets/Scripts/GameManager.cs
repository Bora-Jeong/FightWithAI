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
    Cabbage,
    Cheese,
    Patty,
    BottomBread
}

public class GameManager : Singleton<GameManager>
{
    [SerializeField] DialoguePanel _dialoguePanel;
    [SerializeField] GameObject _gamePanel;
    [SerializeField] GameEndPanel _gameEndPanel;
    [SerializeField] Sprite[] _ingredientSprites;
    [SerializeField] Sprite[] _aiIngredientSprites;

    [Header("Prologue")]
    [SerializeField] Sprite[] _prologueBG;
    [SerializeField] Dialogue[] _dialogues;

    [Header("Game")]
    [SerializeField] Text _dayText;
    [SerializeField] Slider _timeSlider;
    [SerializeField] Text _timeText;
    [SerializeField] Text _playerScoreText;
    [SerializeField] Text _aiScoreText;

    [SerializeField] Transform _recipeRoot;
    [SerializeField] Transform _aiRecipeRoot;
    [SerializeField] GameObject _recipe;
    [SerializeField] AITalk _aiTalk;
    [SerializeField] AI _ai;

    private int _day;
    private float _totalTime;
    private float _remainTime;
    private int _playerScore;
    private int _aiScore;

    private Queue<Hamburger> _recipeQ = new Queue<Hamburger>(); // 플레이어가 만들어야 하는 주문서들

    public Hamburger playerHamburger;

    private Queue<Hamburger> _aiRecipeQ = new Queue<Hamburger>(); // AI의 주문서들

    public Hamburger aiHamburger;

    private readonly float _aiTalkTerm = 8f; // 8초에 한번씩 도발
    private float _aiTalkTime; // AI 말하는 타이머
    public bool isPlaying { get; private set; } // 게임 중?

    private float _aiSpeed = 3;
    public float aiSpeed {
        get
        {
            return _aiSpeed;
        }
        set
        {
            _aiSpeed = value;
            if(_aiSpeed < 0.5f)
            {
                _aiSpeed = 0.5f;
            }
        }
    
    } // ai가 재료 하나를 쌓는 시간

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
            _timeText.text = $"{(int)_remainTime}초";
            _timeSlider.value = _remainTime / _totalTime;
        }
    }

    private void Awake()
    {
        //StartCoroutine(StartPrologue());

        // 임시코드 프롤로그 시작 안하고 바로 게임 시작
        _dialoguePanel.gameObject.SetActive(false);
        _gamePanel.gameObject.SetActive(true);
        RoundStart(1, 60f);
        // 임시 코드
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

    public void nextRound()
    {
        aiSpeed -= aiSpeed * 0.1f;
        RoundStart(_day + 1, _totalTime);
    }

    public void Restart()
    {
        RoundStart(_day, _totalTime);
    }

    private void RoundStart(int day, float time) // 라운드 시작
    {
        _day = day;
        _dayText.text = $"{_day}일차";
        _totalTime = time;
        remainTime = _totalTime;
        playerScore = 0;
        aiScore = 0;
        _aiTalkTime = _aiTalkTerm;
        playerHamburger.Discard();
        aiHamburger.Discard();
        RefreshRecipe();

        isPlaying = true;
        _ai.StartWork();
        StartCoroutine(GameSchedulling());
    }

    IEnumerator GameSchedulling()
    {
        while (remainTime > 0)
        {
            remainTime -= Time.deltaTime;
            _aiTalkTime -= Time.deltaTime;
            if(_aiTalkTime <= 0) // AI가 도발하는 멘트
            {
                _aiTalk.Show();
                _aiTalkTime = _aiTalkTerm;
            }
            yield return null;
        }

        GameOver();
    }

    private void GameOver() // 게임 오버
    {
        isPlaying = false;
        _ai.StopWork();
        _gameEndPanel.gameObject.SetActive(true);
        _gameEndPanel.SetText(_day, playerScore, aiScore);
    }

    private void RefreshRecipe()
    {
        while(_recipeQ.Count > 0)
        {
            Hamburger temp = _recipeQ.Dequeue();
            DestroyImmediate(temp.transform.parent.gameObject);
        }
        while(_aiRecipeQ.Count > 0)
        {
            Hamburger temp = _aiRecipeQ.Dequeue();
            DestroyImmediate(temp.transform.parent.gameObject);
        }

        for (int i = 0; i < 30; i++) // 라운드 시작시 일단 6개 레시피 로드해놓음
        {
            Hamburger hamburger = GetRandomHamburger();
            GameObject recipe = Instantiate(_recipe, _recipeRoot);
            hamburger.transform.SetParent(recipe.transform);
            hamburger.transform.localScale = Vector3.one * 1.2f;
            hamburger.transform.localPosition = new Vector3(0, -40, 0);
            _recipeQ.Enqueue(hamburger);

            GameObject copy = Instantiate(recipe, _aiRecipeRoot);
            copy.transform.localScale = Vector3.one * 0.6f;
            copy.transform.localPosition = new Vector3(0, -30, 0);
            Hamburger aiBurger = copy.GetComponentInChildren<Hamburger>();
            aiBurger.ingredients = new Queue<Ingredient>(hamburger.ingredients);
            _aiRecipeQ.Enqueue(aiBurger);
        }
    }

    public void OnServingButton()
    {
        Queue<Ingredient> player = playerHamburger.ingredients;

        Hamburger recipe = _recipeQ.Peek(); // 레시피 제일 앞 버거
        Queue<Ingredient> backup = new Queue<Ingredient>(recipe.ingredients);

        bool success = true;
        while (player.Count > 0 && recipe.ingredients.Count > 0)
        {
            //Debug.Log($"Player {player.Peek()}  vs {recipe.ingredients.Peek()}");
            if (player.Dequeue() != recipe.ingredients.Dequeue())
            {
                success = false;
                break;
            }
        }

        if (player.Count > 0 || recipe.ingredients.Count > 0)
            success = false;

        if (success) // 성공시
        {
            _recipeQ.Dequeue();
            Destroy(recipe.transform.parent.gameObject); // UI도 삭제
            playerScore++;
        }
        else
            recipe.ingredients = backup;

        OnDumpButton(); // 플레이어 큐, 쟁반 클리어
    }


    public void OnDumpButton()
    {
        playerHamburger.ingredients.Clear(); // 플레이어 큐 clear
        for (int i = playerHamburger.transform.childCount - 1; i >= 0; i--) // 쟁반 클리어
            Destroy(playerHamburger.transform.GetChild(i).gameObject);
    } 

    public void ServeHamburger_ai()
    {
        Hamburger recipe = _aiRecipeQ.Dequeue();

        aiHamburger.Discard();
        
        aiScore++;
        Destroy(recipe.transform.parent.gameObject);

    }

    private Hamburger GetRandomHamburger()
    {
        Hamburger hamburger = new GameObject("Hamburger").AddComponent<Hamburger>();
        int count = Random.Range(_day + 1, _day + 3); // 1일차 최소 2 , 최대 3개
        hamburger.StackIngredientUI(Ingredient.BottomBread);
        for (int i = 0; i < count; i++)
            hamburger.StackIngredientUI((Ingredient)Random.Range(1, 5));
        hamburger.StackIngredientUI(Ingredient.TopBread);
        return hamburger;
    }
    

    public Hamburger GetAiRecipe()
    {
        return _aiRecipeQ.Peek();
    }

    public Sprite GetIngredientSprite(Ingredient ingredient, bool isAI = false) // 재료 사진 얻는 함수!!!
    {
        if(isAI)  return _aiIngredientSprites[(int)ingredient];
        else  return _ingredientSprites[(int)ingredient];
    }
}